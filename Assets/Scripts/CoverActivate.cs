using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverActivate : MonoBehaviour
{
	public bool Activate;
	public bool Deactivate;
	public GameObject PTransfer;
	public static bool CanPassTime = true;
	[Header("Player1 Cover")]
	public GameObject P1Cards;
	public GameObject P1Cover;
	public Animator P1coverAnimator;


	[Header("Player2 Cover")]
	public GameObject P2Cards;
	public GameObject P2Cover;
	public Animator P2coverAnimator;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (TurnSystem.isPlayer1turn)
		{
			if (Activate == true)
			{
				CanPassTime = false;
				StartCoroutine(PlayerOneCoverActivate());
			}

			if(Deactivate == true)
			{
				CanPassTime = true;
				Debug.Log("Done");
				StartCoroutine(PlayerOneCoverDeactivate());
			}
		}

		if (!TurnSystem.isPlayer1turn)
		{
			if (Activate == true)
			{
				CanPassTime = false;
				StartCoroutine(PlayerTwoCoverActivate());
			}

			if (Deactivate == true)
			{
				CanPassTime = true;
				Debug.Log("Done");
				StartCoroutine(PlayerTwoCoverDeactivate());
			}
		}
	}

	IEnumerator PlayerOneCoverActivate()
	{
		Activate = false;
		P1Cover.SetActive(true);
		P1coverAnimator.SetTrigger("Open");
		yield return new WaitForSeconds(1f);
		P1Cards.SetActive(false);
		PTransfer.SetActive(true);
		yield return new WaitForSeconds(1f);
		P1Cover.SetActive(false);
	}

	IEnumerator PlayerOneCoverDeactivate()
	{
		Deactivate = false;
		P1Cover.SetActive(true);
		P1coverAnimator.SetTrigger("Open");
		yield return new WaitForSeconds(1f);
		P1Cards.SetActive(true);
		PTransfer.SetActive(false);
		yield return new WaitForSeconds(1f);
		P1Cover.SetActive(false);
	}

	IEnumerator PlayerTwoCoverActivate()
	{
		Activate = false;
		P2Cover.SetActive(true);
		P2coverAnimator.SetTrigger("Open");
		yield return new WaitForSeconds(1f);
		P2Cards.SetActive(false);
		PTransfer.SetActive(true);
		yield return new WaitForSeconds(1f);
		P2Cover.SetActive(false);
	}

	IEnumerator PlayerTwoCoverDeactivate()
	{
		Deactivate = false;
		P2Cover.SetActive(true);
		P2coverAnimator.SetTrigger("Open");
		yield return new WaitForSeconds(1f);
		P2Cards.SetActive(true);
		PTransfer.SetActive(false);
		yield return new WaitForSeconds(1f);
		P2Cover.SetActive(false);
	}
}
