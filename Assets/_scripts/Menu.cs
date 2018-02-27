using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using Vuforia;

public class Menu : MonoBehaviour {
	public static string SceneName;
	public Button button;
	public GameObject Camera;
	public static string i;
	// Use this for initialization
	void Awake () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Camera.GetComponent<VuforiaBehaviour> ().enabled = false;

		i = PlayerPrefs.GetString ("Selected_Language");
		print (i);
		if (i != "") {

			button.GetComponent<UnityEngine.UI.Image> ().sprite = Resources.Load ("flags/" + i, typeof (Sprite)) as Sprite;
		} else {
			SceneManager.LoadScene ("JSON_Download");
		}
	}

	// Update is called once per frame
	void Update () {

	}
	public void LoadScne (string s) {
		SceneManager.LoadScene (s);
		SceneName = s;

	}

	public void ChooseLanguage () {
		SceneManager.LoadScene ("JSON_Download");
	}
}