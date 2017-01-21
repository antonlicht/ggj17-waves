using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kbmove : MonoBehaviour {

	public float speed = 0.4f;

	private Rigidbody _body;

	void Start() {
		_body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 dir = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		_body.MovePosition(this.transform.position + speed * Time.deltaTime * dir);
	}
}
