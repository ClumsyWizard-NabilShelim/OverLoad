using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSelectionStart : MonoBehaviour
{
	public SelectionMenu P1;
	public SelectionMenu P2;
    public void StarTurretSelection()
	{
		SelectionMenu.StartSelection = true;
	}

	public void CloseSelectionMenu()
	{
		this.GetComponent<Animator>().SetTrigger("SelectionMenu_Out");
	}
}
