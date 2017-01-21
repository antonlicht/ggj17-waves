using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour {

	public Collider bounds;
	public float wanderSpeed;
	public float maxWanderDist;

	private Vector3 _wanderTarget;
	private Vector3 _prevPos;
	private Rigidbody _body = null;
	private float _approxSize = 1.0f;

	void Start () {
		_wanderTarget = this.transform.position;
		_prevPos = _wanderTarget;
		_body = GetComponent<Rigidbody>();
		_approxSize = bounds.bounds.extents.magnitude;
	}

	void OnDisable() {
		_wanderTarget = _body.transform.position;
	}

	void FixedUpdate () {
		Vector3 selfPos = _body.transform.position;
		_wanderTarget.y = selfPos.y;
		if (Vector3.Distance(_wanderTarget, _prevPos) <= _approxSize) {
			_wanderTarget = PickDirection ();
		}				

		_prevPos = selfPos;
		_body.MovePosition (Vector3.MoveTowards (selfPos, _wanderTarget, wanderSpeed * Time.deltaTime));
	}
		

	private Vector3 PickDirection() {
		Vector3 selfPos = _body.transform.position;
		float r1 = Random.Range (-maxWanderDist, maxWanderDist);
		float r2 = Random.Range (-maxWanderDist, maxWanderDist);
		Vector3 dir = new Vector3(r1, 0.0f, r2).normalized;
		RaycastHit hit;

		for (int i = 0; i < 1000; i++) {
			if (Physics.Raycast (selfPos, dir, out hit, maxWanderDist)) {
				if (Vector3.Distance (hit.point, selfPos) > _approxSize * 4.0f) {
					return hit.point;
				} else {
					Vector3 temp = Vector3.Cross(hit.normal, dir);
					dir = Vector3.Cross(temp, hit.normal);
				}
			} else {
				return selfPos + maxWanderDist * dir;
			}
		}

		return selfPos;
	}
}
