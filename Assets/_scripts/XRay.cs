using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRay : MonoBehaviour {
	// public Material[] material;
	public Animator myAnim;
	public Button FadeOutButton, FadeInButton;
	bool FadedIn, FadedOut;
	void Start () {
		myAnim = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Update () {
		if (FadedIn) {
			FadedOut = false;
			myAnim.SetBool ("FadeOut", false);
			myAnim.SetBool ("FadeIn", true);
			FadeInButton.gameObject.SetActive (false);
			FadeOutButton.gameObject.SetActive (true);
		}

		if (FadedOut) {
			FadedIn = false;
			myAnim.SetBool ("FadeIn", false);
			myAnim.SetBool ("FadeOut", true);
			FadeInButton.gameObject.SetActive (true);
			FadeOutButton.gameObject.SetActive (false);
		}
		print (FadedOut + "  " + FadedIn);
	}
	public void FadeIn () {
		FadedIn = true;
		FadedOut = false;
	}
	public void FadeOut () {
		FadedOut = true;
		FadedIn = false;
	}
}