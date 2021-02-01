using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardManager : MonoBehaviour
{
	[Header("Turn Set Up")]
	GameObject gameManager;
	public bool CardActive;
	public int TACA;  //Turns passed After Card is Active
	public bool ThisTurn;

	[HideInInspector]
	public GameObject CardGO;
	[HideInInspector]
    public int CardGoEffectTime;
	[HideInInspector]
	public int cardGOeffectTimeValue;
	[HideInInspector]
	public GameObject CardGOvalue;
	[HideInInspector]
	public int _Damagae;


	public bool use;

	[Header("Sheild")]
	Animator SheildAnimator;
	public Transform Spawnpoint;
	public GameObject SheildGO;
	[HideInInspector]
	public GameObject SheildGOvalue;
	[HideInInspector]
	public PolygonCollider2D SheildCollider;
	[HideInInspector]
	public bool SheildOn;

	[Header("EMP")]
	public bool EMPactive;
	public bool EMPbullletuse;
	[HideInInspector]
	public GameObject EMpeffect;
	//[HideInInspector]
	public GameObject EMPorb;
	public GameObject EMPDestroyparticleEffect;

	[Header("Ray Gun")]
	public bool RayActive;

	[Header("Ice Core")]
	public bool IceCoreactive;
	public bool IceCorebullletuse;
	[HideInInspector]
	public GameObject IceCoreeffect;
	[HideInInspector]
	public GameObject IceOrb;
	public GameObject IceOrbDestroyparticleEffect;

	[Header("CellNuke")]
	public GameObject CellNukeEffect;
	GameObject FlashScreen;
	Animator FlashScreenAnimator;
	[ColorUsage(false)]
	public Color NukedSkyColor;

	[Header("Anti EMP")]
	public GameObject EmpBreakEffect;


	[Header("Lead Shield")]
	public GameObject LeadShield;
	GameObject LeadShieldValue;
	//[HideInInspector]
	public bool LeadShieldOn;
	public GameObject LeadPos;

	[Header("Normalizer")]
	[HideInInspector]
	public bool IsNormalize;

	Camera cam;
	void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag("GM");
		FlashScreen = gameManager.GetComponent<GameManager>().FlashScreen;
        cam = Camera.main;
		FlashScreenAnimator = FlashScreen.GetComponent<Animator>();
		cam.backgroundColor = cam.GetComponentInParent<SkyColor>().NormalSKY;
		NukedSkyColor = cam.GetComponentInParent<SkyColor>().NukeSKY;
		ThisTurn = TurnSystem.isPlayer1turn;
		FlashScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(LeadShieldOn == false && LeadShieldValue != null)
		{
			Destroy(LeadShieldValue);
		}

		if (IsNormalize)
		{
			Normalizer();
		}

		if (EMPactive == true)
		{
			this.GetComponentInChildren<DetectClick>().Selected = false;
		}
		if(CardGO != null/* && !use*/)
		{
			CardGoEffectTime = CardGO.GetComponent<CardSetUp>().EffectTime;
			CardActive = true;
			CardGO.GetComponent<CardSelect>().CardUsed = true;
			_Damagae = CardGO.GetComponent<CardSetUp>().Damage;
			CardGOvalue = CardGO;
			
			if (IceCoreactive != true)
			{
				CardActive = true;
			}
			if (CardGO.tag == "Sheild")
			{
				Shield();
			//	use = true;
			}
			else if (CardGO.tag == "EMP")
			{
				EMP();
			//	use = true;
			}
			else if (CardGO.tag == "RayGun")
			{
				RayGunActivate();
				//use = true;
			}
			else if (CardGO.tag == "IceCore")
			{
				IceCore();
			//	use = true;
			}
			else if(CardGO.tag == "CellNuke")
			{
			
				StartCoroutine(CellNuke());
				//use = true;
			}
			else if (CardGO.tag == "AntiEMP" && EMPactive == true)
			{
				StartCoroutine(AntiEMP());
				//use = true;
			}else if(CardGO.tag == "LeadShield")
			{
				ActivateLeadShield();
				CardActive = false;
				TACA = 0;
				if (CardGOvalue != null)
				{
					CardGOvalue.GetComponent<CardSelect>().CardUsed = false;
				}
			}else if(CardGO.tag == "Normalizer")
			{
				IsNormalize = true;
			}
		}
		
		if (CardActive)
		{
			Debug.Log(cardGOeffectTimeValue);
			TurnCount();
			if (TACA == cardGOeffectTimeValue)
			{
			    deactivateSheild();
				EMPDeactivate();
				IceCoreDeactivate();
				RayGunDeactivate();
				CardActive = false;
				TACA = 0;
				if (CardGOvalue != null)
				{
					CardGOvalue.GetComponent<CardSelect>().CardUsed = false;
				}

			}
		}
		else
		{
			ThisTurn = TurnSystem.isPlayer1turn;
		}
		//if (SheildCollider != null)
		//{
		//	if (SheildGOvalue.transform.parent.tag == "P1/Left" && TurnSystem.isPlayer1turn || SheildGOvalue.transform.parent.tag == "P1/Right" && TurnSystem.isPlayer1turn || SheildGOvalue.transform.parent.tag == "P2/Left" && !TurnSystem.isPlayer1turn || SheildGOvalue.transform.parent.tag == "P2/Right" && !TurnSystem.isPlayer1turn)
		//    {
		//		SheildCollider.enabled = false;
		//	}
		//	else
		//	{
		//		SheildCollider.enabled = true;
		//	}
		//}

		//if(this.tag == "P1/Left")
		//{
		//	gameManager.GetComponent<GameManager>().P1LeftDown();
		//}
		//if (this.tag == "P1/Right")
		//{
		//	gameManager.GetComponent<GameManager>().P1RightDown();
		//}
		//if (this.tag == "P2/Left")
		//{
		//	gameManager.GetComponent<GameManager>().P2LeftDown();
		//}
		//if (this.tag == "P2/Right")
		//{
		//	gameManager.GetComponent<GameManager>().P2RightDown();
		//}
		if (EMPactive)
		{
			this.GetComponentInChildren<TurretAnimScript>().canRotate = true;

			if(this.GetComponentInChildren<TurretAnimScript>().canRotate == true)
			{
				this.GetComponentInChildren<TurretAnimScript>().RotateActive = true;
			}
	
		}

		if (!EMPactive && this.GetComponent<PlayerStats>().cells > 0)
		{
			this.GetComponentInChildren<TurretAnimScript>().RotateActive = false;
			this.GetComponentInChildren<TurretAnimScript>().canRotate = false;
		}
		if(GameOver.Dead)
		{
			deactivateSheild();
			EMPDeactivate();
			IceCoreDeactivate();
			RayGunDeactivate();
			GameOver.Dead = false;
		}
	}


	#region TURN count
	void TurnCount()
	{
		if (ThisTurn != TurnSystem.isPlayer1turn)
		{
			TACA++;
			ThisTurn = TurnSystem.isPlayer1turn;
		}
	}
	#endregion

	#region Cell Nuke
	IEnumerator CellNuke()
	{
		CardGO = null;

		if (this.tag == "P1/Left" || this.tag == "P1/Right")
		{
			if (GameManager.isRanger_P2)
			{	
				CardSetUp.ActivateLeadShield_P2 = true;
			}
		}


		if (this.tag == "P2/Left" || this.tag == "P2/Right")
		{
			if (GameManager.isRanger_P1)
			{
				CardSetUp.ActivateLeadShield_P1 = true;
			}
		}

		GameObject Effect = Instantiate(CellNukeEffect, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(1f);
		gameManager.GetComponent<GameManager>().NukeCamShakeActivate();
		yield return new WaitForSeconds(1.9f);
		FlashScreen.SetActive(true);
		FlashScreenAnimator.SetTrigger("Flash");
		yield return new WaitForSeconds(1f);
		//if (gameManager.GetComponent<GameManager>().BG.GetComponent<BackGroundAttributes>().nukeChange)
		//{
			gameManager.GetComponent<GameManager>().BG.SetActive(false);
			gameManager.GetComponent<GameManager>().BG.GetComponent<BackGroundAttributes>().NuKedVersion_BG.SetActive(true);
		//}

		cam.backgroundColor = NukedSkyColor;
		if (!gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn)
		{
			gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerStats>().cells += _Damagae;
		}
		if (!gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn)
		{
			gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerStats>().cells += _Damagae;
		}
		if (!gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn)
		{
			gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerStats>().cells += _Damagae;
		}
		if (!gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn)
		{
			gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerStats>().cells += _Damagae;
		}

		gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn = false;
		gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn = false;
		gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn = false;
		gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerCardManager>().LeadShieldOn = false;
		gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerCardManager>().IceCoreDeactivate();
		gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerCardManager>().IceCoreDeactivate();
		gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerCardManager>().IceCoreDeactivate();
		gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerCardManager>().IceCoreDeactivate();

		gameManager.GetComponent<GameManager>().NukeCamShakeDectivate();
		Destroy(Effect);
		yield return new WaitForSeconds(1f);
		FlashScreen.SetActive(false);
		
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
		GameManager.CardUsed = false;
		//use = false;
	}
	#endregion

	#region SHEILD
	void Shield()
	{
		SheildActivate();
	}
	void SheildActivate()
	{
		CardGO = null;
		SheildOn = true;
		cardGOeffectTimeValue = CardGoEffectTime +1 ;
		SheildGOvalue = Instantiate(SheildGO, Spawnpoint.position, Spawnpoint.rotation);
		SheildAnimator = SheildGOvalue.GetComponent<Animator>();
		SheildAnimator.SetTrigger("Sheild");
		SheildCollider = SheildGOvalue.GetComponent<PolygonCollider2D>();
		SheildGOvalue.transform.parent = transform.Find("Body").transform;
		if (this.tag == "P2/Left" || this.tag == "P2/Right")
		{
			SheildGOvalue.transform.localRotation = Quaternion.Euler(-180, 0, 0f);
		}
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
		//use = false;
		GameManager.CardUsed = false;
	}

	public void deactivateSheild()
	{
		if(SheildAnimator != null)
		{
			SheildAnimator.SetTrigger("SheildClose");
		}
		Destroy(SheildGOvalue, 5);
		SheildOn = false;
	}
	#endregion

	#region EMP
	void EMP()
	{
		EMPbullletuse = true;
		CardActive = false;
		CardGO = null;
		GameManager.CardUsed = false;
		//use = false;
	}

	void EMPDeactivate()
	{
		EMPactive = false;
		EMPbullletuse = false;
		Destroy(EMpeffect);
		if (EMPorb != null)
		{
			GameObject effect = Instantiate(EMPDestroyparticleEffect, EMPorb.transform.position, Quaternion.identity);
			Destroy(effect, 5f);
			Destroy(EMPorb);
		}
		
	}
	#endregion

	#region IceCore
	void IceCore()
	{
		IceCorebullletuse = true;
		CardActive = false;
		CardGO = null;
		GameManager.CardUsed = false;
		//	use = false;
	}

	void IceCoreDeactivate()
	{
		if(this.tag == "P1/Left" || this.tag == "P1/Right")
		{
			gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = false;
			gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = false;
		}

		if (this.tag == "P2/Left" || this.tag == "P2/Right")
		{
			gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = false;
			gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = false;
		}
		IceCoreactive = false;
		Destroy(IceCoreeffect);
		if (IceOrb != null)
		{
			GameObject effect = Instantiate(IceOrbDestroyparticleEffect, IceOrb.transform.position, Quaternion.identity);
			Destroy(effect, 5f);
		}

		Destroy(IceOrb);
	}
	#endregion

	#region Ray Gun

	void RayGunActivate()
	{
		RayActive = true;
		cardGOeffectTimeValue = CardGoEffectTime;
		GameManager.CardUsed = false;
		//	use = false;
	}

	void RayGunDeactivate()
	{
		RayActive = false;
	}
	#endregion

	#region Anti EMP
	IEnumerator AntiEMP()
	{
		GameObject effect2 = Instantiate(EmpBreakEffect, EMPorb.transform.position, Quaternion.identity);	
		EMPactive = false;
		Destroy(EMpeffect);
		if (EMPorb != null)
		{
			yield return new WaitForSeconds(1.2f);
			GameObject effect = Instantiate(EMPDestroyparticleEffect, EMPorb.transform.position, Quaternion.identity);
			Destroy(effect, 5f);
		}
		Destroy(effect2);
		Destroy(EMPorb);
		//use = false;
		yield return new WaitForSeconds(1.2f);
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
		GameManager.CardUsed = false;
	}
	#endregion

	#region Lead Shield Activate
	public void ActivateLeadShield()
	{
		CardActive = false;
		CardGO = null;
		if (LeadShieldValue == null)
		{
			FindObjectOfType<AudioManager>().Play("Lead");
			Debug.Log("anim? lead");
			LeadShieldValue = Instantiate(LeadShield, LeadPos.transform.position, LeadPos.transform.rotation);
		}
		
		LeadShieldValue.transform.parent = LeadPos.transform;
		LeadShieldOn = true;
		GameManager.CardUsed = false;
	}

	#endregion

	#region Normalizer
	void Normalizer()
	{
		CardActive = false;
		CardGO = null;
		if (this.tag == "P1/Left" || this.tag == "P1/Right")
		{
			gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerStats>().cells = 2;
			gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerStats>().cells = 2;
			if (gameManager.GetComponent<GameManager>().P1LselectedGO.GetComponent<PlayerStats>().cells == 2 && gameManager.GetComponent<GameManager>().P1RselectedGO.GetComponent<PlayerStats>().cells == 2)
			{
				IsNormalize = false;
			}
		}
		if (this.tag == "P2/Left" || this.tag == "P2/Right")
		{
			gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerStats>().cells = 2;
			gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerStats>().cells = 2;
			if (gameManager.GetComponent<GameManager>().P2LselectedGO.GetComponent<PlayerStats>().cells == 2 && gameManager.GetComponent<GameManager>().P2RselectedGO.GetComponent<PlayerStats>().cells == 2)
			{
				IsNormalize = false;
			}
		}
		gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
		GameManager.CardUsed = false;
	}

	#endregion region


}
