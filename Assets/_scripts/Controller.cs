using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public GameObject FirstPhase, SecondPhase, ThirdPhase, ForthPhase, FifthPhase, MainCam;
	public AudioSource au;
	string url, Language;
	string index;

	IEnumerator cr;
	Texture[] texture;
	void Start () {

		Language = Menu.i;
		print (Language);
		StartCoroutine (StartPhases ());

	}

	IEnumerator getVoiceOver (string url) {
		if (File.Exists (url)) {
			WWW www = new WWW ("file://" + url);
			yield return www;
			au.clip = www.GetAudioClip ();
			au.Play ();
		}

	}
	IEnumerator StartPhases () {
		index = "1";
		url = Application.persistentDataPath + "/Languages/" + Language + "/" + index + ".mp3";
		StartCoroutine (getVoiceOver (url));

		yield return new WaitForSeconds (20f);
		index = "2";
		url = Application.persistentDataPath + "/Languages/" + Language + "/" + index + ".mp3";
		StartCoroutine (getVoiceOver (url));
		SecondPhase.SetActive (true);
		Destroy (FirstPhase);

		yield return new WaitForSeconds (25f);
		index = "3";
		url = Application.persistentDataPath + "/Languages/" + Language + "/" + index + ".mp3";
		StartCoroutine (getVoiceOver (url));
		ThirdPhase.SetActive (true);
		Destroy (SecondPhase);

		yield return new WaitForSeconds (20);
		index = "4";
		url = Application.persistentDataPath + "/Languages/" + Language + "/" + index + ".mp3";
		StartCoroutine (getVoiceOver (url));
		ForthPhase.SetActive (true);
		Destroy (ThirdPhase);

		yield return new WaitForSeconds (14);
		index = "5";
		url = Application.persistentDataPath + "/Languages/" + Language + "/" + index + ".mp3";
		StartCoroutine (getVoiceOver (url));
		FifthPhase.SetActive (true);
		Destroy (ForthPhase);
	}

}