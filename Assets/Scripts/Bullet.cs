using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float Speed;
	public int damage;
	public GameObject Effect;
	public GameObject gameManager;
	GameManager GM;
	//public static PowerUpManager powermanager;
	public Animator CamAnimator;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("Damage : " + damage);
		this.GetComponent<CircleCollider2D>().enabled = false;
		FindObjectOfType<AudioManager>().Play("BulletMovement");
		gameManager = GameObject.FindGameObjectWithTag("GM");
		GM = gameManager.GetComponent<GameManager>();
		CamAnimator = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		StartCoroutine(ActivateBullet());
		transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

	IEnumerator ActivateBullet()
	{
		yield return new WaitForSeconds(0.1f);
		this.GetComponent<CircleCollider2D>().enabled = true;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
		FindObjectOfType<AudioManager>().Stop("BulletMovement");
	}
	void OnTriggerEnter2D(Collider2D HitGO)
	{
		HIT();
		if(HitGO.tag != "Sheild")
		{
			if (HitGO.transform.parent.tag == "P1/Left" || HitGO.transform.parent.tag == "P1/Right" || HitGO.transform.parent.tag == "P2/Left" || HitGO.transform.parent.tag == "P2/Right")
			{
				if (HitGO.GetComponentInParent<PlayerStats>().cells + damage < 6)
				{

					HitGO.GetComponentInParent<PlayerStats>().OverLoad(damage);
				}
			}
		}
	
        if (HitGO.tag == "Sheild")
		{
			HitGO.GetComponentInParent<Animator>().SetTrigger("SheildHit");
			FindObjectOfType<AudioManager>().Play("SheildHit");
		}
	}

	void HIT()
	{
		FindObjectOfType<AudioManager>().Stop("BulletMovement");
		FindObjectOfType<AudioManager>().Play("HIT");
		GM.CamShake();
		GameObject effect = Instantiate(Effect, transform.position, transform.rotation);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}
}
