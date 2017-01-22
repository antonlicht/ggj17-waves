using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	private Vector3 _oldPos;
	public SpriteRenderer flipSprite;

	void Start () {
		_oldPos = this.transform.position;
	}

	void Update () {
		Camera camera = Camera.main;
		transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
			camera.transform.rotation * Vector3.up);
	}

  private float count;

	void FixedUpdate() {
		Vector3 curPos = this.transform.position;
		Vector3 dir = (curPos - _oldPos);


    if (flipSprite)
    {
      dir = this.transform.InverseTransformDirection (dir);
      count += Mathf.Sign (dir.x);

      if (Mathf.Abs(count) > 5)
      {
        count = 0;
        flipSprite.flipX = dir.x > 0;
      }
		}

		_oldPos = curPos;
	}
}
