using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

  public LayerMask layerMask;
	public Collider areaOfEffect;
	public Collider areaOfDamage;

	public Wandering wandering;

	public Animator headAnimator;
	public Animator walkAnimator;

	public string targetTag;
	public float chaseSpeed;
	public float damage;

  public float damageRange = 10;

	public GameObject chaseTarget = null;
	private Rigidbody _body = null;
	private bool _captured = false;

	void Start () {
		_captured = false;
		chaseTarget = null;
		_body = GetComponent<Rigidbody>();
		areaOfEffect.isTrigger = true;
		areaOfDamage.isTrigger = false;
	}

	void OnTriggerEnter(Collider other) {
		if (chaseTarget)
			return;
		if (!other.CompareTag(targetTag))
			return;
		if (!CanChase(other.gameObject) )
			return;

		chaseTarget = other.gameObject;
		wandering.enabled = false;
	}

	void OnTriggerStay(Collider other) {
		OnTriggerEnter (other);
	}

	void OnTriggerExit(Collider other) {
		if (chaseTarget == other.gameObject)
			chaseTarget = null;

		if (headAnimator) 
			headAnimator.Play ("EnemyHead_scan");
	}

	bool CanChase(GameObject target) {
		Vector3 selfPos = _body.transform.position;
		Vector3 dir = target.transform.position - selfPos;
		RaycastHit hit;

		bool wasHit = Physics.Raycast (selfPos, dir, out hit, 10000, layerMask);
	  if (wasHit)
	  {
	    return hit.collider.gameObject == target;
	  }
    
		return false;
	}

	void FixedUpdate () {
		Vector3 selfPos = _body.transform.position;

		if (chaseTarget)
		{
		  Player player = chaseTarget.gameObject.GetComponentInParent<Player>();
		  if (player != null && (player.transform.position-transform.position).magnitude < damageRange)
		  {
		    player.ApplyDamage(damage * Time.deltaTime);
		    _captured = true;
		  }
		}			

		if (chaseTarget && CanChase(chaseTarget)) {
			wandering.enabled = false;

			Vector3 targetPos = chaseTarget.transform.position;
			_body.MovePosition (Vector3.MoveTowards (selfPos, targetPos, chaseSpeed * Time.deltaTime));
		} 
		else {
			chaseTarget = null;
			wandering.enabled = true;
		}
			
		float animationSpeedCoeff = 1.0f;
		if (wandering.enabled)
			animationSpeedCoeff = 0.5f;
		
		if (walkAnimator)
			walkAnimator.SetFloat ("speed", animationSpeedCoeff);

		if (headAnimator) {
			headAnimator.SetBool ("drool", !(wandering.enabled || _captured));
			headAnimator.SetBool ("attack", _captured);
		}

	  _captured = false;
	}
}
