using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOxygen : MonoBehaviour {

	// Use this for initialization
	public GameObject BubbleSpawn1, BubbleSpawn2, BubbleSpawn3, Co2 , ThirdCamera;
	public ParticleSystem ps;
	void Start () { }

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter (Collider other) {
		if (other.name == "BubbleSpawn1") {
			BubbleSpawn1.SetActive (true);
			BubblesMove.Bu1 = true;
		} else if (other.name == "BubbleSpawn2") {
			BubbleSpawn2.SetActive (true);
			Destroy (BubbleSpawn1);
			BubblesMove.Bu1 = false;
		} else if (other.name == "BubbleSpawn3") {
			BubbleSpawn3.SetActive (true);
			Destroy (BubbleSpawn2);
			BubblesMove.Bu1 = false;
		} else if (other.name == "Co2Spawn") {
			ps.gameObject.SetActive (true);
		} 
	}
}