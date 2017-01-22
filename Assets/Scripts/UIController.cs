using UnityEngine;

using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  public Player Player;
  public Slider Slider;
  public GameObject WonScreen;
  public GameObject LostScreen;

	void Update ()
  {
		Slider.value = Player.health;
	}

  public void ShowWonScreen()
  {
    WonScreen.SetActive(true);
  }
}
