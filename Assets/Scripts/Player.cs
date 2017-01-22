using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody Rigidbody;
  public Animator Animator;
  public GameObject damageSprite;

  public float Speed = 100f;
  public float RotationSpeed = 10f;

  public float health = 100f;

  public float boostTimeInSecond;
  
  public float boostMultiplier = 5f;


  void FixedUpdate ()
  {
    Move ();
  }

  void ApplyDamage(float damage) {
	health -= damage;
	if (health < 0f) health = 0f;		
	Animator.SetBool ("Dead", IsDead());
	damageSprite.SetActive (!IsDead());
  }

  private bool IsDead() { return health <= 0f; }
  
  private void Move ()
  {
    var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    
    Vector2 velocity = input * Speed;
    
    if (Input.GetKey("space"))
    {
      velocity *= boostMultiplier;
    }

	if (IsDead()) { velocity = velocity * 0f; }
				
    Rigidbody.MovePosition(transform.position + ((transform.forward * velocity.y + transform.right * velocity.x) * Time.deltaTime));

    
    var eulerAngles = transform.rotation.eulerAngles;
    eulerAngles.y += Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
    
    if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
    {
//      eulerAngles.y += Input.GetAxis("Vertical") * RotationSpeed * Time.deltaTime;
      
      Animator.SetFloat("Horizontal", velocity.x);
      Animator.SetFloat("Vertical", 0);
    }
    else
    {
      
      Animator.SetFloat ("Vertical", velocity.y);
      Animator.SetFloat("Horizontal", 0);
    }
    
    Rigidbody.MoveRotation(Quaternion.Euler(eulerAngles));
  }

}
