using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCore : MonoBehaviour
{
	public float Speed;
	GameManager GM;
	public GameObject IceCoreEffect;
	GameObject OnhitGO;
	GameObject HitCELL;
	GameObject EffectPos;
	public int IceCoreEffectTime;
	bool CanMove;
	bool IsPlayer1;
	bool IsPlayer2;
	public GameObject iceOrbExplode;

	bool isHit;
	// Start is called before the first frame update
	void Start()
	{
		FindObjectOfType<AudioManager>().Play("BulletMovement");
		CanMove = true;
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>(); ;
	}

	// Update is called once per frame
	void Update()
	{
		if (CanMove)
		{
			StartCoroutine(ActivateBullet());
			transform.Translate(Vector3.right * Speed * Time.deltaTime);
		}

	}

	IEnumerator ActivateBullet()
	{
		yield return new WaitForSeconds(0.1f);
		this.GetComponent<CircleCollider2D>().enabled = true;
		yield return new WaitForSeconds(2f);
		if (!isHit)
		{
			Destroy(gameObject);
		}

		FindObjectOfType<AudioManager>().Stop("BulletMovement");
	}

	void OnTriggerEnter2D(Collider2D HitGO)
	{
		if (HitGO.tag == "P1/Right" || HitGO.tag == "P1/Left")
		{
			OnhitGO = HitGO.gameObject;
			IsPlayer1 = true;
			isHit = true;
		}

		if (HitGO.tag == "P2/Right" || HitGO.tag == "P2/Left")
		{
			OnhitGO = HitGO.gameObject;
			IsPlayer2 = true;
			isHit = true;
		}
	
		if (HitGO.tag == "CellHolder")
		{
			HitCELL = HitGO.gameObject;
			EffectPos = HitCELL.transform.Find("IceCorePos").gameObject;
			isHit = true;
		}
		if(HitGO.tag == "Sheild")
		{
			GameObject g = Instantiate(iceOrbExplode, transform.position, Quaternion.identity);
			Destroy(g, 2f);
			HitGO.GetComponent<Animator>().SetTrigger("SheildHit");
			FindObjectOfType<AudioManager>().Play("SheildHit");
			Destroy(gameObject);
		}
		FindObjectOfType<AudioManager>().Stop("BulletMovement");
		HIT();
	}

	void HIT()
	{
		isHit = true;
		if (IsPlayer1)
		{
			GM.P1LselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = true;
			GM.P1RselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = true;
		}
		if (IsPlayer2)
		{
			GM.P2LselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = true;
			GM.P2RselectedGO.GetComponent<PlayerCardManager>().IceCoreactive = true;
		}
		OnhitGO.GetComponentInParent<PlayerCardManager>().cardGOeffectTimeValue = IceCoreEffectTime;
		OnhitGO.GetComponentInParent<PlayerCardManager>().CardActive = true;
		GM.CamShake();
		if (HitCELL != null)
		{
			isHit = true;
			CanMove = false;
			transform.position = HitCELL.transform.position;
			FindObjectOfType<AudioManager>().Stop("BulletMovement");
			OnhitGO.GetComponentInParent<PlayerCardManager>().IceCorebullletuse = false;
			GameObject effect = Instantiate(IceCoreEffect, EffectPos.transform.position, EffectPos.transform.rotation);
			effect.transform.parent = EffectPos.transform;
			OnhitGO.GetComponentInParent<PlayerCardManager>().IceCoreeffect = effect;
			OnhitGO.GetComponentInParent<PlayerCardManager>().IceOrb = this.gameObject;
		}
	}
}
