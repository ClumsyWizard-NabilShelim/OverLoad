using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
	GameManager GM;
	public static bool isPlayer1turn = true;
	public Image player1TurnImage;
	public Image player2TurnImage;

	public float TurnLength;
	public float TurnTime;

	public static bool DontChange;
	// Start is called before the first frame update
	void Start()
    {
		isPlayer1turn = true;
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (GameManager.IsTutorial)
		{
			isPlayer1turn = true;
		}
		if (ScenetransManager.StartGame && CoverActivate.CanPassTime == true)
		{
			if (isPlayer1turn)
			{
				Player1();
			}
			else
			{
				Player2();
			}

		}
		if (!ScenetransManager.StartGame)
		{
			TurnTime = TurnLength;
		}
	}

	void Player1()
	{
		player2TurnImage.fillAmount = 0;
		if (CoverActivate.CanPassTime && DontChange == false)
		{
			if(GM.SelectedGO != null)
			{
				if (GM.SelectedGO.CompareTag("P1/Left") && GM.P1LselectedGO.GetComponent<Shooting>().BulletShot == false)
				{
					TurnTime -= Time.deltaTime;
				}
				else if (GM.SelectedGO.CompareTag("P1/Right") && GM.P1RselectedGO.GetComponent<Shooting>().BulletShot == false)
				{
					TurnTime -= Time.deltaTime;
				}
			}
			if (GM.SelectedGO == null)
			{
				TurnTime -= Time.deltaTime;
			}
		}

		player1TurnImage.fillAmount = (TurnTime / TurnLength);
		if (TurnTime <= 0)
		{
			TurnTime = TurnLength;
			isPlayer1turn = !isPlayer1turn;
			
		}
	}

	void Player2()
	{
		player1TurnImage.fillAmount = 0;
		if (CoverActivate.CanPassTime && DontChange == false)
		{
			if(GM.SelectedGO != null)
			{
				if (GM.SelectedGO.CompareTag("P2/Left") && GM.P2LselectedGO.GetComponent<Shooting>().BulletShot == false)
				{
					TurnTime -= Time.deltaTime;
				}
				else if (GM.SelectedGO.CompareTag("P2/Right") && GM.P2RselectedGO.GetComponent<Shooting>().BulletShot == false)
				{
					TurnTime -= Time.deltaTime;
				}
			}
			if(GM.SelectedGO == null)
			{
				TurnTime -= Time.deltaTime;
			}
		}
		
		player2TurnImage.fillAmount = (TurnTime / TurnLength);
		if (TurnTime <= 0)
		{
			TurnTime = TurnLength;
			isPlayer1turn = !isPlayer1turn;
		}
	}

	public void switchTurn()
	{
		if(CoverActivate.CanPassTime == true && !GameManager.IsTutorial)
		{
			GM.SelectedGO = null;
			GM.EmpbreakSelectedGO = null;
			isPlayer1turn = !isPlayer1turn;
			TurnTime = TurnLength;
			GM.SelectedGO = null;
			DontChange = false;
		}
	}
}
