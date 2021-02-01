using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButtonsOn : MonoBehaviour
{
	public GameObject[] Button1;

	public void Activate()
	{
		//if (GameManager.IsTutorial)
		//{
			for (int i = 0; i < Button1.Length; i++)
			{
				Button1[i].SetActive(true);
			}
		//}
	
	}
	public void Deactivate()
	{
		//if (GameManager.IsTutorial)
		//{
		for (int i = 0; i < Button1.Length; i++)
		{
			Button1[i].SetActive(false);
		}
		//}

	}
}
