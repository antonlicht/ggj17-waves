using UnityEngine;

public class Home : MonoBehaviour
{
  public UIController uiController;
  public Wandering wandering;
  public Animator animator;

  private float pauseTime;

  private void Start ()
  {
    var circlePos = Random.insideUnitSphere * 10000;
    circlePos.y = 0;
    transform.position = circlePos;

    pauseTime = Random.Range (2, 10);
  }

  private void Update ()
  {
    pauseTime -= Time.deltaTime;
    if (pauseTime < 0)
    {
      if (wandering.enabled)
      {
        wandering.enabled = false;
        animator.SetBool ("idle", true);
      }
      else if (pauseTime < -1)
      {
        wandering.enabled = true;
        animator.SetBool ("idle", false);
        pauseTime = Random.Range (2, 10);
      }
    }


  }

  private void OnTriggerEnter (Collider other)
  {
    if (other.gameObject.GetComponentInParent<Player> () != null && other.name != "CatSpawnRadius")
    {
      uiController.ShowWonScreen ();
    }
  }
}