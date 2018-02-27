using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Co2Move : MonoBehaviour {
	ParticleSystem Co2Particle;

	// Use this for initialization
	void Start () {
		Co2Particle = GetComponent<ParticleSystem> ();
		StartCoroutine (wait ());
	}

	// Update is called once per frame
	void Update () {

	}
	IEnumerator wait () {
		yield return new WaitForSeconds (6);
		var col = Co2Particle.collision;
		col.enabled = false;
	}
}