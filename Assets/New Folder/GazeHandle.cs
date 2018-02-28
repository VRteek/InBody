using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GazeHandle : MonoBehaviour, TimeInputHandler {

	public void HandleTimedInput () {
		SceneManager.LoadScene ("VR");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		StartCoroutine (LoadDevice ("cardboard"));
		XRSettings.enabled = true;

	}

	IEnumerator LoadDevice (string newDevice) {
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}

}