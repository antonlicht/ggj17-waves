using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  public Player Player;
  public Slider Slider;
  public GameObject WonScreen;
  public GameObject LostScreen;

  public Button[] ReloadButtons;

  void Awake ()
  {
    foreach (Button button in ReloadButtons)
    {
      button.onClick.AddListener (() => SceneManager.LoadScene (1));
    }
  }

  void Update ()
  {
    Slider.value = Player.health;
    if (Player.health <= 0)
    {
      ShowLostScreen ();
    }
  }

  private void UntagPlayer ()
  {
    foreach (Collider collider in Player.GetComponentsInChildren<Collider> ())
    {
      if (collider.tag == "player")
      {
        collider.tag = "Untagged";
        collider.enabled = false;
        collider.enabled = true;
      }
    }
  }

  public void ShowLostScreen ()
  {
    UntagPlayer ();
    LostScreen.SetActive (true);
  }

  public void ShowWonScreen ()
  {
    UntagPlayer ();
    WonScreen.SetActive (true);
  }
}
