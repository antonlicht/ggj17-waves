using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody Rigidbody;
  public Animator Animator;

  public float Speed = 100f;
  public float RotationSpeed = 10f;

  public float boostTimeInSecond;
  
  public float boostMultiplier = 5f;

  private Vector3 rotation;

  void Update ()
  {
    Move ();
//    AdjustRotation ();
  }
  
  private void Move ()
  {
    var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    
    Vector2 velocity = input * Speed;

    var eulerAngles = transform.rotation.eulerAngles;
    eulerAngles.y += Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
    
    if (Input.GetKey("space"))
    {
      velocity *= boostMultiplier;
    }

    Rigidbody.MovePosition(transform.position + ((transform.forward * velocity.y + transform.right * velocity.x) * Time.deltaTime));

    Rigidbody.MoveRotation(Quaternion.Euler(eulerAngles));
    
    if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
    {
      Animator.SetFloat("Horizontal", velocity.x);
      Animator.SetFloat("Vertical", 0);
    }
    else
    {
      Animator.SetFloat ("Vertical", velocity.y);
      Animator.SetFloat("Horizontal", 0);
    }      
  }

  private void AdjustRotation ()
  {
    float sinRot = Mathf.Sin (transform.position.z * 0.005f) * Mathf.Rad2Deg;
    float cosRot = Mathf.Cos (-transform.position.x * 0.005f) * Mathf.Rad2Deg;
    float linRot = (transform.position.x + transform.position.z + Mathf.Sin (Time.time) * 50) * 0.05f;
    rotation = new Vector3 (0f, sinRot + cosRot + linRot, 0f);
  }
}
