using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VRMocapMatch
{
	LEFT,
	RIGHT
}

public class VRMocapMatchPosition : MonoBehaviour {

	static Dictionary<VRMocapMatch,VRMocapMatchPosition> matchObjects = new Dictionary<VRMocapMatch, VRMocapMatchPosition> ();

	public VRMocapMatch matchPos = VRMocapMatch.LEFT;

	SteamVR_TrackedController controller;
	Vector3? storedPosition = null;

	public static VRMocapMatchPosition Get( VRMocapMatch position ) {
		if (matchObjects.ContainsKey (position)) {
			return matchObjects [position];
		}

		return null;
	}

	public bool HasValue() {
		return storedPosition != null;
	}

	public Vector3 GetPosition() {
		if (storedPosition == null)
			return transform.position;
		else
			return storedPosition.Value;
	}

	// Use this for initialization
	void Awake () {
		matchObjects.Add(matchPos,this);
		controller = GetComponent<SteamVR_TrackedController> ();
		if (controller) {
			controller.TriggerClicked += TriggerClicked;
		}
	}
	
	void OnDestroy() {
		matchObjects.Remove(matchPos);
	}

	void TriggerClicked( object sender, ClickedEventArgs args ) {
		storedPosition = transform.position;
	}
}