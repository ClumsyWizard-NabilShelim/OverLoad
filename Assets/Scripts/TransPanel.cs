using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransPanel : MonoBehaviour
{
	public static bool TransitionOpen;
    public void ReloadScene()
	{
		if (TransitionOpen)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	
	}
}
