using System.Collections;
using ArabicSupport;
using UnityEngine;
using UnityEngine.UI;

public class SetArabicTextExample : MonoBehaviour {

	public string text;

	void Start () {

		//gameObject.GetComponent<Text>().text = ArabicFixer.Fix (text, true, true);
		// foreach (char c in text) {

		// 	string character = c + "";
		gameObject.GetComponent<Text> ().text = ArabicFixer.Fix (text, true, true);

		// yield return new WaitForSeconds (0.125f);

	}

}