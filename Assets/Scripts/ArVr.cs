using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArVr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadDevice (""));
	}

	// Update is called once per frame
	IEnumerator LoadDevice (string newDevice) {
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}
}