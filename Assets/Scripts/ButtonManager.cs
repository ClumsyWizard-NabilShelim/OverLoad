using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	public GameObject gameManager;

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GM");
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void SkipTurn()
	{
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
	}
}
