using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WatchHealth : MonoBehaviour {

	public Slider slider;
	public Player player;

	void Update () {
		slider.value = player.health;
	}
}
