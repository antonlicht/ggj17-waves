using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  public PointerListener TouchInput;
  public float Speed = 70f;
  public float Threshold = 5f;

  private Vector2 velocity;

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
    Move ();
    AdjustRotation ();
  }

  private void Move ()
  {
    transform.localPosition += (transform.forward * velocity.y + transform.right * velocity.x) * Time.deltaTime;
  }

  private void AdjustRotation ()
  {
    transform.eulerAngles = new Vector3 (0f, Mathf.Sin (transform.position.y * 0.05f) * Mathf.Rad2Deg + Mathf.Cos (transform.position.x * 0.05f) * Mathf.Rad2Deg, 0f);
  }


  private void AddListeners ()
  {
    TouchInput.onDrag.AddListener (HandleDrag);
    TouchInput.onEndDrag.AddListener (HandleEndDrag);
  }

  public void HandleDrag (PointerEventData eventData)
  {
    Vector2 delta = eventData.position - eventData.pressPosition;

    if (Mathf.Abs (delta.y) > Threshold || Mathf.Abs (delta.x) > Threshold)
    {
      velocity = (Mathf.Abs (delta.y) > Mathf.Abs (delta.x) ? new Vector2 (0f, Mathf.Sign (delta.y)) : new Vector2 (Mathf.Sign (delta.x), 0f)) * Speed;
    }
    else
    {
      velocity = Vector2.zero;
    }

  }

  private void HandleEndDrag (PointerEventData eventData)
  {
    velocity = Vector3.zero;
  }

}
