using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Collider areaOfEffect;
	public Collider areaOfDamage;

	public Wandering wandering;

	public string targetTag;
	public float chaseSpeed;
	public float damage;

	private GameObject _chaseTarget = null;
	private Rigidbody _body = null;
	private bool _captured = false;

	void Start () {
		_captured = false;
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
		if (!CanChase(other.gameObject) )
			return;

		_chaseTarget = other.gameObject;
		wandering.enabled = false;
	}

	void OnTriggerStay(Collider other) {
		OnTriggerEnter (other);
	}

	void OnTriggerExit(Collider other) {
		if (_chaseTarget == other.gameObject) {
			_chaseTarget = null;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject == _chaseTarget) {
			_captured = true;
		}
	}

	void OnCollisionStay(Collision collision) {
		OnCollisionEnter (collision);
	}

	void OnCollisionExit(Collision collision) {
		if (collision.collider.gameObject == _chaseTarget) {
			_captured = false;
		}
	}

	bool CanChase(GameObject target) {
		Vector3 selfPos = _body.transform.position;
		Vector3 dir = target.transform.position - selfPos;
		RaycastHit hit;

		bool wasHit = Physics.Raycast (selfPos, dir, out hit);
		if (wasHit && hit.collider.gameObject == target) {
			return true;
		}				

		return false;
	}

	void FixedUpdate () {
		Vector3 selfPos = _body.transform.position;

		if (_captured) {
		//	_chaseTarget.SendMessage ("ApplyDamage", damage * Time.deltaTime);
		}

		if (_chaseTarget && CanChase(_chaseTarget)) {
			wandering.enabled = false;

			Vector3 targetPos = _chaseTarget.transform.position;
			_body.MovePosition (Vector3.MoveTowards (selfPos, targetPos, chaseSpeed * Time.deltaTime));
		} 
		else {
			_chaseTarget = null;
			wandering.enabled = true;
		}
			
	}
}
