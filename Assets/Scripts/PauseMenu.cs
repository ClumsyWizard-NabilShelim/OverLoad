using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public Animator ShipPauseMenu;

	public static bool isPause = false;
	public static bool PauseV = false;
	bool canpause = true;
	bool isPlay = false;
	bool isMainMenu;
    // Start is called before the first frame update
    void Start()
    {
		isPause = false;
		canpause = true;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && PauseV == true)
		{
			isPause = !isPause;
		}

		if (isPause && canpause == true)
		{
			Pause();
		}

		if (isPlay)
		{
			StartCoroutine(PlayMethod());
		}

		if (isMainMenu)
		{
			StartCoroutine(MainMenuMethod());
		}
    }

	IEnumerator PlayMethod()
	{
		ShipPauseMenu.SetBool("Pause", false);
		ShipPauseMenu.SetBool("Play", true);
		yield return new WaitForSeconds(1f);
		isPause = false;
		isPlay = false;
		canpause = true;

	}
	void Pause()
	{
		ShipPauseMenu.SetBool("Play", false);
		ShipPauseMenu.SetBool("Pause", true);
		canpause = false;
	}

	public void Play()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		isPlay = true;
	}

	public void MainMenu()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		isMainMenu = true;
	}
	IEnumerator MainMenuMethod()
	{
		Camera.main.transform.parent.GetComponent<Animator>().SetTrigger("MainMenuOUT");
		yield return new WaitForSeconds(4f);
		MenuManager.CanFade = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void quit()
	{
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		Application.Quit();
	}
}
