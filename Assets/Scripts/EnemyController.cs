using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Collider areaOfEffect;
	public Collider areaOfDamage;
	public GameObject wanderDebug;
	public string targetTag;
	public float chaseSpeed;
	public float wanderSpeed;
	public float maxWanderDist;

	private GameObject _chaseTarget = null;
	private Vector3 _wanderTarget;
	private Rigidbody _body = null;

	void Start () {
		_chaseTarget = null;
		_wanderTarget = this.transform.position;
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
	}

	void OnTriggerStay(Collider other) {
		OnTriggerEnter (other);
	}

	void OnTriggerExit(Collider other) {
		if (_chaseTarget == other.gameObject) {
			_chaseTarget = null;
		}
	}

	bool CanChase(GameObject target) {
		Vector3 selfPos = this.transform.position;
		Vector3 dir = target.transform.position - selfPos;
		RaycastHit hit;

		bool wasHit = Physics.Raycast (selfPos, dir, out hit);
		if (wasHit && hit.collider.gameObject == target) {
			return true;
		}				

		return false;
	}

	void FixedUpdate () {
		Vector3 selfPos = this.transform.position;

		if (_chaseTarget && CanChase(_chaseTarget)) {
			_wanderTarget = selfPos; // reset the wander target

			Vector3 targetPos = _chaseTarget.transform.position;
			_body.MovePosition (Vector3.MoveTowards (selfPos, targetPos, chaseSpeed * Time.deltaTime));
		} 
		else {
			_chaseTarget = null;
			_wanderTarget.y = selfPos.y;
			if (Vector3.Distance(_wanderTarget, areaOfDamage.ClosestPointOnBounds(_wanderTarget)) <= 0.01f) {
				float r1 = Random.Range (-maxWanderDist, maxWanderDist);
				float r2 = Random.Range (-maxWanderDist, maxWanderDist);
				Vector3 randomDir = new Vector3(r1, 0.0f, r2).normalized;
				RaycastHit hit;
				if (Physics.Raycast (selfPos, randomDir, out hit, maxWanderDist)) {
					_wanderTarget = hit.point;
				} else {
					_wanderTarget = selfPos + maxWanderDist * randomDir;
				}

				if (wanderDebug) {
					wanderDebug.transform.position = _wanderTarget;
				}
			}				

			_body.MovePosition (Vector3.MoveTowards (selfPos, _wanderTarget, wanderSpeed * Time.deltaTime));
		}
			
	}
}
