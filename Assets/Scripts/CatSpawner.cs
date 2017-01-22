using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour {
	public Collider spawnArea;
	public GameObject enemyPrefab;
	public GameObject parent;

	public string spawnPointTag;
	public int maxEnemies = 20;

	private int _enemiesCount = 0;
	private float _enemyHeight = 0.0f;
	private List<GameObject> _inactive;

	void Start () {
		_inactive = new List<GameObject> ();
		spawnArea.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag(spawnPointTag) || _enemiesCount >= maxEnemies)
			return;

		GameObject newEnemy = null;
		if (_inactive.Count > 0) {
			newEnemy = _inactive [0];
			_inactive.RemoveAt (0);
		} else {
			newEnemy = Instantiate(enemyPrefab);

	    newEnemy.transform.SetParent (parent.transform);
		  
		  var localPos = newEnemy.transform.localPosition;
		  localPos.x = Random.Range(-100, 100);
		  localPos.y = Random.Range(-100, 100);
		  newEnemy.transform.localPosition = localPos;
		}
			
		Vector3 respawnPos = other.gameObject.transform.position;
		Vector3 newPos = new Vector3 (respawnPos.x, _enemyHeight, respawnPos.z);
		newEnemy.transform.position = newPos;
		newEnemy.SetActive(true);
				
		_enemiesCount++;
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag(spawnPointTag) && (Random.Range(0.0f, 1.0f) > 0.7f)) {
			OnTriggerEnter (other);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<EnemyController> () == null)
			return;

		other.gameObject.SetActive (false);
		_inactive.Add (other.gameObject);
		_enemiesCount--;
	}		
}
