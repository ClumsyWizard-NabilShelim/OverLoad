using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
	public string ThisTag;
	[HideInInspector]
	public GameObject ClassGO;
	[HideInInspector]
	public GameObject TurretGO;

	public static bool StartSelection = false;
	[HideInInspector]
	public bool TurretSelected;
	[Header("Turret Class____Player1")]
	public GameObject[] Turret_Classes_P1;
    int classIndex_P1;

	[Header("Turret_P1")]
	public GameObject[] Turret_Aggressor_P1;
	public GameObject[] Turret_Ranger_P1;
	public GameObject[] Turret_Defender_P1;

	bool TurretOn_Aggressor_P1;
	bool TurretOn_Ranger_P1;
	bool TurretOn_Defender_P1;

    int Turret_Aggressor_P1_index;
	int Turret_Ranger_P1_index;
	int Turret_Defender_P1_index;

	[Header("Aggressor")]
	public GameObject[] Aggressor_Turrets_Left;
	public GameObject[] Aggressor_Turrets_Right;

	[Header("Ranger")]
	public GameObject[] Ranger_Turrets_Left;
	public GameObject[] Ranger_Turrets_Right;

	[Header("Defender")]
	public GameObject[] Defender_Turrets_Left;
	public GameObject[] Defender_Turrets_Right;

	public GameObject P1SelectionMenu;
	public GameObject P2SelectionMenu;

	public Animator ReadyAnim;
	public Animator ReadyAnim2;

	public bool ClearTurretINFO;

	public GameObject Aggressor_Cards;
	public GameObject Ranger_Cards;
	public GameObject Defender_Cards;


	// Start is called before the first frame update
	void Start()
    {
		ReadyAnim.gameObject.SetActive(false);
		ReadyAnim2.gameObject.SetActive(false);
		if (ClearTurretINFO)
		{
			Back_P1();
		}
		
		//TurretClassSelection_P1();
	}

    // Update is called once per frame
    void Update()
	{
		if (StartSelection)
		{
			StartCoroutine(Select());
		}
		if (classIndex_P1 > Turret_Classes_P1.Length - 1)
		{
			classIndex_P1 = 0;
			TurretClassSelection_P1();
		}

		if (classIndex_P1 < 0)
		{
			classIndex_P1 = Turret_Classes_P1.Length - 1;
			TurretClassSelection_P1();
		}

		#region Aggressor_P1_Bounds
		if (Turret_Aggressor_P1_index > Turret_Aggressor_P1.Length - 1)
		{
			Turret_Aggressor_P1_index = 0;
			Aggressor_P1();
		}

		if (Turret_Aggressor_P1_index < 0)
		{
			Turret_Aggressor_P1_index = Turret_Aggressor_P1.Length - 1;
			Aggressor_P1();
		}
		#endregion

		#region Ranger_P1_Bounds
		if (Turret_Ranger_P1_index > Turret_Ranger_P1.Length - 1)
		{
			Turret_Ranger_P1_index = 0;
			Ranger_P1();
		}

		if (Turret_Ranger_P1_index < 0)
		{
			Turret_Ranger_P1_index = Turret_Ranger_P1.Length - 1;
			Ranger_P1();
		}
		#endregion

		#region Defender_P1_Bounds
		if (Turret_Defender_P1_index > Turret_Defender_P1.Length - 1)
		{
			Turret_Defender_P1_index = 0;
			Defender_P1();
		}

		if (Turret_Defender_P1_index < 0)
		{
			Turret_Defender_P1_index = Turret_Defender_P1.Length - 1;
			Defender_P1();
		}
		#endregion
	}

	#region AggressorSelection_P1
	void Aggressor_P1()
	{
		int i = 0;
		foreach (GameObject Class in Turret_Aggressor_P1)
		{
			if (i == Turret_Aggressor_P1_index)
			{
				Turret_Aggressor_P1[i].SetActive(true);
				Aggressor_Turrets_Left[i].SetActive(true);
				Aggressor_Turrets_Right[i].SetActive(true);
				TurretGO = Turret_Aggressor_P1[i];
			}
			else
			{
				Turret_Aggressor_P1[i].SetActive(false);
				Aggressor_Turrets_Left[i].SetActive(false);
				Aggressor_Turrets_Right[i].SetActive(false);
			}
			i++;
		}

		
	}
	#endregion

	#region RangerSelection_P1
	void Ranger_P1()
	{
		int i = 0;
		foreach (GameObject Class in Turret_Ranger_P1)
		{
			if (i == Turret_Ranger_P1_index)
			{
				Turret_Ranger_P1[i].SetActive(true);
				Ranger_Turrets_Left[i].SetActive(true);
				Ranger_Turrets_Right[i].SetActive(true);
				TurretGO = Turret_Ranger_P1[i];
			}
			else
			{
				Turret_Ranger_P1[i].SetActive(false);
				Ranger_Turrets_Left[i].SetActive(false);
				Ranger_Turrets_Right[i].SetActive(false);
			}
			i++;
		}
	}
	#endregion

	#region DefenderSelection_P1
	void Defender_P1()
	{
		int i = 0;
		foreach (GameObject Class in Turret_Defender_P1)
		{
			if (i == Turret_Defender_P1_index)
			{
				Turret_Defender_P1[i].SetActive(true);
				Defender_Turrets_Left[i].SetActive(true);
				Defender_Turrets_Right[i].SetActive(true);
				TurretGO = Turret_Defender_P1[i];
			}
			else
			{
				Turret_Defender_P1[i].SetActive(false);
				Defender_Turrets_Left[i].SetActive(false);
				Defender_Turrets_Right[i].SetActive(false);
			}
			i++;
		}
	}
	#endregion

	#region ClassSelction
	void TurretClassSelection_P1()
	{
		int i = 0;
		foreach (GameObject Class in Turret_Classes_P1)
		{
			if(i == classIndex_P1)
			{
				Turret_Classes_P1[i].SetActive(true);
				ClassGO = Turret_Classes_P1[i];
			}
			else
			{
				Turret_Classes_P1[i].SetActive(false);
			}
			i++;
		}
	}
	#endregion

	#region Buttons_P1
	public void ClassIndexIncrease_P1()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		if (TurretOn_Aggressor_P1)
		{
			Turret_Aggressor_P1_index++;
			Aggressor_P1();
		}
		if (TurretOn_Ranger_P1)
		{
			Turret_Ranger_P1_index++;
			Ranger_P1();
		}
		if (TurretOn_Defender_P1)
		{
			Turret_Defender_P1_index++;
			Defender_P1();
		}
		if (!TurretOn_Ranger_P1 && !TurretOn_Aggressor_P1 && !TurretOn_Defender_P1)
		{
			classIndex_P1++;
			TurretClassSelection_P1();
		}
		
	}

	public void ClassIndexDecrease_P1()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		if (TurretOn_Aggressor_P1)
		{
			Turret_Aggressor_P1_index--;
			Aggressor_P1();
		}
		if (TurretOn_Ranger_P1)
		{
			Turret_Ranger_P1_index--;
			Ranger_P1();
		}
		if (TurretOn_Defender_P1)
		{
			Turret_Defender_P1_index--;
			Defender_P1();
		}
		if (!TurretOn_Ranger_P1 && !TurretOn_Aggressor_P1 && !TurretOn_Defender_P1)
		{
			classIndex_P1--;
			TurretClassSelection_P1();
		}

	}
	#endregion

	#region ClassEnter_P1
	public void Open_Aggressor()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		TurretSelected = true;
		ClassGO.GetComponentInChildren<Image>().enabled = false;
		TurretOn_Aggressor_P1 = true;
		Aggressor_P1();
		Aggressor_Cards.SetActive(true);
		Ranger_Cards.SetActive(false);
		Defender_Cards.SetActive(false);
	}

	public void Open_Ranger()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		TurretSelected = true;
		ClassGO.GetComponentInChildren<Image>().enabled = false;
		TurretOn_Ranger_P1 = true;
		Ranger_P1();
		Aggressor_Cards.SetActive(false);
		Ranger_Cards.SetActive(true);
		Defender_Cards.SetActive(false);
		if(ThisTag == "P1")
		{
			GameManager.isRanger_P1 = true;
		}
		if (ThisTag == "P2")
		{
			GameManager.isRanger_P2 = true;
		}
	}

	public void Open_Defender()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		TurretSelected = true;
		ClassGO.GetComponentInChildren<Image>().enabled = false;
		TurretOn_Defender_P1 = true;
		Defender_P1();
		Aggressor_Cards.SetActive(false);
		Ranger_Cards.SetActive(false);
		Defender_Cards.SetActive(true);
	}
	#endregion

	IEnumerator Select()
	{
		TurretClassSelection_P1();
		yield return new WaitForSeconds(0.1f);
		StartSelection = false;
	}
	public void Back_P1()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		TurretSelected = false;

		if(ClassGO != null)
		{
			ClassGO.GetComponentInChildren<Image>().enabled = true;
		}


		for (int i = 0; i < Aggressor_Turrets_Left.Length; i++)
		{
			Aggressor_Turrets_Left[i].SetActive(false);
		}
		for (int i = 0; i < Aggressor_Turrets_Right.Length; i++)
		{
			Aggressor_Turrets_Right[i].SetActive(false);
		}
		//////////////////////////////////////////////////////////////////
		for (int i = 0; i < Ranger_Turrets_Left.Length; i++)
		{
			Ranger_Turrets_Left[i].SetActive(false);
		}
		for (int i = 0; i < Ranger_Turrets_Right.Length; i++)
		{
			Ranger_Turrets_Right[i].SetActive(false);
		}
		/////////////////////////////////////////////////////////////////
		for (int i = 0; i < Defender_Turrets_Left.Length; i++)
		{
			Defender_Turrets_Left[i].SetActive(false);
		}
		for (int i = 0; i < Defender_Turrets_Right.Length; i++)
		{
			Defender_Turrets_Right[i].SetActive(false);
		}
		TurretOn_Ranger_P1 = false;
		TurretOn_Defender_P1 = false;
		TurretOn_Aggressor_P1 = false;
		if(TurretGO != null)
		{
			TurretGO.SetActive(false);
		}
		TurretClassSelection_P1();
		if (ThisTag == "P1")
		{
			GameManager.isRanger_P1 = false;
		}
		if (ThisTag == "P2")
		{
			GameManager.isRanger_P2 = false;
		}
	}

	public void P1Selected()
	{
		
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		if (P1SelectionMenu.GetComponent<SelectionMenu>().TurretSelected == true)
		{
			ReadyAnim.gameObject.SetActive(true);
			ReadyAnim.SetTrigger("Ready");
			GameManager.P1Ready = true;
		}
	}

	public void P2Selected()
	{
		
		FindObjectOfType<AudioManager>().Play("ButtonPress");
		if (P2SelectionMenu.GetComponent<SelectionMenu>().TurretSelected == true)
		{
			ReadyAnim2.gameObject.SetActive(true);
			ReadyAnim2.SetTrigger("Ready");
			GameManager.P2Ready = true;
		}
	}
}
