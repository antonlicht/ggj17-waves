using DG.Tweening;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
  public Player player;
  public Home home;

  public Image[] images; // 0 is top, clockwise till  3 left

  private float t;

  void Start()
  {
    foreach (var image in images)
    {
      image.gameObject.SetActive(true);
      image.SetAlpha(0);
    }
  }
  
  void Update()
  {
    t += Time.deltaTime;
    if (t > 4)
    {
      t = 0;
      FlashIndicator();
    }
  }

  private void FlashIndicator()
  {
    Image image;
    var distance = player.transform.position - home.transform.position;
    Debug.Log(distance);
    if (Mathf.Abs(distance.x) > Mathf.Abs(distance.z))
    {
      //left/right
      image = distance.x > 0 ? images[1] : images[3];
    }
    else
    {
      //up/down
      image = distance.y > 0 ? images[2] : images[0];
    }
    image.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);
  }
}