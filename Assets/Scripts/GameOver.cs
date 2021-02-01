using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	int EXgo;
	public PlayerStats[] P;
	public GameObject ExplosionGameobject;
	public GameObject ExplosionSpawnCentreP1;
	public GameObject ExplosionSpawnCentreP2;
	public GameObject ScreenP1;
	public GameObject ScreenP2;
	public GameObject[] SpawnPointP1;
	public GameObject[] SpawnPointP2;
	public Animator P1Ship;
	public Animator P2Ship;
	public Animator WinScreen;
	public float canspawnValue;
	float canspawn;
	public bool CanExplode = true;
	public float FlashTimeValue;
	float FlashTime;
	int FlashAmount;
	bool canflash = true;

	public static bool Dead = false;
	GameManager GM;
	[Header("Win_Text")]
	public TextMeshPro WinnerNameDisplayText;
	public TMP_InputField P1Name;
	public TMP_InputField P2Name;


	public static bool isDead;
	// Start is called before the first frame update
	void Start()
	{
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		isDead = false;
		P1Name.characterLimit = 10;
		P2Name.characterLimit = 10;
		canspawn = 0;
		FlashTime = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (GM != null)
		{
			if (GM.P1LselectedGO != null)
			{
				P[0] = GM.P1LselectedGO.GetComponent<PlayerStats>();

			}
			if (GM.P1RselectedGO != null)
			{
				P[1] = GM.P1RselectedGO.GetComponent<PlayerStats>();
			}
			if (GM.P2LselectedGO != null)
			{
				P[2] = GM.P2LselectedGO.GetComponent<PlayerStats>();
			}
			if (GM.P2RselectedGO != null)
			{
				P[3] = GM.P2RselectedGO.GetComponent<PlayerStats>();
			}

		}
		if (P[0].cells == 0 && P[1].cells == 0)
		{
			//Player1 death
			StartCoroutine(P1Death());
		}
		else if (P[2].cells == 0 && P[3].cells == 0)
		{
			//player2 death
			StartCoroutine(P2Death());
		}

		if(P[0].cells == 0 && P[1].cells == 0 && P[2].cells == 0 && P[3].cells == 0)
		{
			StartCoroutine(P2Death());
			StartCoroutine(P1Death());
			WinnerNameDisplayText.text = "Draw";
		}

	}

	IEnumerator P1Death()
	{
		isDead = true;
		if (P[2].cells != 0 || P[3].cells != 0)
		{
			if (P2Name.text == "")
			{
				WinnerNameDisplayText.text = "Player 2";
			}
			else
			{
				WinnerNameDisplayText.text = P2Name.text;
			}
		}
		
		Dead = true;
		if (CanExplode)
		{
			if (canflash && FlashAmount != 10)
			{
				if (FlashTime <= 0)
				{
					FindObjectOfType<AudioManager>().Play("Blip");
					ScreenP1.SetActive(true);
					FlashTime = FlashTimeValue;
					FlashAmount++;
				}
				else
				{
					//FindObjectOfType<AudioManager>().Stop("Blip");
					ScreenP1.SetActive(false);
					FlashTime -= Time.deltaTime;
				}

				//	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	yield return new WaitForSeconds(0.1f);
				//	ScreenP1.SetActive(false);
				//	canflash = false;
			}
			if (FlashAmount == 10)
			{
				ScreenP1.SetActive(false);
				canflash = false;
			}
			if (EXgo != 5 && FlashAmount == 10)
			{
				if (canspawn <= 0)
				{
					Instantiate(ExplosionGameobject, SpawnPointP1[Random.Range(0, SpawnPointP1.Length - 1)].transform.position, Quaternion.identity);
					canspawn = canspawnValue;
					EXgo++;
				}
				else
				{
					canspawn -= Time.deltaTime;
				}
			}
			//yield return new WaitForSeconds(1f);
			if (EXgo == 5)
			{
				P1Ship.SetTrigger("ShipOut");
				yield return new WaitForSeconds(2f);
				WinScreen.SetTrigger("Win");
				CanExplode = false;
				canflash = true;
			}

		}
	}

	IEnumerator P2Death()
	{
		isDead = true;
		if (P[0].cells != 0 || P[1].cells != 0)
		{
			if (P1Name.text == "")
			{
				WinnerNameDisplayText.text = "Player 1";
			}
			else
			{
				WinnerNameDisplayText.text = P1Name.text;
			}
		}
		Dead = true;
		if (CanExplode)
		{
			if (canflash && FlashAmount != 10)
			{
				if (FlashTime <= 0)
				{
					FindObjectOfType<AudioManager>().Play("Blip");
					ScreenP2.SetActive(true);
					FlashTime = FlashTimeValue;
					FlashAmount++;
				}
				else
				{
					//FindObjectOfType<AudioManager>().Stop("Blip");
					ScreenP2.SetActive(false);
					FlashTime -= Time.deltaTime;
				}

				//	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	yield return new WaitForSeconds(0.1f);
				////	FindObjectOfType<AudioManager>().Stop("Blip");
				//	ScreenP1.SetActive(false);
				//	yield return new WaitForSeconds(0.1f);
				//	//FindObjectOfType<AudioManager>().Play("Blip");
				//	ScreenP1.SetActive(true);
				//	//FindObjectOfType<AudioManager>().Stop("Blip");
				//	yield return new WaitForSeconds(0.1f);
				//	ScreenP1.SetActive(false);
				//	canflash = false;
			}
			if (FlashAmount == 10)
			{
				ScreenP2.SetActive(false);
				canflash = false;
			}
			if (EXgo != 5 && FlashAmount == 10)
			{
				if (canspawn <= 0)
				{
					Instantiate(ExplosionGameobject, SpawnPointP2[Random.Range(0, SpawnPointP2.Length - 1)].transform.position, Quaternion.identity);
					canspawn = canspawnValue;
					EXgo++;
				}
				else
				{
					canspawn -= Time.deltaTime;
				}
			}
			//yield return new WaitForSeconds(1f);
			if (EXgo == 5)
			{
				P2Ship.SetTrigger("ShipOut");
				yield return new WaitForSeconds(2f);
				WinScreen.SetTrigger("Win");
				CanExplode = false;
				canflash = true;
			}

		}

	}
}
