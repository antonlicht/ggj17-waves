using System;
using System.Collections.Generic;
using DG.Tweening;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
  public Transform player;
  public Transform home;

  public Image[] images; // 0 is top, clockwise till  3 left

  private float t;

  void Start()
  {
    foreach (var image in images)
    {
      image.gameObject.SetActive(true);
      image.SetAlpha(0);
    }
  }
  
  void Update()
  {
    t += Time.deltaTime;
    if (t > 4  )
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
    float xDiff = home.position.x - player.position.x;
    float zDiff = home.position.z - player.position.z;
   
    var angle = Mathf.Atan(xDiff / zDiff) * 180 / Mathf.PI;
    
    // tangent only returns an angle from -90 to +90.  we need to check if its behind us and adjust.
    if (zDiff < 0)
    {
      if (xDiff >= 0)
        angle += 180f;
      else
        angle -= 180f;
    }
     
    float playerAngle = player.eulerAngles.y;
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