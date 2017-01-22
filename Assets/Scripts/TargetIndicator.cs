using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
  public Player player;
  public Transform home;

  public RectTransform incomingSignal;
  
  public RectTransform outgoingSignal;
  
  private float offset;

  public float showSignalDuration = 0.5f;
  
  private float t;

  private Camera camera;

  private Vector2 playerOnScreen;

  private Coroutine signalling;
  
  void Start()
  {
    offset = 128 - (128 / (Screen.width / (float) Screen.height));
    incomingSignal.gameObject.SetActive(false);
    outgoingSignal.gameObject.SetActive(false);
    
    camera = Camera.main;
    
    playerOnScreen = RectTransformUtility.WorldToScreenPoint(camera, player.transform.position);
    playerOnScreen.y -= offset;

    outgoingSignal.anchoredPosition = playerOnScreen;
  }
  
  void Update()
  {    
    if (Input.GetKey(KeyCode.LeftShift) && player.energy >= 1 && signalling == null)
    {
      player.energy -= 1;
      signalling = StartCoroutine(SignalRoutine());
    }
  }

  private IEnumerator SignalRoutine()
  {
    outgoingSignal.gameObject.SetActive(true);
    
    yield return new WaitForSeconds(1);
    
    outgoingSignal.gameObject.SetActive(false);
    
    incomingSignal.gameObject.SetActive(true);
    
    var t = Time.time + showSignalDuration;
    while (t > Time.time)
    {
      AdjustCatSignalPosition();
      yield return null;
    }
    
    incomingSignal.gameObject.SetActive(false);

    signalling = null;
  }

  private void AdjustCatSignalPosition()
  {
    var homeOnScreen = RectTransformUtility.WorldToScreenPoint(camera, home.transform.position);        
    
    homeOnScreen.y -= offset;
    
    var distance = homeOnScreen - playerOnScreen;

    if (distance.magnitude > 160)
    {
      distance = distance.normalized * 160;
    }

    var arrowPosition = playerOnScreen + distance;
    
    incomingSignal.anchoredPosition = arrowPosition;
  }
}