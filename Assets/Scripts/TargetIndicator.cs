using System.Collections;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
  public Player player;
  public Home home;

  public RectTransform incomingSignal;
  
  public RectTransform outgoingSignal;
  
  private float offset;

  public float showSignalDuration = 1f;
  
  private float t;

  private Camera cam;

  private Vector2 playerOnScreen;

  private Coroutine signalling;
  
  void Start()
  {
    offset = 128 - (128 / (Screen.width / (float) Screen.height));
    incomingSignal.gameObject.SetActive(false);
    outgoingSignal.gameObject.SetActive(false);
    
    cam = Camera.main;
    
    playerOnScreen = RectTransformUtility.WorldToScreenPoint(cam, player.transform.position);
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

    home.TuneUpDaRadiooo(true);
    home.TriggerCat();

    while (t > Time.time)
    {
      AdjustCatSignalPosition();
      yield return null;
    }
    home.TuneUpDaRadiooo (false);

    incomingSignal.gameObject.SetActive(false);

    signalling = null;
  }

  private void AdjustCatSignalPosition()
  {
    var homeOnScreen = RectTransformUtility.WorldToScreenPoint(cam, home.transform.position);        
    
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