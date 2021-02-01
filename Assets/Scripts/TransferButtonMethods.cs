using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferButtonMethods : MonoBehaviour
{
    public void Increase()
	{
		if (this.GetComponent<Transfering>().CellsToTransfer + 1 < this.GetComponent<PlayerStats>().cells + 1)
		{
			this.GetComponent<Transfering>().CellsToTransfer++;
		}

	}

	public void Decrease()
	{
		if (this.GetComponent<Transfering>().CellsToTransfer > 0)
		{
			this.GetComponent<Transfering>().CellsToTransfer--;
		}
	}

}
