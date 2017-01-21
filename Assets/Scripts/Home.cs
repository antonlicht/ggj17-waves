﻿using UnityEngine;

public class Home : MonoBehaviour
{
  private void Start()
  {
    var circlePos = Random.insideUnitSphere * 10000;
    circlePos.y = 0;
    transform.position = circlePos;
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log(other.gameObject);
    if (other.gameObject.GetComponentInParent<Player>() != null)
    {
      Debug.Log("you won!");
    }
  }
}