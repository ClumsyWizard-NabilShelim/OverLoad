using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
	public GameObject Gamemanger;
	public bool Selected = false;
	public bool EmpbreakSelected = false;
	public GameObject ThisGO;
	public GameObject selectionDetection1;
	public GameObject selectionDetection2;

    // Start is called before the first frame update
    void Start()
    {
		this.GetComponent<TurretAnimScript>().enabled = true;
		Gamemanger = GameObject.FindGameObjectWithTag("GM");

	}

    // Update is called once per frame
    void Update()
    {
		if(Shooting.Firing == true || Gamemanger.GetComponent<GameManager>().SelectedGO == null)
		{
			Selected = false;
		}
		if (this.tag == "P1/Left" && Selected == false || this.tag == "P1/Right" && Selected == false)
		{
			Debug.Log("Fuck");
			selectionDetection1.GetComponent<LineRenderer>().enabled = false;
		}
		if (this.tag == "P2/Left" && Selected == false || this.tag == "P2/Right" && Selected == false)
		{
			selectionDetection2.GetComponent<LineRenderer>().enabled = false;
		}


		if (selectionDetection1 != null)
		{
			selectionDetection1.GetComponent<LineRenderer>().sortingOrder = -1;
		}

		if (selectionDetection2 != null)
		{
			selectionDetection2.GetComponent<LineRenderer>().sortingOrder = -1;
		}
		//	if(this.transform.parent.tag == "P1/Left")
		//	{
		//		if (Input.GetKeyDown(KeyCode.Alpha1))
		//		{
		//			Detect();
		//		}
		//	}

		//	if (this.transform.parent.tag == "P1/Right")
		//	{
		//		if (Input.GetKeyDown(KeyCode.Alpha2))
		//		{
		//			Detect();
		//		}
		//	}

		//	if (this.transform.parent.tag == "P2/Left")
		//	{
		//		if (Input.GetKeyDown("p"))
		//		{
		//			Detect();
		//		}
		//	}

		//	if (this.transform.parent.tag == "P2/Right")
		//	{
		//		if (Input.GetKeyDown("l"))
		//		{
		//			Detect();
		//		}
		//	}
	}

	void Detect()
	{

		if (GameManager.IsTutorial == false)
		{
			Gamemanger.GetComponent<GameManager>().RotateGameObject = this.transform.parent.gameObject;
			if (PauseMenu.isPause == false)
			{
				if (Gamemanger.GetComponent<GameManager>().SelectedGO == null)
				{
					FindObjectOfType<AudioManager>().Play("Select");
				}
				if (Transfering.CanTransferV != true)// && this.GetComponent<PlayerStats>().cells != 0)
				{
					Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = null;
					PlayerController2.ClickPos = transform.parent;
					EmpbreakSelected = !EmpbreakSelected;
					Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = this.transform.parent.gameObject;
					if (this.transform.parent.gameObject.tag == "P1/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P1/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn)
					{
						Gamemanger.GetComponent<GameManager>().SelectedGO = null;
						if (this.GetComponentInParent<PlayerStats>().cells != 0) { Debug.Log("Fuck"); selectionDetection1.SetActive(true); }
						Selected = !Selected;
						Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;

					}
					else if (this.transform.parent.gameObject.tag == "P2/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P2/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn)
					{
						StartCoroutine(WaitForChange());
						Gamemanger.GetComponent<GameManager>().SelectedGO = null;
						if (this.GetComponentInParent<PlayerStats>().cells != 0) { selectionDetection2.SetActive(true); }
						Selected = !Selected;
						Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;
					}
				}

			}

		}
		#region If tutorial
		if (GameManager.IsTutorial == true && TutorialSequenceHandeler.TurretClick == false)
		{
			Gamemanger.GetComponent<GameManager>().RotateGameObject = this.transform.parent.gameObject;
			if (PauseMenu.isPause == false)
			{
				if (Gamemanger.GetComponent<GameManager>().SelectedGO == null)
				{
					FindObjectOfType<AudioManager>().Play("Select");
				}
				if (Transfering.CanTransferV != true)// && this.GetComponent<PlayerStats>().cells != 0)
				{
					Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = null;
					PlayerController2.ClickPos = transform.parent;
					EmpbreakSelected = !EmpbreakSelected;
					Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = this.transform.parent.gameObject;
					if (this.transform.parent.gameObject.tag == "P1/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P1/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn)
					{
						Gamemanger.GetComponent<GameManager>().SelectedGO = null;
						if (this.GetComponentInParent<PlayerStats>().cells != 0) { Debug.Log("Fuck"); selectionDetection1.SetActive(true); }
						Selected = !Selected;
						Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;

					}
					else if (this.transform.parent.gameObject.tag == "P2/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P2/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn)
					{
						StartCoroutine(WaitForChange());
						Gamemanger.GetComponent<GameManager>().SelectedGO = null;
						if (this.GetComponentInParent<PlayerStats>().cells != 0) { selectionDetection2.SetActive(true); }
						Selected = !Selected;
						Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;
					}
				}

			}
		}
		#endregion
	}

	void OnMouseDown()
	{
		if(GameManager.CardUsed == false)
		{
			if (!Shooting.RayOn)
			{
				GameManager.PSgo = this.transform.parent.gameObject;
			}
			if (GameManager.IsTutorial == false)
			{
				Gamemanger.GetComponent<GameManager>().RotateGameObject = this.transform.parent.gameObject;
				if (PauseMenu.isPause == false)
				{
					if (Gamemanger.GetComponent<GameManager>().SelectedGO == null)
					{
						FindObjectOfType<AudioManager>().Play("Select");
					}
					if (Transfering.CanTransferV != true)// && this.GetComponent<PlayerStats>().cells != 0)
					{
						Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = null;
						PlayerController2.ClickPos = transform.parent;
						EmpbreakSelected = !EmpbreakSelected;
						Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = this.transform.parent.gameObject;
						if (this.transform.parent.gameObject.tag == "P1/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P1/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn)
						{
							Gamemanger.GetComponent<GameManager>().SelectedGO = null;
							if (this.GetComponentInParent<PlayerStats>().cells != 0)
							{

								selectionDetection1.SetActive(true);
								selectionDetection1.GetComponent<LineRenderer>().enabled = true;
							}
							Selected = !Selected;
							if (GameManager.CardUsed == false)
							{
								Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;
							}

						}
						else if (this.transform.parent.gameObject.tag == "P2/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P2/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn)
						{
							StartCoroutine(WaitForChange());
							Gamemanger.GetComponent<GameManager>().SelectedGO = null;
							if (this.GetComponentInParent<PlayerStats>().cells != 0)
							{
								selectionDetection2.SetActive(true);
								selectionDetection2.GetComponent<LineRenderer>().enabled = true;
							}
							Selected = !Selected;
							if (GameManager.CardUsed == false)
							{
								Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;
							}
						}
					}

				}

			}

			#region If tutorial
			if (GameManager.IsTutorial == true && TutorialSequenceHandeler.TurretClick == false)
			{
				Gamemanger.GetComponent<GameManager>().RotateGameObject = this.transform.parent.gameObject;
				if (PauseMenu.isPause == false)
				{
					if (Gamemanger.GetComponent<GameManager>().SelectedGO == null)
					{
						FindObjectOfType<AudioManager>().Play("Select");
					}
					if (Transfering.CanTransferV != true)// && this.GetComponent<PlayerStats>().cells != 0)
					{
						Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = null;
						PlayerController2.ClickPos = transform.parent;
						EmpbreakSelected = !EmpbreakSelected;
						Gamemanger.GetComponent<GameManager>().EmpbreakSelectedGO = this.transform.parent.gameObject;
						if (this.transform.parent.gameObject.tag == "P1/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P1/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && TurnSystem.isPlayer1turn)
						{
							Gamemanger.GetComponent<GameManager>().SelectedGO = null;
							if (this.GetComponentInParent<PlayerStats>().cells != 0)
							{
								selectionDetection1.SetActive(true);
								selectionDetection1.GetComponent<LineRenderer>().enabled = true;
							}
							Selected = !Selected;
							Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;

						}
						else if (this.transform.parent.gameObject.tag == "P2/Left" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn || this.transform.parent.gameObject.tag == "P2/Right" && this.GetComponentInParent<PlayerCardManager>().EMPactive == false && !TurnSystem.isPlayer1turn)
						{
							StartCoroutine(WaitForChange());
							Gamemanger.GetComponent<GameManager>().SelectedGO = null;
							if (this.GetComponentInParent<PlayerStats>().cells != 0)
							{
								selectionDetection2.SetActive(true);
								selectionDetection2.GetComponent<LineRenderer>().enabled = true;
							}
							Selected = !Selected;
							Gamemanger.GetComponent<GameManager>().SelectedGO = this.transform.parent.gameObject;
						}
					}

				}
			}
			#endregion
		}

	}
	IEnumerator WaitForChange()
	{
		this.GetComponent<PlayerController2>().Offset = -180;
		yield return new WaitForSeconds(0.2f);
		this.GetComponent<PlayerController2>().Offset = 0;
	}
}
