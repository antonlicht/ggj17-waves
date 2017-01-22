﻿using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
  public AudioSource AudioSource;
  public void Trigger()
  {
    if (AudioSource)
    {
      AudioSource.Play();
    }
  }
}
