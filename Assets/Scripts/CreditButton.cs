using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{
   public string SceneName;
   public void Change()
   {
		SceneManager.LoadScene(SceneName);
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
   }
}
