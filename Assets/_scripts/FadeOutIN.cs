using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutIN : MonoBehaviour {

	public Animator Myroot;
	public float FadeDuration = 2;
	private Renderer FadeInOutQuad;
	AnimatorClipInfo[] info;
	void Start () {

		info = Myroot.GetCurrentAnimatorClipInfo (0);

	}

	IEnumerator FadeInOut (float to, float Duration) {
		float tt = 0;
		Material TargetMat = FadeInOutQuad.sharedMaterial;
		TargetMat.color = Color.black;
		while (tt < Duration) {
			tt += Time.deltaTime;
			TargetMat.color = new Color (TargetMat.color.r, TargetMat.color.g, TargetMat.color.b, Mathf.Lerp (1, 0, tt / Duration));
			yield return null;
		}

		if (gameObject.name == "QuadSecond") {
			yield return new WaitForSeconds (info[0].clip.length * 2);
		} else {
			yield return new WaitForSeconds (info[0].clip.length);
		}
		tt = 0;
		while (tt < Duration) {
			tt += Time.deltaTime / Duration;
			if (gameObject.name != "FifthQuad") {
				TargetMat.color = new Color (TargetMat.color.r, TargetMat.color.g,
				TargetMat.color.b, Mathf.Lerp (0, 1, tt / Duration));
			}
			yield return null;
		}
	}

	void OnEnable () {
		FadeInOutQuad = GetComponent<Renderer> ();
		StartCoroutine (FadeInOut (0, FadeDuration));
	}
}