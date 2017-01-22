using System;
using System.Collections.Generic;
using DG.Tweening;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
  public Player player;
  public Transform home;
  Transform playerTransform;

  public Image arrow;
  
  private float t;

  void Start()
  {
    arrow.SetAlpha(0);
    playerTransform = player.transform;
  }
  
  void Update()
  {    
    if (Input.GetKey(KeyCode.LeftShift) && player.energy >= 1)
    {
      player.energy -= 1;
      FlashIndicator();
    }
  }

  private void FlashIndicator()
  {
    var angle = GetAngle();
    
    var euler = new Vector3(0, 0, -angle);
    var rotation = Quaternion.Euler(euler);
    arrow.transform.parent.rotation = rotation;    
    arrow.DOFade(1, 0.25f).SetLoops(2, LoopType.Yoyo);
  }
  
  float GetAngle()
  {
    float xDiff = home.position.x - playerTransform.position.x;
    float zDiff = home.position.z - playerTransform.position.z;
   
    var angle = Mathf.Atan(xDiff / zDiff) * 180 / Mathf.PI;
    
    // tangent only returns an angle from -90 to +90.  we need to check if its behind us and adjust.
    if (zDiff < 0)
    {
      if (xDiff >= 0)
        angle += 180f;
      else
        angle -= 180f;
    }
     
    float playerAngle = playerTransform.eulerAngles.y;
    angle -= playerAngle;
    
    while (angle > 180f)
    {
      angle = 360f - angle;
    }

    while (angle < -180f)
    {
      angle = angle + 360f;
    }
    
    return angle;
  }
}