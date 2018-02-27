using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Play : MonoBehaviour {
	public Button button;
	public Sprite s1, s2;
	public AudioSource audioSource;
	void Update () {
		if (audioSource.isPlaying) {
			button.image.sprite = s2;
			
		} else {
			button.image.sprite = s1;
			
		}
	}
	public void Check () {
		if (audioSource.clip != null) {
			if (audioSource.isPlaying) {
				button.image.sprite = s1;
				audioSource.Pause ();
			} else {
				button.image.sprite = s2;
				audioSource.Play ();
			}
		}
	}

}