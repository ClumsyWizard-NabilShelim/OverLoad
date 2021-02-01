using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EHandler : MonoBehaviour
{
	public GameObject CARD;
	public bool OpenTransferContainer;

	public void Close()
	{
		//this.GetComponent<Image>().enabled = false;
		//Debug.Log("closed");
	}
	public void Open()
	{
		if (OpenTransferContainer)
		{
			CARD.SetActive(false);
		}
		else
		{
			CARD.SetActive(true);
		}
	}
}
