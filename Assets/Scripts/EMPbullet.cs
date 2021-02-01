using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPbullet : MonoBehaviour
{
	public float Speed;
	GameManager GM;
	public GameObject EMPeffect;
	GameObject OnhitGO;
	GameObject HitCELL;
	public int EMPeffectTime;
	Animator EmpAnimator;
	bool CanMove;
	public GameObject EmpHitEffect;
    // Start is called before the first frame update
    void Start()
    {
		FindObjectOfType<AudioManager>().Play("BulletMovement");
		CanMove = true;
		EmpAnimator = GetComponent<Animator>();
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>(); ;
    }

    // Update is called once per frame
    void Update()
    {
		if (CanMove)
		{
			StartCoroutine(ActivateBulllet());
			transform.Translate(Vector3.right * Speed * Time.deltaTime);
		}
		
	}

	IEnumerator ActivateBulllet()
	{
		yield return new WaitForSeconds(0.1f);
		this.GetComponent<CircleCollider2D>().enabled = true;
		yield return new WaitForSeconds(2f);
		//Destroy(gameObject);
		FindObjectOfType<AudioManager>().Stop("BulletMovement");
	}
	void OnTriggerEnter2D(Collider2D HitGO)
	{
		if(HitGO.tag == "P1/Right" || HitGO.tag == "P1/Left" || HitGO.tag == "P2/Right" || HitGO.tag == "P2/Left")
		{
			OnhitGO = HitGO.gameObject;
			HIT();
		}
		//else
		//{
		//	GameObject g = Instantiate(EmpHitEffect, transform.position, Quaternion.identity);
		//	Destroy(g, 2f);
		//	FindObjectOfType<AudioManager>().Play("SheildHit");
		//	Destroy(gameObject);
		//}

		if (HitGO.tag == "CellHolder")
		{
			HitCELL = HitGO.gameObject;
			HIT();
		}
		//else
		//{
		//	GameObject g = Instantiate(EmpHitEffect, transform.position, Quaternion.identity);
			
		//	FindObjectOfType<AudioManager>().Play("SheildHit");
		//	Destroy(gameObject);
		//}
		if (HitGO.tag == "Sheild")
		{
			HitGO.GetComponent<Animator>().SetTrigger("SheildHit");
			FindObjectOfType<AudioManager>().Play("SheildHit");
			GameObject g = Instantiate(EmpHitEffect, transform.position, Quaternion.identity);
			Destroy(g, 2f);
			Destroy(gameObject);
		}
		FindObjectOfType<AudioManager>().Stop("BulletMovement");
	}

	void HIT()
	{
		//FindObjectOfType<AudioManager>().Play("TurretPowerDown");
		if(OnhitGO != null)
		{
			OnhitGO.GetComponentInParent<PlayerCardManager>().EMPactive = true;
			OnhitGO.GetComponentInParent<PlayerCardManager>().cardGOeffectTimeValue = EMPeffectTime;
			OnhitGO.GetComponentInParent<PlayerCardManager>().CardActive = true;
		}
	
		GM.CamShake();
		if(HitCELL != null)
		{
			CanMove = false;
			transform.position = HitCELL.transform.position;
			FindObjectOfType<AudioManager>().Stop("BulletMovement");
			EmpAnimator.SetTrigger("EMP");
			GameObject effect = Instantiate(EMPeffect, transform.position, transform.rotation);
			OnhitGO.GetComponentInParent<PlayerCardManager>().EMpeffect = effect;
			OnhitGO.GetComponentInParent<PlayerCardManager>().EMPorb = this.gameObject;
		}
	}
}
