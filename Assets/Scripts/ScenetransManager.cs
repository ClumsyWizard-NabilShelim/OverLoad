using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenetransManager : MonoBehaviour
{
	//use in TurnSystem 
	public static bool StartGame;
	public static bool CardSet;
	public static bool ChangeRendCanvas;
	GameObject LoCanvas;

	void Awake()
	{
		LoCanvas = Camera.main.transform.GetChild(0).gameObject;
	}
	public void StartGAME()
	{
		StartGame = true;
		cursor.cursorGO.SetActive(true);
		CardSelect.canGrown = true;
	}

	public void StopGAME()
	{
		StartGame = false;
	}

	public void CanPause()
	{
		PauseMenu.PauseV = true;
	}
	public void CantPause()
	{
		PauseMenu.PauseV = false;
	}

	public void CardSetPos()
	{
		CardSet = true;
	}

	public void UnParnet_Canvas()
	{
		Camera.main.transform.GetChild(0).transform.SetParent(null);
	}

	public void SetParnet_Canvas()
	{
		LoCanvas.transform.SetParent(Camera.main.transform);
	}
}
