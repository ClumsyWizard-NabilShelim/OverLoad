using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shooting : MonoBehaviour
{
	public GameObject gameManager;
	public GameObject Bullet;
	public GameObject EMPbullet;
	public GameObject IceCoreBullet;
	//[HideInInspector]
	public GameObject ray;
	public GameObject ShootPos;
	public static bool Firing = false;
    GameObject BGO;
	public GameObject ChargeUp;
	public GameObject chargeUPeffect;
	public bool BulletShot = false;
	public AudioManager audioMANAGER;

	public GameObject B;

	public float RAYPosX;
	public static bool RayOn = false;


	// Start is called before the first frame update
	void Start()
    {
		B = transform.Find("Body").gameObject;
		
		//if(ray = null)
		//{
		//	ray = this.gameObject.GetComponentInChildren<Image>();
		//}
		//ray.transform.localScale = new Vector3(0, 0, 0);
		//ray.transform.position = ShootPos.transform.position;
		//ray.transform.localScale = new Vector3(1, 1, 1);
		audioMANAGER = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

	// Update is called once per frame
	void Update()
	{
		gameManager = GameObject.FindGameObjectWithTag("GM");
		if(ray != null && B != null)
		{
			ray.transform.SetParent(B.transform);
			
		}


		if (PlayerController2.ClickPos != null && this.GetComponentInChildren<DetectClick>().Selected)
		{
			if (this.GetComponent<PlayerCardManager>().IceCoreactive == false && this.tag == "P1/Left" && PlayerController2.ClickPos.gameObject.tag == "P1/Right" && TurnSystem.isPlayer1turn || this.GetComponent<PlayerCardManager>().IceCoreactive == false && this.tag == "P1/Right" && PlayerController2.ClickPos.gameObject.tag == "P1/Left" && TurnSystem.isPlayer1turn)
			{
				this.GetComponentInChildren<DetectClick>().Selected = false;
				this.gameObject.GetComponent<Transfering>().CanTransfer = true;
			}
			else if (this.GetComponent<PlayerCardManager>().IceCoreactive == false && this.tag == "P2/Left" && PlayerController2.ClickPos.gameObject.tag == "P2/Right" && !TurnSystem.isPlayer1turn || this.GetComponent<PlayerCardManager>().IceCoreactive == false && this.tag == "P2/Right" && PlayerController2.ClickPos.gameObject.tag == "P2/Left" && !TurnSystem.isPlayer1turn)
			{
				this.GetComponentInChildren<DetectClick>().Selected = false;
				this.gameObject.GetComponent<Transfering>().CanTransfer = true;
			}
		}
	
		if (Input.GetMouseButtonDown(0))
		{
			if (this.GetComponent<PlayerCardManager>().CardGO != null && this.GetComponent<PlayerCardManager>().CardGO.GetComponent<CardSetUp>().CanShoot == true)
			{
				if (this.GetComponentInChildren<PlayerController2>().CanShoot)
				{
					if(GameManager.CardUsed == false)
					{
						StartCoroutine(Shoot());
					}

				}
			}
			if (this.GetComponent<PlayerCardManager>().CardGO == null)
			{
				if (this.GetComponentInChildren<PlayerController2>().CanShoot)
				{
					if (GameManager.CardUsed == false)
					{
						StartCoroutine(Shoot());
					}
				}
			}
		}
	}


	IEnumerator Shoot()
	{
		if (this.GetComponentInChildren<DetectClick>().Selected)
		{
			this.GetComponentInChildren<PlayerController2>().CanShoot = false;
			this.GetComponentInChildren<DetectClick>().Selected = false;
			audioMANAGER.Play("Shoot");
			Firing = true;
			BulletShot = true;
			yield return new WaitForSeconds(0.1f);
			if (this.GetComponent<PlayerCardManager>().RayActive == false && this.GetComponent<PlayerCardManager>().EMPbullletuse == false && this.GetComponent<PlayerCardManager>().IceCorebullletuse == false)
			{
				this.GetComponentInChildren<TurretAnimScript>().recoil = true;
				BGO = Instantiate(Bullet, ShootPos.transform.position, ShootPos.transform.rotation);
				BGO.GetComponent<Bullet>().damage = this.GetComponent<PlayerStats>().cells;
				yield return new WaitForSeconds(2f);
				gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
			}
			else if (this.GetComponent<PlayerCardManager>().EMPbullletuse == true)
			{
				this.GetComponentInChildren<TurretAnimScript>().recoil = true;
				Instantiate(EMPbullet, ShootPos.transform.position, ShootPos.transform.rotation);
				gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
				this.GetComponent<PlayerCardManager>().EMPbullletuse = false;
			}
			else if (this.GetComponent<PlayerCardManager>().IceCorebullletuse == true)
			{
				this.GetComponentInChildren<TurretAnimScript>().recoil = true;
				this.GetComponent<PlayerCardManager>().IceCorebullletuse = false;
				//this.GetComponent<PlayerStats>().cells += 1;
				Instantiate(IceCoreBullet, ShootPos.transform.position, ShootPos.transform.rotation);

				gameManager.GetComponentInChildren<TurnSystem>().switchTurn();

			}
			else if (this.GetComponent<PlayerCardManager>().RayActive == true)
			{
				//if(PlayerController2.ClickPos.position != transform.position)
				//{
				RayOn = true;
				yield return new WaitForSeconds(0.2f);
				FindObjectOfType<AudioManager>().Play("ChargeUpLaser");
				GameObject CUPeffect = Instantiate(chargeUPeffect, ShootPos.transform.position, ShootPos.transform.rotation);
				GameObject CUP = Instantiate(ChargeUp, ShootPos.transform.position, ShootPos.transform.rotation);
		
				yield return new WaitForSeconds(2f);
				if (GameManager.PSgo.GetComponent<PlayerCardManager>().SheildOn)
				{
					
					GameManager.PSgo.GetComponent<PlayerCardManager>().deactivateSheild();
					GameManager.PSgo.GetComponent<PlayerCardManager>().TACA = GameManager.PSgo.GetComponent<PlayerCardManager>().cardGOeffectTimeValue;
					GameObject g = Instantiate(gameManager.GetComponent<GameManager>().sheildBreak, GameManager.PSgo.GetComponent<PlayerCardManager>().SheildGOvalue.transform.position, Quaternion.identity);
					Destroy(GameManager.PSgo.GetComponent<PlayerCardManager>().SheildGOvalue);
					Destroy(g, 5f);
				}
				Destroy(CUP);
				Destroy(CUPeffect);
				ray.gameObject.SetActive(true);
				FindObjectOfType<AudioManager>().Play("LaserShoot");
				//Ray.SetPosition(1, new Vector3(PlayerController2.ClickPos.transform.localPosition.x+100, 0, 0));
				//ray.transform.position = ShootPos.transform.position;
				//ray.rectTransform.SetParent(ShootPos.transform);
				if(this.tag == "P1/Left" || this.tag == "P1/Right")
				{
					ray.transform.position = ShootPos.transform.position;
					ray.transform.localPosition = new Vector3(RAYPosX, ShootPos.transform.localPosition.y, ShootPos.transform.localPosition.x);
				}

				if (this.tag == "P2/Left" || this.tag == "P2/Right")
				{
					ray.transform.localPosition = new Vector3(RAYPosX, ShootPos.transform.localPosition.y, ShootPos.transform.localPosition.x);
				}
				ray.transform.localScale = new Vector2(/*PlayerController2.ClickPos.transform.localPosition.x*/ 30, 0.04f);
			//	ray.transform.localPosition = new Vector2(/*P*//*layerController2.ClickPos.transform.localPosition.x*/ray.transform.localPosition.x + 4, ray.transform.localPosition.y
				yield return new WaitForSeconds(1f);
				gameManager.GetComponentInChildren<TurnSystem>().switchTurn();
				yield return new WaitForSeconds(0.7f);
				ray.gameObject.SetActive(false);
				FindObjectOfType<AudioManager>().Stop("LaserShoot");

		
				GameManager.PSgo.GetComponent<PlayerStats>().OverLoad(2);
				//	PlayerController2.ClickPos.gameObject.GetComponent<PlayerStats>().OverLoad(this.GetComponent<PlayerCardManager>()._Damagae);
				this.GetComponent<PlayerCardManager>().RayActive = false;
				//}
			}
			this.GetComponentInChildren<DetectClick>().Selected = false;
			Firing = false;
			BulletShot = false;
			RayOn = false;
		}
	}

}
