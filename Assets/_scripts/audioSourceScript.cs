using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class audioSourceScript : MonoBehaviour {
	#region Public Variables
	public string obj;
	public AudioSource au;
	#endregion
	#region Protected Variables
	string url, lang;
	#endregion
	#region Main Methods
	void Start () {
		lang = PlayerPrefs.GetString ("Selected_Language");
		url = Application.persistentDataPath + "/Languages/" + lang + "/";
		StartCoroutine (setClip ());

	}
	void OnEnable () {
		StartCoroutine (setClip ());
	}
	#endregion
	#region Utility Methods
	public IEnumerator setClip () {

		if (File.Exists (url + obj + ".mp3")) {
			WWW www = new WWW ("file://" + url + obj + ".mp3");
			yield return www;
			au.clip = www.GetAudioClip ();
			print (url + obj + ".mp3");
			//au.Play ();

		}

	}
	#endregion
}