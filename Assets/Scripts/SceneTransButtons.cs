using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransButtons : MonoBehaviour
{
	public Animator CAmAnim;
	public Animator transPanel;
	public Animator WinAnim;
	public Animator selectionMenu;
	public bool isRestart;
	public static bool MenuOpen = true;
	public bool ismenu;
	public Animator CamAudioAnim;
	bool press;

	public GameObject SelectionMenu;

	public Animator LocationButton;
	//public static SceneTransButtons instance;

	void Awake()
	{
		SelectionMenu = GameObject.FindGameObjectWithTag("SelectionMenu");
		//DontDestroyOnLoad(gameObject);
		//if(instance == null)
		//{
		//	instance = this;
		//}
		//else
		//{
		//	Destroy(gameObject);
		//	return;
		//}
		
		
	}
	void Start()
	{
		CAmAnim = GameObject.FindGameObjectWithTag("cameraHolder").GetComponent<Animator>();
	}
	void Update()
	{
		if(MenuOpen == false)
		{
			transform.GetChild(0).gameObject.SetActive(false);
		}
		if (isRestart)
		{
			/*StartCoroutine(*/StartRestart()/*)*/;
		}
		if (ismenu)
		{	
			StartCoroutine(AfterWinMenu());
		}
	}

    public void Play()
	{
		if (!press)
		{
			if (GameManager.P1Ready && GameManager.P2Ready)
			{
				press = true;
				if (!GameManager.IsTutorial)
				{
					LocationButton.SetTrigger("OUT");
				}
				selectionMenu.SetTrigger("SelectionMenu_Out");
				FindObjectOfType<AudioManager>().Play("ButtonPress");
				CAmAnim.SetTrigger("MainMenuIN");
				StartCoroutine(CloseMenus());
				CamAudioAnim.SetTrigger("ThemeOut");
				if (ScenetransManager.StartGame == false)
				{
					cursor.cursorGO.SetActive(false);
				}
				else
				{
					cursor.cursorGO.SetActive(true);
				}
				if (GameManager.IsTutorial)
				{
					TutorialSequenceHandeler.PlayClicked = true;
				}
			}

		}

	}

	IEnumerator CloseMenus()
	{
		yield return new WaitForSeconds(5f);
		SelectionMenu.SetActive(false);
	}

	public void SelectTurret()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		selectionMenu.SetTrigger("SelectionMenu_In");
		if (GameManager.IsTutorial)
		{
			TutorialManager.StartTutorial = true;
		}
	}
	//public void MainMenu()
	//{
	//	FindObjectOfType<AudioManager>().Play("ButtonPress");
	//	CAmAnim.SetTrigger("MainMenuOUT");
	//	CamAudioAnim.SetTrigger("ThemeIn");
	//}
	public void Quit()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		Application.Quit();
	}

	void StartRestart()
	{
		MenuManager.CloseMenu = true;
		MenuManager.CanPlay = true;
		MenuManager.CanFade = true;
		if(transPanel == null)
		{
			transPanel = GameObject.FindGameObjectWithTag("TransPanel").GetComponent<Animator>();
		}
		TransPanel.TransitionOpen = true;
		transPanel.SetTrigger("FadeIN");
		FindObjectOfType<AudioManager>().Stop("ShipThrusters");
		//yield return new WaitForSeconds(1f);
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		ismenu = false;
		isRestart = false;
	}
	public void Restart()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		Debug.Log("Work");
		GameOver.Dead = false;
		isRestart = true;
	}

	public void AfterWinMainMenu()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		//CamAudioAnim.SetTrigger("ThemeIn");
		WinAnim.SetTrigger("ThrusterFadeOut");
		ismenu = true;
	}

	IEnumerator AfterWinMenu()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		CAmAnim.SetTrigger("MainMenuOUT");
		MenuManager.CloseMenu = false;
		MenuManager.CanPlay = false;
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene("PVPScene");
	}

	public void Credit()
	{
		//TransPanel.SetTrigger("FadeIN");
		SceneManager.LoadScene("Credits");
	}
}
