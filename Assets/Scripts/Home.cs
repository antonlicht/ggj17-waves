using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
  private void Start()
  {
    var circlePos = Random.insideUnitSphere * 5000;
    circlePos.y = 0;
    transform.position = circlePos;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.GetComponentInParent<Player>() != null)
    {
      Debug.Log("you won!");
//      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}