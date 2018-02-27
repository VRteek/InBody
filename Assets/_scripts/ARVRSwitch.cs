using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Vuforia;

public class ARVRSwitch : MonoBehaviour {

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (Menu.SceneName == "VR") {
			StartCoroutine (LoadDevice ("cardboard"));
			Camera.main.GetComponent<VuforiaBehaviour> ().enabled = false;
			XRSettings.enabled = true;
		} else if (Menu.SceneName == "AR") {
			Camera.main.GetComponent<VuforiaBehaviour> ().enabled = true;
			XRSettings.enabled = false;
		}
	}
	IEnumerator LoadDevice (string newDevice) {
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}
}