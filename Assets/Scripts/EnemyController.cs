using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Collider areaOfEffect;
	public Collider areaOfDamage;
	public string targetTag;
	public float speed;

	private GameObject _chaseTarget = null;
	private Rigidbody _body = null;

	void Start () {
		_chaseTarget = null;
		_body = GetComponent<Rigidbody>();
		areaOfEffect.isTrigger = true;
		areaOfDamage.isTrigger = false;
	}

	void OnTriggerEnter(Collider other) {
		if (_chaseTarget)
			return;
		if (!other.CompareTag(targetTag))
			return;

		_chaseTarget = other.gameObject;
	}

	void OnTriggerStay(Collider other) {
		OnTriggerEnter (other);
	}

	void OnTriggerExit(Collider other) {
		if (_chaseTarget == other.gameObject) {
			_chaseTarget = null;
		}
	}

	void FixedUpdate () {
		if (_chaseTarget) {			
			Vector3 targetPos = _chaseTarget.transform.position;
			Vector3 selfPos = this.transform.position;
			Vector3 dir = targetPos - selfPos;
			RaycastHit hit;

			bool wasHit = Physics.Raycast (selfPos, dir.normalized, out hit);
			if (wasHit && hit.collider.gameObject != _chaseTarget) {
				_chaseTarget = null;
				return;
			}

			_body.MovePosition (Vector3.MoveTowards (selfPos, targetPos, speed * Time.deltaTime));
		}
	}
}
