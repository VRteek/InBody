using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {
	public GameObject cam;
	// Use this for initialization
	void Start () { }

	// Update is called once per frame
	void Update () { }
	void OnTriggerEnter (Collider other) {
		if (other.name == "FadeFinish") {
			print (other.name);
		}
	}
}