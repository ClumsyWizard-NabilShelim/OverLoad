using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject P1LselectedGO;
	
	public GameObject P1RselectedGO;
	
	public GameObject P2LselectedGO;

	public GameObject P2RselectedGO;

	public GameObject SkipButton1;
	public GameObject SkipButton2;

	public GameObject SelectedGO;
	public GameObject EmpbreakSelectedGO;

	public Canvas MainHeadCanvas;
    Animator CamAnimator;
	public GameObject P1LeftContainer;
	public GameObject P1RightContainer;
	public GameObject P2LeftContainer;
	public GameObject P2RightContainer;

	public GameObject FlashScreen;

	public GameObject BG;

	//[HideInInspector]
	public GameObject RotateGameObject;
	//bool ShakeActive;
	//bool NukeShakeActive;
	//bool nukeactivated;

	//float ShakeTimer;
	//float ShakeTimerValue;

	public static bool cardSelectedP1;
	public static bool cardSelectedP2;

	public static bool P1Ready;
	public static bool P2Ready;

	public static bool IsTutorial;
	public bool TutorialValue;

	public GameObject rayL_1;
	public GameObject rayR_1;

	public GameObject rayL_2;
	public GameObject rayR_2;

	public static GameObject PSgo;

	public GameObject sheildBreak;
	public static bool CardUsed;

	public static bool isRanger_P1;
	public static bool isRanger_P2;
	//public static bool Deselect;

	// Start is called before the first frame update
	void Start()
    {
		IsTutorial = TutorialValue;
		//ShakeTimerValue = 0.3f;
		//ShakeTimer = ShakeTimerValue;
		CamAnimator = Camera.main.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		if(P1LselectedGO != null)
		{
			P1LselectedGO.GetComponent<Shooting>().ray = rayL_1;
			P1LselectedGO.GetComponent<Transfering>().Active = true;
		}

		if (P1RselectedGO != null)
		{
			P1RselectedGO.GetComponent<Shooting>().ray = rayR_1;
			P1RselectedGO.GetComponentInChildren<PlayerController2>().IsActiveThisGO = true;
			P1RselectedGO.GetComponent<Transfering>().Active = true;
		}

		if (P2LselectedGO != null)
		{
			P2LselectedGO.GetComponent<Shooting>().ray = rayL_2;
			P2LselectedGO.GetComponentInChildren<PlayerController2>().IsActiveThisGO = true;
			P2LselectedGO.GetComponent<Transfering>().Active = true;
		}

		if (P2RselectedGO != null)
		{
			P2RselectedGO.GetComponent<Shooting>().ray = rayR_2;
			P2RselectedGO.GetComponentInChildren<PlayerController2>().IsActiveThisGO = true;
			P2RselectedGO.GetComponent<Transfering>().Active = true;
		}
		//if (ShakeActive)
		//{
		//	MainHeadCanvas.renderMode = RenderMode.WorldSpace;
		//	if (ShakeTimer <= 0)
		//	{
		//		ShakeActive = false;
		//		MainHeadCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		//		ShakeTimer = ShakeTimerValue;
		//	}
		//	else
		//	{
		//		ShakeTimer -= Time.deltaTime;
		//	}

		//}
		//if(Shooting.Firing == true)
		//{
		//	MainHeadCanvas.renderMode = RenderMode.WorldSpace;
		//}
		//if (NukeShakeActive)
		//{
		//	MainHeadCanvas.renderMode = RenderMode.WorldSpace;
		//	nukeactivated = true;
		//}
		//if (!NukeShakeActive && nukeactivated)
		//{
		//	MainHeadCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		//	nukeactivated = false;
		//}

		if (SelectedGO != null)
		{
			if(SelectedGO.GetComponentInChildren<DetectClick>().Selected == false)
			{
				SelectedGO = null;
			}
		}
		if (TurnSystem.isPlayer1turn)
		{
			SkipButton1.SetActive(true);
			SkipButton2.SetActive(false);
		}

		if (!TurnSystem.isPlayer1turn)
		{
			SkipButton1.SetActive(false);
			SkipButton2.SetActive(true);
		}

	}

	public void CamShake()
	{
		//ShakeActive = true;
		CamAnimator.SetTrigger("CamShake");
	}

	public void NukeCamShakeActivate()
	{
		//NukeShakeActive = true;
		CamAnimator.SetBool("NukeCamShake", true);
	}
	public void NukeCamShakeDectivate()
	{
		//NukeShakeActive = false;
		CamAnimator.SetBool("NukeCamShake", false);
	}

}
