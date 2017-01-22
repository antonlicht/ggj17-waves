using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
  public Slider bar;

  public Player player;

  private float lastValue;
  private void Update()
  {
    bar.value = player.energy;
  }
}