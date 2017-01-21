using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  public PointerListener TouchInput;
  public float Speed = 70f;
  public float Threshold = 5f;

  private Vector3 velocity;

  void OnEnable ()
  {
    AddListeners ();
  }

  void OnDisable ()
  {
    TouchInput.RemoveAllListeners ();
  }

  void Update ()
  {
    transform.localPosition += velocity * Time.deltaTime;
  }


  private void AddListeners ()
  {
    TouchInput.onDrag.AddListener (HandleDrag);
    TouchInput.onEndDrag.AddListener (HandleEndDrag);
  }

  public void HandleDrag (PointerEventData eventData)
  {
    Vector2 delta = eventData.position - eventData.pressPosition;

    if (Mathf.Abs(delta.y) > Threshold || Mathf.Abs(delta.x) > Threshold)
    {
      velocity = (Mathf.Abs (delta.y) > Mathf.Abs (delta.x) ? transform.forward * Mathf.Sign(delta.y)  : transform.right * Mathf.Sign (delta.x)) * Speed;
    }
    else
    {
      velocity = Vector3.zero;
    }

  }

  private void HandleEndDrag (PointerEventData eventData)
  {
    velocity = Vector3.zero;
  }

}
