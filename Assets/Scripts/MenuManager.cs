using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameObject FadeCanvas;
	public static MenuManager instance;
	public static bool CloseMenu = false;
	public static bool CanPlay = false;
	public static bool CanFade = true;

	// Start is called before the first frame update
	void Awake()
    {
		DontDestroyOnLoad(gameObject);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}
	void Update()
	{



		if (CanFade)
		{
			FadeCanvas.SetActive(true);
		}else if (!CanFade)
		{
			FadeCanvas.SetActive(false);
		}
		if(CloseMenu == true)
		{
			SceneTransButtons.MenuOpen = false;
		}
		if(CloseMenu == false)
		{
			SceneTransButtons.MenuOpen = true;
		}
		if(CanPlay == true)
		{
			ScenetransManager.StartGame = true;
		}
	}
}
