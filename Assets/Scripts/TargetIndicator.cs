using System;
using System.Collections.Generic;
using DG.Tweening;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
  public Camera camera;
  public Transform player;
  public Transform home;

  Image[] images; // 0 is top, clockwise till  3 left

  private float t;

  void Start()
  {
    images = GetComponentsInChildren<Image>(includeInactive: true);
    
    foreach (var image in images)
    {
      image.gameObject.SetActive(true);
      image.SetAlpha(0);
    }
  }
  
  void Update()
  {
    t += Time.deltaTime;
    if (t > 2)
    {
      t = 0;
      FlashIndicator();
    }
  }

  private void FlashIndicator()
  {
    var angle = GetAngle();
    Image image = null;
    if (angle > -45 && angle < 45)
    {
      image = images[0];
    }
    if (angle > 45 && angle < 135)
    {
      image = images[1];
    }
    if ((angle > 135 && angle < 180) || (angle > -180 && angle < -135))
    {
      image = images[2];
    }
    if (angle > -135 && angle < -45)
    {
      image = images[3];
    }
    if (image)
    {
      image.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);      
    }
  }
  
  float GetAngle()
  {
    float angle;
    float xDiff = home.position.x - player.position.x;
    float zDiff = home.position.z - player.position.z;
   
    angle = Mathf.Atan(xDiff / zDiff) * 180 / Mathf.PI;

    // tangent only returns an angle from -90 to +90.  we need to check if its behind us and adjust.
    if (zDiff < 0)
    {
      angle += zDiff >= 0 ? 180f : -180f;
    }
 
    // this is our angle of rotation from 0->360
    float playerAngle = player.eulerAngles.y;
    if (playerAngle > 180f) playerAngle = 360f - playerAngle;
 
    
    angle -= playerAngle;
 
    // Make sure we didn't rotate past 180 in either direction
    if (angle < -180f) angle += 360;
    else if (angle > 180f) angle -= 360;
 
    return angle;
  }
}