using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySounds : MonoBehaviour {
	public AudioSource au;
	public Button playButton, pauseButoon;
	void Start () {
		au = GetComponent<AudioSource> ();
	}
	public void Play () {
		au.Play ();
		playButton.gameObject.SetActive (false);
		pauseButoon.gameObject.SetActive (true);
	}
	public void Pause () {
		au.Pause ();
		playButton.gameObject.SetActive (true);
		pauseButoon.gameObject.SetActive (false);
	}
}