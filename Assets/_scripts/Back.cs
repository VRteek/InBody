using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Back : MonoBehaviour {

	#region Main Methods
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			string name = SceneManager.GetActiveScene ().name;
			if (name == "Menu")
				// Android close icon or back button tapped.
				Application.Quit ();

			if (name == "AR" || name == "VR") {
				SceneManager.LoadScene ("Menu");
			}
		}
	}
	#endregion

}