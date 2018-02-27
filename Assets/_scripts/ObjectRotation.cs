using System.Collections;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {

	public float organSpeedRotation = 1.0f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {

		transform.Rotate (-Vector3.up * Time.deltaTime * organSpeedRotation);
	}
}