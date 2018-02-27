using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesMove : MonoBehaviour {

	public Transform target;
	public float speed;
	public Animator MyAnim, MyAnimColor;
	public GameObject cell;
	float step;
	public static bool Bu1;
	void Start () {
		StartCoroutine (wait ());

	}
	void Update () {

	}
	// void OnCollisionEnter (Collision other) {
	// 	if (other.transform.tag == "CellTarget") {
	// 		print ("Hatshfet");
	// 		gameObject.transform.SetParent (target.transform.parent);
	// 		// gameObject.transform.position = target.transform.position - new Vector3 (0.01f, 0.01f, 0.01f);
	// 		if (Bu1 == true) {
	// 			gameObject.transform.Rotate (180, 0, 0);
	// 		} else if (Bu1 == false) {
	// 			gameObject.transform.Rotate (0, 180, 0);
	// 		}

	// 		speed = 0;
	// 		MyAnim.SetBool ("Collided", true);
	// 	}
	// }
	void OnTriggerEnter (Collider other) {
		if (other.tag == "CellTarget") {
			target = other.gameObject.transform;
			print ("Hatshfet");
			// speed = 0;
			gameObject.transform.SetParent (other.gameObject.transform);
			if (Bu1 == true) {
				gameObject.transform.Rotate (180, 0, 0);
			} else if (Bu1 == false) {
				gameObject.transform.Rotate (0, 180, 0);
			}
			transform.localPosition = Vector3.zero;
			MyAnim.SetBool ("Collided", true);
		}
		if (gameObject.name == "Bubble3") {
			MyAnimColor.SetBool ("ChangeColor", true);
		}

	}
	IEnumerator wait () {
		yield return new WaitForSeconds (0.5f);
		step = speed * Time.deltaTime;
		while (Vector3.Distance (transform.position, target.position) >= 0.11f) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			yield return null;
		}
	}
}