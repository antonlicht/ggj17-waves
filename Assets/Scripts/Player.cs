using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody Rigidbody;
  public Animator Animator;
  public GameObject damageSprite;

  public float Speed = 100f;
  public float RotationSpeed = 20f;

  public float health = 100f;

  public float energy;
  
  public float boostMultiplier = 5f;

  private Vector3 rotation;

  void FixedUpdate ()
  {
    Move ();
    energy = Mathf.Clamp01(energy + Time.deltaTime / 3f);
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
    
    if (Input.GetKey("space") && energy > 0)
    {
      velocity *= boostMultiplier;
      energy -= Time.deltaTime * 2;
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

  private void AdjustRotation ()
  {
    float sinRot = Mathf.Sin (transform.position.z * 0.005f) * Mathf.Rad2Deg;
    float cosRot = Mathf.Cos (-transform.position.x * 0.005f) * Mathf.Rad2Deg;
    float linRot = (transform.position.x + transform.position.z + Mathf.Sin (Time.time) * 50) * 0.05f;
    rotation = new Vector3 (0f, sinRot + cosRot + linRot, 0f);
  }
}
