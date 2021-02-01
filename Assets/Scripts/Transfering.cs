using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transfering : MonoBehaviour
{
	public GameObject gameManager;
	public bool CanTransfer;
	public static bool CanTransferV;
	[HideInInspector]
	public CoverActivate cover;

	GameObject MainCanvasCover;
	GameObject HowManyToTransferContainer;
	GameObject TranferFromGO;
	GameObject TransferToGO;
	[HideInInspector]
	public GameObject[] TransferCells = new GameObject[4];
	[HideInInspector]
	public GameObject CellPowerHolder;
	[HideInInspector]
	public int CellsToTransfer;
	bool clicked = false;

	[HideInInspector]
	public bool Active;
    // Start is called before the first frame update
    void Awake()
    {
		MainCanvasCover = GameObject.FindGameObjectWithTag("CoverCanvas");
		cover = MainCanvasCover.GetComponent<CoverActivate>();
		CellsToTransfer = 1;
		TransferCells = new GameObject[4];
	}

    // Update is called once per frame
    void Update()
    {
		if (this.tag == "P1/Left"/* && TranferFromGO == null && TransferToGO == null && HowManyToTransferContainer == null*/)
		{
			TranferFromGO = gameManager.GetComponent<GameManager>().P1LselectedGO;
			TransferToGO = gameManager.GetComponent<GameManager>().P1RselectedGO;
			HowManyToTransferContainer = gameManager.GetComponent<GameManager>().P1LeftContainer;
			CellPowerHolder = gameManager.GetComponent<GameManager>().P1LeftContainer.transform.GetChild(0).gameObject;
		}

		if (this.tag == "P1/Right"/* && TranferFromGO == null && TransferToGO == null && HowManyToTransferContainer == null*/)
		{
			TranferFromGO = gameManager.GetComponent<GameManager>().P1RselectedGO;
			TransferToGO = gameManager.GetComponent<GameManager>().P1LselectedGO;
			HowManyToTransferContainer = gameManager.GetComponent<GameManager>().P1RightContainer;
			CellPowerHolder = gameManager.GetComponent<GameManager>().P1RightContainer.transform.GetChild(0).gameObject;
		}

		if (this.tag == "P2/Left"/* &*//*& TranferFromGO == null && TransferToGO == null && HowManyToTransferContainer == null*/)
		{
			TranferFromGO = gameManager.GetComponent<GameManager>().P2LselectedGO;
			TransferToGO = gameManager.GetComponent<GameManager>().P2RselectedGO;
			HowManyToTransferContainer = gameManager.GetComponent<GameManager>().P2LeftContainer;
			CellPowerHolder = gameManager.GetComponent<GameManager>().P2LeftContainer.transform.GetChild(0).gameObject;
		}

		if (this.tag == "P2/Right"/* && TranferFromGO == null && TransferToGO == null && HowManyToTransferContainer == null*/)
		{
			TranferFromGO = gameManager.GetComponent<GameManager>().P2RselectedGO;
			TransferToGO = gameManager.GetComponent<GameManager>().P2LselectedGO;
			HowManyToTransferContainer = gameManager.GetComponent<GameManager>().P2RightContainer;
			CellPowerHolder = gameManager.GetComponent<GameManager>().P2RightContainer.transform.GetChild(0).gameObject;
		}

		for (int i = 0; i < CellPowerHolder.transform.childCount; i++)
		{
			TransferCells[i] = CellPowerHolder.transform.GetChild(i).gameObject;
		}

		CanTransferV = CanTransfer;
		for (int i = 0; i < TransferCells.Length; i++)
		{
			if (i < CellsToTransfer)
			{
				TransferCells[i].SetActive(true);
			}
			else
			{
				TransferCells[i].SetActive(false);
			}
		}
		if (CanTransfer)
		{
			TutorialSequenceHandeler.CoverActivated = true;
			TransferActivate();
		}

    }

	public void Done()
	{
		if (this.gameObject.activeInHierarchy)
		{
			if (Active)
			{

				if (clicked == false)
				{
					clicked = true;
					FindObjectOfType<AudioManager>().Play("ButtonPress");
					if (TranferFromGO/*.GetComponentInParent<PlayerStats>() */== null)
					{
						if (this.tag == "P1/Left")
						{
							TranferFromGO = gameManager.GetComponent<GameManager>().P1LselectedGO;
							TransferToGO = gameManager.GetComponent<GameManager>().P1RselectedGO;
						}

						if (this.tag == "P1/Right")
						{
							TranferFromGO = gameManager.GetComponent<GameManager>().P1RselectedGO;
							TransferToGO = gameManager.GetComponent<GameManager>().P1LselectedGO;
						}

						if (this.tag == "P2/Left")
						{
							TranferFromGO = gameManager.GetComponent<GameManager>().P2LselectedGO;
							TransferToGO = gameManager.GetComponent<GameManager>().P2RselectedGO;
						}

						if (this.tag == "P2/Right")
						{
							TranferFromGO = gameManager.GetComponent<GameManager>().P2RselectedGO;
							TransferToGO = gameManager.GetComponent<GameManager>().P2LselectedGO;
						}
					}
					TranferFromGO.GetComponent<PlayerStats>().cells -= CellsToTransfer;
					TransferToGO.GetComponent<PlayerStats>().cells += CellsToTransfer;
					CanTransfer = false;
					TransferCLose();
					if (CellsToTransfer != 0 && this.enabled == true)
					{
						StartCoroutine(Change());
					}
					TranferFromGO.GetComponentInChildren<DetectClick>().Selected = false;
					TransferToGO.GetComponentInChildren<DetectClick>().Selected = false;
					if (GameManager.IsTutorial)
					{
						TutorialSequenceHandeler.TransferDonePressed = true;
					}
				}
			}
		}
	
	}

	IEnumerator Waiter()
	{
		yield return new WaitForSeconds(2f);
	}
	void TransferActivate()
	{
		FindObjectOfType<AudioManager>().Play("CardSelect");
		cover.PTransfer = HowManyToTransferContainer;
		cover.Activate = true;
		cover.Deactivate = false;
		CanTransfer = false;
	}

	IEnumerator Change()
	{
		yield return new WaitForSeconds(0f);
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
		clicked = false;
	}

	void TransferCLose()
	{
		FindObjectOfType<AudioManager>().Play("CardSelect");
		if(cover == null)
		{
			MainCanvasCover = GameObject.FindGameObjectWithTag("CoverCanvas");
			cover = MainCanvasCover.GetComponent<CoverActivate>();
		}
		cover.Deactivate = true;
	}
}
