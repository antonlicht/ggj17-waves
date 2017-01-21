using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kbmove : MonoBehaviour {

	public float speed = 0.4f;

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		this.transform.position += speed * Time.deltaTime * movement;
	}
}
