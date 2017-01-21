using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody Rigidbody;
  public Animator Animator;

  public float Speed = 70f;
  public float Threshold = 5f;

  void Update ()
  {
    Move ();
    AdjustRotation ();
  }

  private void Move ()
  {
    Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * Speed;

    Rigidbody.MovePosition(transform.position + ((transform.forward * velocity.y + transform.right * velocity.x) * Time.deltaTime));

    Animator.SetFloat ("Horizontal", velocity.x);
    Animator.SetFloat ("Vertical", velocity.y);
  }

  private void AdjustRotation ()
  {
    float sinRot = Mathf.Sin (transform.position.z * 0.005f) * Mathf.Rad2Deg;
    float cosRot = Mathf.Cos (-transform.position.x * 0.005f) * Mathf.Rad2Deg;
    float linRot = (transform.position.x + transform.position.z + Mathf.Sin (Time.time) * 50) * 0.05f;
    transform.eulerAngles = new Vector3 (0f, sinRot + cosRot + linRot, 0f);
  }
}
