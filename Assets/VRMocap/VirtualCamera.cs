using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HKUECT;

public class VirtualCamera : MonoBehaviour {

	public GameObject cam, zoom1, zoom2;
	//public string cam, zoom1, zoom2;

	[Range(1,90)]
	public float minFOV;
	[Range(0.01f, 100.0f)]
	public float fovScale;

	RigidbodyDefinition camDef, zoomDef1, zoomDef2;
	Transform t;
	Camera mCamera;

	void Start() {
		t = transform;
		mCamera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void LateUpdate () {
		//if (OptiTrackOSCClient.GetRigidbody (cam, out camDef)) {
		if ( cam != null ) {
			t.position = cam.transform.position;//camDef.position;
			t.rotation = cam.transform.rotation;//camDef.rotation;
		}
		//if (OptiTrackOSCClient.GetRigidbody (zoom1, out zoomDef1)) {
		//	if (OptiTrackOSCClient.GetRigidbody (zoom2, out zoomDef2)) {
		if ( zoom1 != null && zoom2 != null ) {
				//mCamera.fieldOfView = minFOV + Vector3.Distance (zoomDef1.position, zoomDef2.position) * fovScale;
			mCamera.fieldOfView = minFOV + Vector3.Distance (zoom1.transform.position, zoom2.transform.position) * fovScale;
			Debug.Log (minFOV + Vector3.Distance (zoom1.transform.position, zoom2.transform.position) * fovScale);
		//	}
		}
	}
}
