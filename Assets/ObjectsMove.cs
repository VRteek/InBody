using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMove : MonoBehaviour {

	public GameObject[] rbcs;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < rbcs.Length; i++) {
			rbcs[i].GetComponent<Animator> ();
		}
	}

	// Update is called once per frame
	void Update () {

	}
}