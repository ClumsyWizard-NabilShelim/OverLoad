using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public int cells = 1;

	public GameObject[] CellGO;

	public SpriteRenderer[] rend;

	public GameObject gameManager;

	bool R = false;

	void Awake()
	{
	}
	void start()
	{
		cells = 1;
	}
	void Update()
	{
		for (int i = 0; i < CellGO.Length; i++)
		{
			if (i < cells)
			{
				CellGO[i].SetActive(true);
			}
			else
			{
				CellGO[i].SetActive(false);
			}
		}

		if (cells >= 5 || cells <= 0)
		{
			cells = 0;
			this.GetComponentInChildren<TurretAnimScript>().canRotate = true;
			if (this.GetComponentInChildren<TurretAnimScript>().canRotate == true)
			{
				this.GetComponentInChildren<TurretAnimScript>().RotateActive = true;
			}
			this.GetComponentInChildren<TurretAnimScript>().RotateActive = true;
			this.GetComponentInChildren<PlayerController2>().enabled = false;
			this.GetComponent<Shooting>().enabled = false;
			rend[0].color = Color.grey;
			rend[1].color = Color.grey;
			R = true;
		}

		if (cells > 0)
		{
			//A.enabled = false;
			//this.GetComponent<DetectClick>().enabled = true;
			transform.GetComponent<AudioSource>().enabled = false;
			this.GetComponentInChildren<PlayerController2>().enabled = true;
			this.GetComponent<Shooting>().enabled = true;

			if (R)
			{
				if(this.tag == "P1/Left" || this.tag == "P1/Right")
				{
					transform.Find("Body").localRotation = Quaternion.Euler(0, 0, 0);
				}
				if(this.tag == "P2/Left" || this.tag == "P2/Right")
				{
					transform.Find("Body").localRotation = Quaternion.Euler(0, 0, -180);
				}
				R = false;
			}

			rend[0].color = Color.white;
			rend[1].color = Color.white;
		}
	}



	public void OverLoad(int amount)
	{
		if (!GameOver.isDead)
		{
			if(cells + amount <= 5)
			{
				cells += amount;
			}
		
		}
		
	}
}
