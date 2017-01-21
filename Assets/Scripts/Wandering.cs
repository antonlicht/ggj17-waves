using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour {

	public Collider collider;
	public float wanderSpeed;
	public float maxWanderDist;
	private Vector3 _wanderTarget;
	private Rigidbody _body = null;

	void Start () {
		_wanderTarget = this.transform.position;
		_body = GetComponent<Rigidbody>();
	}

	void OnDisable() {
		_wanderTarget = _body.transform.position;
	}

	void FixedUpdate () {
		Vector3 selfPos = _body.transform.position;
		_wanderTarget.y = selfPos.y;
		if (Vector3.Distance(_wanderTarget, collider.ClosestPointOnBounds(_wanderTarget)) <= 0.01f) {
			float r1 = Random.Range (-maxWanderDist, maxWanderDist);
			float r2 = Random.Range (-maxWanderDist, maxWanderDist);
			Vector3 randomDir = new Vector3(r1, 0.0f, r2).normalized;
			RaycastHit hit;
			if (Physics.Raycast (selfPos, randomDir, out hit, maxWanderDist)) {
				_wanderTarget = hit.point;
			} else {
				_wanderTarget = selfPos + maxWanderDist * randomDir;
			}
		}				

		_body.MovePosition (Vector3.MoveTowards (selfPos, _wanderTarget, wanderSpeed * Time.deltaTime));
	}
}
