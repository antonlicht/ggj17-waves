using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  public PointerListener TouchInput;
  public Animator Animator;

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
    if (velocity.magnitude > 1)
    {
      transform.localPosition += (transform.forward * velocity.y + transform.right * velocity.x) * Time.deltaTime;
    }

    Animator.SetFloat("Horizontal", velocity.x);
    Animator.SetFloat("Vertical", velocity.y);
  }

  private void AdjustRotation ()
  {
    float sinRot = Mathf.Sin(transform.position.z*0.005f)*Mathf.Rad2Deg;
    float cosRot = Mathf.Cos (-transform.position.x * 0.005f) * Mathf.Rad2Deg;
    float linRot = (transform.position.x + transform.position.z)*0.05f;
    transform.eulerAngles = new Vector3 (0f, sinRot + cosRot + linRot , 0f);
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
      velocity = velocity.normalized;
    }

  }

  private void HandleEndDrag (PointerEventData eventData)
  {
    velocity = velocity.normalized;
  }

}
