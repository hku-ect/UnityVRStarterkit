using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HKUECT;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class CalibrationData : ScriptableObject {
	public bool isValid = false;
	public float centerX, centerY, centerZ;
	public float yRotation;
	public float scale;

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/CalibrationData")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CalibrationData> ();
	}
	#endif
}

public class Calibration : MonoBehaviour {
	public CalibrationData calibrationData;

	public string LeftName, RightName;
	public OptiTrackOSCClient client;
	public GameObject[] swapActiveWhenCalibrated;
	bool calibrated = false;

	void Start() {
		//enter from calibrated scene
		if (calibrationData != null && calibrationData.isValid) {
			//apply to current setup
			ApplyCalibrationData();
			calibrated = true;
			//Debug.Log ("Applied Calibration");
			SwapActives ();
		}
	}

	void Update() {
		VRMocapMatchPosition l, r;
		l = VRMocapMatchPosition.Get (VRMocapMatch.LEFT);
		r = VRMocapMatchPosition.Get (VRMocapMatch.RIGHT);
		if (l && r) {
			if (l.HasValue () && r.HasValue () && !calibrated) {
				calibrated = PerformCalibration ();
				if (calibrated) {
					SwapActives ();
				}
			}
		}
	}

	void SwapActives() {
		foreach( GameObject g in swapActiveWhenCalibrated ) {
			g.SetActive (!g.activeSelf);
		}
	}
	
	public bool PerformCalibration() {
		Vector3 oscLeft, oscRight, left, right, center;
		oscLeft = oscRight = left = right = center = Vector3.zero;
		float yOffset = 0, rot = 0, scale = 1;

		//re-get the OSC objects
		RigidbodyDefinition def_xz, def_minxminz;
		if (OptiTrackOSCClient.GetRigidbody(LeftName, out def_xz)) {
			oscLeft = def_xz.position;
		}
		if (OptiTrackOSCClient.GetRigidbody(RightName, out def_minxminz)) {
			oscRight = def_minxminz.position;
		}

		//store our anchored calibration data (virtual positions of 
		left = VRMocapMatchPosition.Get(VRMocapMatch.LEFT).GetPosition();
		right = VRMocapMatchPosition.Get(VRMocapMatch.RIGHT).GetPosition();

		//store average y-offset for all anchors (gives user some leeway for error)... do we even need this?
		yOffset += left.y - oscLeft.y;
		yOffset += right.y - oscRight.y;
		yOffset = yOffset * .5f;

		//IDEA 1b
		//determine offset between vXZOsc and anchor
		//Vector3 vXZOffset = oscLeft - left;

		//compare the "same" line, unrotated v rotated, to determine angle between
		Vector3 v1, v2;
		v1 = right - left;           //rotated
		v2 = oscRight - oscLeft;     //unrotated
		//align the height values
		v1.y = v2.y;

		rot = Vector3.Angle(v1, v2);    //does this return a signed value? NOPE
		Vector3 cross = Vector3.Cross(v1, v2);
		if (cross.y < 0) rot = -rot;    //hope this works

		//rotate world center around this angle (how do I know which direction, does this matter?
		client.transform.rotation = Quaternion.Euler(0, -rot, 0);

		//rotate the optitrack XZ over the same angle
		Vector3 rotatedXZ = Quaternion.Euler(0, -rot, 0) * oscLeft;
		//subtract this from XZ Object position to get "real center" and position world center there
		center = left - rotatedXZ * (v1.magnitude / v2.magnitude); //take scale diff into account
		client.transform.position = center;

		//apply scale
		scale = (v1.magnitude / v2.magnitude);
		client.scale = scale;

		if (calibrationData != null) {
			calibrationData.isValid = true;
			calibrationData.centerX = center.x;
			calibrationData.centerY = center.y;
			calibrationData.centerZ = center.z;
			calibrationData.yRotation = rot;
			calibrationData.scale = scale;
		}

		return true;
	}

	void ApplyCalibrationData() {
		client.transform.rotation = Quaternion.Euler(0, -calibrationData.yRotation, 0);
		client.transform.position = new Vector3( calibrationData.centerX, calibrationData.centerY, calibrationData.centerZ );
		client.scale = calibrationData.scale;
	}
}
