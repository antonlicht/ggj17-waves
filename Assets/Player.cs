using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  public PointerListener TouchInput;
  public float MinSpeed = 10f;
  public float SpeedThreshold = 2f;
  public float MaxSpeed = 30f;

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
    Vector3 newVelocity = (Mathf.Abs(delta.y) > Mathf.Abs(delta.x) ? transform.up * delta.y : transform.right * delta.x);

    if ((newVelocity.magnitude < MinSpeed - SpeedThreshold && velocity.magnitude > 0) || (newVelocity.magnitude < MinSpeed + SpeedThreshold && velocity.magnitude == 0))
    {
      velocity = Vector3.zero;
    }
    else
    {
      if (newVelocity.magnitude > MaxSpeed)
      {
        newVelocity = newVelocity.normalized * MaxSpeed;
      }

      velocity = newVelocity;
    }
  }

  private void HandleEndDrag (PointerEventData eventData)
  {
    velocity = Vector3.zero;
  }

}
