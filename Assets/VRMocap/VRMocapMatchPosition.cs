using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRMocapMatchPosition : MonoBehaviour {

	static Dictionary<SteamVR_Input_Sources,VRMocapMatchPosition> matchObjects = new Dictionary<SteamVR_Input_Sources, VRMocapMatchPosition> ();

	public SteamVR_Input_Sources matchPos = SteamVR_Input_Sources.LeftHand;
	public SteamVR_Action_Boolean inputAction;

	//SteamVR_TrackedObject controller;
	Vector3? storedPosition = null;

	public static VRMocapMatchPosition Get( SteamVR_Input_Sources position ) {
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
		inputAction.onStateDown += TriggerClicked;
	}
	
	void OnDestroy() {
		matchObjects.Remove(matchPos);
	}

	void TriggerClicked( SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource ) {
		//Debug.Log(matchPos.ToString());
		storedPosition = transform.position;
	}
}