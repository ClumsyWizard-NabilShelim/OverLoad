using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSetUp : MonoBehaviour
{
	public float Speed = 2;
	public GameObject gameManager;

	public int EffectTime;
	public int Damage;
	public bool CanShoot = true;
	public SpriteRenderer Renderer;
	public Sprite CardImage;
	public GameObject PlayerGO;
	GameObject EMPbreakPlayerGO;

	public Image CircuitCharge;
	public Animator CircuitAnimator;
	public Animator DisapperAnimator;
    public bool Select;

	//[HideInInspector]
	public string CardTag;

	public GameObject DesTextGO;

	public static bool ActivateLeadShield_P1;
	public static bool ActivateLeadShield_P2;

	public static bool LeadOn_P1;
	public static bool LeadOn_P2;
	//public Vector3 Size;
	//public Vector3 Position;
	//[HideInInspector]

	int Chance = -1;


	bool Move;

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GM");
		Renderer.sprite = CardImage;
		if(CardTag == "P1")
		{
			
			DesTextGO.transform.localRotation = Quaternion.Euler(DesTextGO.transform.localRotation.x, 0, DesTextGO.transform.localRotation.z);

		}
		if (CardTag == "P2")
		{
			
			DesTextGO.transform.localRotation = Quaternion.Euler(DesTextGO.transform.localRotation.x, 180, DesTextGO.transform.localRotation.z);
			this.GetComponent<CardSelect>().effectText.transform.localRotation = Quaternion.Euler(0, 180, 0);
			this.GetComponent<CardSelect>().DamageText.transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		//CircuitAnimator = this.GetComponentInChildren<Animator>();
		//DisapperAnimator = this.GetComponent<Animator>();
		//Size = transform.localScale;
		//Position = transform.position;
	}

	void Update()
	{
		PlayerGO = gameManager.GetComponent<GameManager>().SelectedGO;

		EMPbreakPlayerGO = gameManager.GetComponent<GameManager>().EmpbreakSelectedGO;

		Debug.Log(LeadOn_P2);
		if(CardTag == "P1")
		{
			if (ActivateLeadShield_P1)
			{
				if (this.tag == "LeadShield")
				{
					if (Chance == -1)
					{
						Chance = Random.Range(0, 2);
					}

					if (Chance == 0)
					{
						Debug.Log("Return");
						return;
					}
					else if (Chance == 1)
					{
						StartCoroutine(Lead());
					//	LeadOn_P1 = true;
						int C = Random.Range(0, 2);
						if (C == 0)
						{
							gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerCardManager>().ActivateLeadShield();
							Debug.Log("P1L : lead");
							ActivateLeadShield_P1 = false;
						}
						if (C == 1)
						{
							gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerCardManager>().ActivateLeadShield();
							Debug.Log("P1L : lead");
							ActivateLeadShield_P1 = false;
						}
					}
				}

			}
		}

		if (CardTag == "P2")
		{
			if (ActivateLeadShield_P2)
			{
				if (this.tag == "LeadShield")
				{
					if(Chance == -1)
					{
						Chance = Random.Range(0, 2);
					}
					if (Chance == 0)
					{
						Debug.Log("Return");
						return;
					}
					else if (Chance == 1)
					{
						//LeadOn_P2 = true;
						StartCoroutine(Lead());
						int C = Random.Range(0, 2);
				
						if (C == 0)
						{
							gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerCardManager>().ActivateLeadShield();
							Debug.Log("P2L : lead");
							ActivateLeadShield_P2 = false;
						}
						if (C == 1)
						{
							gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerCardManager>().ActivateLeadShield();
							Debug.Log("P2R : lead");
							ActivateLeadShield_P2 = false;
						}

					}
				}

			}
		}

	}


	void OnMouseDown()
	{
		if (!Select)
		{
			if (GameManager.CardUsed == false)
			{
				StartCoroutine(ActivatePower());
			}

		}
		
	}

	IEnumerator Lead()
	{
		GameManager.CardUsed = true;
		Debug.Log("Card in");
		Select = true;
		FindObjectOfType<AudioManager>().Play("CardSelect");
		CanShoot = false;
		CircuitAnimator.SetTrigger("ChargeUP");
		yield return new WaitForSeconds(1.8f);
		DisapperAnimator.SetTrigger("Disapper");
		Destroy(this.gameObject, 1f);
		CanShoot = true;
		//if(CardTag == "P1")
		//{
		//	ActivateLeadShield_P1 = false;
		//}
		//if (CardTag == "P2")
		//{
		//	ActivateLeadShield_P2 = false;
		//}

	}
	IEnumerator ActivatePower()
	{
		
		if (TurnSystem.isPlayer1turn && CardTag == "P1" || !TurnSystem.isPlayer1turn && CardTag == "P2")
		{
			if (EMPbreakPlayerGO != null)
			{

				if (this.tag == "AntiEMP")
				{
					if (EMPbreakPlayerGO.tag == "P1/Left" && TurnSystem.isPlayer1turn || EMPbreakPlayerGO.tag == "P1/Right" && TurnSystem.isPlayer1turn || EMPbreakPlayerGO.tag == "P2/Left" && !TurnSystem.isPlayer1turn || EMPbreakPlayerGO.tag == "P2/Right" && !TurnSystem.isPlayer1turn)
					{
						TurnSystem.DontChange = true;
						GameManager.CardUsed = true;
						Select = true;
						FindObjectOfType<AudioManager>().Play("CardSelect");
						CanShoot = false;
						EMPbreakPlayerGO.GetComponent<PlayerCardManager>().CardGO = this.gameObject;
						CircuitAnimator.SetTrigger("ChargeUP");
						yield return new WaitForSeconds(1.8f);
						DisapperAnimator.SetTrigger("Disapper");
						Destroy(this.gameObject, 2f);
						CanShoot = true;
						//TurnSystem.DontChange = false;

					}
				}
			}
			if (PlayerGO != null)
			{
				
				if (PlayerGO.GetComponent<PlayerCardManager>().CardActive == false)
				{
					Debug.Log("Card1");
					if (this.tag == "Sheild" || this.tag == "RayGun" || this.tag == "CellNuke" || this.tag == "LeadShield" || this.tag == "Normalizer")
					{
						Debug.Log("Card2");
						if (PlayerGO.tag == "P1/Left" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P1/Right" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Left" && !TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Right" && !TurnSystem.isPlayer1turn)
						{
							TurnSystem.DontChange = true;
							GameManager.CardUsed = true;
							Debug.Log("Card in");
							Select = true;
							FindObjectOfType<AudioManager>().Play("CardSelect");
							CanShoot = false;
							PlayerGO.GetComponent<PlayerCardManager>().CardGO = this.gameObject;
							CircuitAnimator.SetTrigger("ChargeUP");
							yield return new WaitForSeconds(1.8f);
							DisapperAnimator.SetTrigger("Disapper");
							Destroy(this.gameObject, 1f);
							CanShoot = true;
						//	TurnSystem.DontChange = false;
						}
					}
					if (this.tag == "EMP")
					{
						if (PlayerGO.tag == "P1/Left" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P1/Right" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Left" && !TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Right" && !TurnSystem.isPlayer1turn)
						{
							TurnSystem.DontChange = true;
							GameManager.CardUsed = true;
							Select = true;
							FindObjectOfType<AudioManager>().Play("CardSelect");
							CanShoot = false;
							PlayerGO.GetComponent<PlayerCardManager>().CardGO = this.gameObject;
							PlayerGO.GetComponent<Shooting>().EMPbullet.GetComponent<EMPbullet>().EMPeffectTime = EffectTime;
							CircuitAnimator.SetTrigger("ChargeUP");
							yield return new WaitForSeconds(1.8f);
							DisapperAnimator.SetTrigger("Disapper");
							Destroy(this.gameObject, 1f);
							CanShoot = true;
						//	TurnSystem.DontChange = false;
						}
					}

					if (this.tag == "IceCore")
					{
						if (PlayerGO.tag == "P1/Left" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P1/Right" && TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Left" && !TurnSystem.isPlayer1turn || PlayerGO.tag == "P2/Right" && !TurnSystem.isPlayer1turn)
						{
							TurnSystem.DontChange = true;
							GameManager.CardUsed = true;
							Select = true;
							CanShoot = false;
							FindObjectOfType<AudioManager>().Play("CardSelect");
							PlayerGO.GetComponent<PlayerCardManager>().CardGO = this.gameObject;
							PlayerGO.GetComponent<Shooting>().IceCoreBullet.GetComponent<IceCore>().IceCoreEffectTime = EffectTime;
							CircuitAnimator.SetTrigger("ChargeUP");
							yield return new WaitForSeconds(1.8f);
							DisapperAnimator.SetTrigger("Disapper");
							Destroy(this.gameObject, 2f);
							CanShoot = true;
						//	TurnSystem.DontChange = false;
						}
					}

				}
			}
		}
	}
	

}
