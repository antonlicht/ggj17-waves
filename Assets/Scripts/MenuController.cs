using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
  public Button Button;
  void Start()
  {
    Button.onClick.AddListener(() =>
    {
      SceneManager.LoadScene(1);
    });    
  }
}
