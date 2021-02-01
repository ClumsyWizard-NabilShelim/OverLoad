using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequenceHandeler : MonoBehaviour
{

	public GameObject[] PopUps;
	GameManager GM;

	int popUpIndex;

	public static bool PlayClicked;
	public static bool CoverActivated;
	public static bool TransferDonePressed;
	public static bool CardClicked;
	public static bool CardClosed;
	public static bool TurretClick;
	public TutorialManager TM;

	bool NextPartIn;
    // Start is called before the first frame update
    void Start()
    {
		GM = FindObjectOfType<GameManager>();
		PopUps = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			PopUps[i] = transform.GetChild(i).gameObject;
		}
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < PopUps.Length; i++)
		{
			if(i == popUpIndex)
			{
				PopUps[i].SetActive(true);
			}
			else
			{
				PopUps[i].SetActive(false);
			}
		}

		if (popUpIndex == 0)
		{
			if (PlayClicked == true)
			{
				popUpIndex++;
				NextPartIn = true;
				TurretClick = true;
			}
		}
		else if(popUpIndex == 1)
		{
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
				TurretClick = true;
			}
			
			if (Shooting.Firing == true)
			{
				popUpIndex++;
				NextPartIn = true;
				//TurretClick = false;
			}
		}
		else if (popUpIndex == 2)
		{
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
				TurretClick = true;
			}

			if (CoverActivated)
			{
				Debug.Log("FuckingHeal");
				popUpIndex++;
				NextPartIn = true;
				//TurretClick = false;
			}
		}else if(popUpIndex == 3)
		{
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
				TurretClick = true;
			}

			if (TransferDonePressed == true)
			{
				popUpIndex++;
				NextPartIn = true;
				//TurretClick = false;
			}
		}
		else if (popUpIndex == 4)
		{
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
				TurretClick = true;
			}

			if (CardClicked == true)
			{
				popUpIndex++;
				NextPartIn = true;
				//TurretClick = false;
			}
		}
		else if (popUpIndex == 5)
		{
			PlayerPrefs.SetInt("Isfalse", 1);
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
				TurretClick = true;
			}

			if (CardClosed == true)
			{
				Debug.Log("aight mate");
				popUpIndex++;
				NextPartIn = true;
				//TurretClick = false;
			}
		}
		else if (popUpIndex == 6)
		{
			if (NextPartIn)
			{
				StartCoroutine(NextPart());
			}
		}


		if (popUpIndex > transform.childCount)
		{
			Debug.Log("Done this is the end");
		}
    }

	IEnumerator NextPart()
	{
		yield return new WaitForSeconds(2f);
		TM.RobotAnimator.SetBool("RobotIn", true);
		NextPartIn = false;
	}
}
