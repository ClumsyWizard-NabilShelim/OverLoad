using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelect : MonoBehaviour
{
	public bool CardUsed;
	public static bool canGrown;


	CardDeselect cardDeselect;
	GameManager GM;
	public Vector3 thisObjectPos;
	Vector3 defaultSize;
	public Vector3 GrowthSize;
	BoxCollider2D DeselectCollider;

	Transform CamPos;

	public TextMeshProUGUI Discription;
	public TextMeshProUGUI effectText;
	public TextMeshProUGUI DamageText;
	[TextArea]
	public string DiscriptionText;
	public bool Active;
	public bool goback;
	// Start is called before the first frame update

	void Awake()
	{
		//if(DeselectCollider != null)
		//{
		//DeselectCollider = transform.parent.parent.GetComponent<BoxCollider2D>();
		//DeselectCollider.enabled = false;
		//}
		
	}
	void Start()
    {
		DamageText.text = this.GetComponent<CardSetUp>().Damage.ToString();
		effectText.text = this.GetComponent<CardSetUp>().EffectTime.ToString();
		Discription.text = DiscriptionText;
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		defaultSize = transform.localScale;
		CamPos = Camera.main.transform;
		cardDeselect = transform.parent.parent.GetComponent<CardDeselect>();
	}

	// Update is called once per frame
	void Update()
	{
		thisObjectPos = transform.parent.position;
	
		if(GameManager.IsTutorial == false)
		{
			if (this.GetComponent<CardSetUp>().CardTag == "P1" && !TurnSystem.isPlayer1turn)
			{
				goback = false;
				GameManager.cardSelectedP1 = false;
			}

			if (this.GetComponent<CardSetUp>().CardTag == "P2" && TurnSystem.isPlayer1turn)
			{
				goback = false;
				GameManager.cardSelectedP2 = false;
			}
		}
	
		if (this.GetComponent<CardSetUp>().CardTag == "P1")
		{
			if (goback)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, GrowthSize, 0.1f);
				transform.position = Vector3.Lerp(transform.position, new Vector2(CamPos.position.x, CamPos.position.y), 0.1f);
				GameManager.cardSelectedP1 = true;
			}
			if (!goback)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, defaultSize, 0.1f);
				transform.position = Vector3.Lerp(transform.position, thisObjectPos, 0.1f);
			}
	
		}
	
		if (this.GetComponent<CardSetUp>().CardTag == "P2")
		{
			if (goback)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, GrowthSize, 0.1f);
				transform.position = Vector3.Lerp(transform.position, new Vector2(CamPos.position.x, CamPos.position.y), 0.1f);
				GameManager.cardSelectedP2 = true;

			}
	
			if (!goback)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, defaultSize, 0.1f);
				transform.position = Vector3.Lerp(transform.position, thisObjectPos, 0.1f);
			}
		}
	}

	void OnMouseDown()
	{
	
		if(this.tag != "AntiEMP")
		{
			if (GM.SelectedGO == null)
			{
				if (this.GetComponent<CardSetUp>().CardTag == "P1" && TurnSystem.isPlayer1turn)
				{
					if(GameManager.IsTutorial == false)
					{
						if (!GameManager.cardSelectedP1)
						{ 
							goback = !goback;
						}
					}
					if (GameManager.IsTutorial)
					{
						if (!GameManager.cardSelectedP1)
						{
							if (TutorialSequenceHandeler.TransferDonePressed == true)
							{
								TutorialSequenceHandeler.CardClicked = true;
								if (TutorialSequenceHandeler.CardClicked)
								{
									goback = !goback;
								}
							}

							
						}
					}
					if(GameManager.IsTutorial == false)
					{
						if (GameManager.cardSelectedP1)
						{
							TutorialSequenceHandeler.CardClosed = true;
							goback = false;
							GameManager.cardSelectedP1 = false;
						}
					}

					if (GameManager.IsTutorial == true)
					{
						if (GameManager.cardSelectedP1)
						{
							if(TutorialSequenceHandeler.CardClicked == false)
							{
								TutorialSequenceHandeler.CardClosed = true;
								goback = false;
								GameManager.cardSelectedP1 = false;
							}
							
						}
					}

				}

				if (this.GetComponent<CardSetUp>().CardTag == "P2" && !TurnSystem.isPlayer1turn)
				{
					if (!GameManager.cardSelectedP2)
					{
						goback = !goback;
					}
					if (GameManager.cardSelectedP2)
					{
						goback = false;
						GameManager.cardSelectedP2 = false;
					}
				}
			}
		}

		if(this.tag == "AntiEMP")
		{
			if (GM.EmpbreakSelectedGO == null)
			{
				if (this.GetComponent<CardSetUp>().CardTag == "P1" && TurnSystem.isPlayer1turn)
				{
					if (!GameManager.cardSelectedP1)
					{
						goback = !goback;
					}
					if (GameManager.cardSelectedP1)
					{
						goback = false;
						GameManager.cardSelectedP1 = false;
					}
				}

				if (this.GetComponent<CardSetUp>().CardTag == "P2" && !TurnSystem.isPlayer1turn)
				{
					if (!GameManager.cardSelectedP2)
					{
						goback = !goback;
					}
					if (GameManager.cardSelectedP2)
					{
						goback = false;
						GameManager.cardSelectedP2 = false;
					}
				}


			}
		}


	}
}
