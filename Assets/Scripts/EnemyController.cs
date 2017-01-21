using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Collider2D areaOfEffect;
	public Collider2D areaOfDamage;
	public string targetTag;
	public float speed;

	private GameObject _chaseTarget = null;
	private Rigidbody2D _body = null;

	void Start () {
		_chaseTarget = null;
		_body = GetComponent<Rigidbody2D>();
		areaOfEffect.isTrigger = true;
		areaOfDamage.isTrigger = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (_chaseTarget)
			return;
		if (!other.CompareTag(targetTag))
			return;

		_chaseTarget = other.gameObject;
	}

	void OnTriggerStay2D(Collider2D other) {
		OnTriggerEnter2D (other);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (_chaseTarget == other.gameObject) {
			_chaseTarget = null;
		}
	}

	void FixedUpdate () {
		if (_chaseTarget) {			
			Vector3 targetPos = _chaseTarget.transform.position;
			Vector3 selfPos = this.transform.position;			
			_body.MovePosition (Vector3.MoveTowards (selfPos, targetPos, speed * Time.deltaTime));
		}
	}
}
