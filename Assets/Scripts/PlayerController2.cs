using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
	public static Transform ClickPos;
	[HideInInspector]
	public float Offset;
	[HideInInspector]
	public string Thistag;

	public int ClickCount;
	bool turn;
	public bool canTurn;
	GameManager GM;

	GameObject G;

	public float minZ;
	public float maxZ;

	[HideInInspector]
	public bool CanShoot;
	float RotZ;

	[HideInInspector]
	public bool IsActiveThisGO = false;
	// Start is called before the first frame update
	void Start()
	{
		if(this.transform.parent.tag == "P1/Left" || this.transform.parent.tag == "P1/Right")
		{
			minZ = 345;
			maxZ = 15f;
		}

		if (this.transform.parent.tag == "P2/Left" || this.transform.parent.tag == "P2/Right")
		{
			minZ = 170;
			maxZ = 190;
		}

		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		Thistag = this.tag;
	}

	// Update is called once per frame
	void Update()
	{
		G = GM.RotateGameObject;
		if(this.tag == "P1/Left")
		{
			GM.P1LselectedGO = this.transform.parent.gameObject;
		}

		if (this.tag == "P1/Right")
		{
			GM.P1RselectedGO = this.transform.parent.gameObject;
		}

		if (this.tag == "P2/Left")
		{
			GM.P2LselectedGO = this.transform.parent.gameObject;
		}

		if (this.tag == "P2/Right")
		{
			GM.P2RselectedGO = this.transform.parent.gameObject;
		}
		//if (Input.GetMouseButton(0))
		//{

		//	if (GM.RotateGameObject != null && GM.RotateGameObject.transform.position != transform.position && this.GetComponentInParent<DetectClick>().Selected == true && this.GetComponentInParent<PlayerStats>().cells != 0)
		//	{

		//		if (this.tag == "P1/Left" && GM.RotateGameObject.tag != "P1/Right" && TurnSystem.isPlayer1turn || this.tag == "P1/Right" && GM.RotateGameObject.tag != "P1/Left" && TurnSystem.isPlayer1turn)
		//		{

		//			PositionTurret();
		//		}
		//		if (this.tag == "P2/Left" && GM.RotateGameObject.tag != "P2/Right" && !TurnSystem.isPlayer1turn || this.tag == "P2/Right" && GM.RotateGameObject.tag != "P2/Left" && !TurnSystem.isPlayer1turn)
		//		{
		//			Debug.Log("P2");
		//			PositionTurret2();
		//		}
		//	}
		//}

		//if (this.transform.parent.tag == "P1/Left" && this.GetComponent<DetectClick>().Selected == true && GM.RotateGameObject.tag != "P1/Left")
		//{
		//	if (this.GetComponentInParent<PlayerStats>().cells > 0)
		//	{
		//		//Vector2 difference = ClickPos.transform.position - transform.position;
		//		Vector3 difference = ClickPos.transform.position - transform.position;
		//		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		//		transform.rotation = Quaternion.Euler(0, 0, rotZ + Offset);
		//	}
		//}
		//if(this.transform.parent.tag == "P1/Right" && this.GetComponent<DetectClick>().Selected == true && GM.RotateGameObject.tag != "P1/Right")
		//{
		//	if (this.GetComponentInParent<PlayerStats>().cells > 0)
		//	{
		//		//Vector2 difference = ClickPos.transform.position - transform.position;
		//		Vector3 difference = ClickPos.transform.position - transform.position;
		//		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		//		transform.rotation = Quaternion.Euler(0, 0, rotZ + Offset);
		//	}
		//}

		//if (/*this.transform.parent.tag == "P2/Left" && */this.GetComponent<DetectClick>().Selected == true/* && GM.SelectedGO.tag != "P2/Left"*/)
		//{
		//	Debug.Log("why........................");
		//	if (this.GetComponentInParent<PlayerStats>().cells > 0)
		//	{

		//		//Vector2 difference = ClickPos.transform.position - transform.position;
		//		Vector3 difference = ClickPos.transform.position - transform.position;
		//		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		//		transform.rotation = Quaternion.Euler(0, 0, rotZ + Offset);
		//	}
		//}
		//if (/*this.transform.parent.tag == "P2/Right" && */this.GetComponent<DetectClick>().Selected == true/* && GM.SelectedGO.tag != "P2/Right"*/)
		//{
		//	Debug.Log("why........................");
		//	if (this.GetComponentInParent<PlayerStats>().cells > 0)
		//	{
		//		Debug.Log("why........................");
		//		//Vector2 difference = ClickPos.transform.position - transform.position;
		//		
		//	}
		//}
		//if (this.GetComponent<DetectClick>().Selected)
		//{
		//	float Zrot = Input.GetAxisRaw("Horizontal") * 10f;
		//	transform.Rotate(Vector3.forward, Zrot * Time.deltaTime);
		//}



		//if(transform.rotation.z * Mathf.Rad2Deg < maxZ && transform.rotation.z * Mathf.Rad2Deg > minZ)
		//{
		if(this.transform.parent.tag == "P1/Left" || this.transform.parent.tag == "P1/Right")
		{

			if (this.GetComponent<DetectClick>().Selected)
			{
				//rotZ += (Input.mousePosition.z - transform.position.z)  * 40 * Time.deltaTime;
				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotZ);
				if (Input.GetMouseButtonDown(0))
				{
		
					if (ClickCount == 1)
					{
						if(GM.RotateGameObject != null)
						{
							if (GM.RotateGameObject.tag != this.tag)
							{
								if (this.tag == "P1/Left" && GM.RotateGameObject.tag != "P1/Right" && TurnSystem.isPlayer1turn || this.tag == "P1/Right" && GM.RotateGameObject.tag != "P1/Left" && TurnSystem.isPlayer1turn)
								{
									CanShoot = true;
									Vector3 difference = GM.RotateGameObject.transform.position - transform.position;
									RotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
									transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, RotZ);
									GM.RotateGameObject = null;
								}
							}
						}		
					}
					else
					{
						ClickCount++;
						CanShoot = false;
					}
					
				}
				
			}
		}

		if (this.transform.parent.tag == "P2/Left" || this.transform.parent.tag == "P2/Right")
		{

			if (this.GetComponent<DetectClick>().Selected)
			{
				//rotZ += Input.GetAxisRaw("Horizontal") * 40 * Time.deltaTime;

				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotZ);
				
				if (Input.GetMouseButtonDown(0))
				{
					//Debug.Log(ClickPos.gameObject);
					if (ClickCount == 1)
					{
						if(GM.RotateGameObject != null)
						{
							if (GM.RotateGameObject.tag != this.tag)
							{
								if (this.tag == "P2/Left" && GM.RotateGameObject.tag != "P2/Right" && !TurnSystem.isPlayer1turn || this.tag == "P2/Right" && GM.RotateGameObject.tag != "P2/Left" && !TurnSystem.isPlayer1turn)
								{
									CanShoot = true;
									Vector3 difference = GM.RotateGameObject.transform.position - transform.position;
									RotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
									transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, RotZ);
									GM.RotateGameObject = null;
								}
							}
						}		
					}
					else
					{
						ClickCount++;
						transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180);
						canTurn = false;
					}

				}
			}
		}
		//}


	}
	
	void PositionTurret()
	{
		
		//if (GM.RotateGameObject.tag == "P1/Right" || GM.RotateGameObject.tag == "P1/Left")
		//{
		//	Debug.Log("okkkk");
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		return;
		//	}
		//}
		//else
		//{
			if (this.GetComponentInParent<PlayerStats>().cells > 0)
			{
		
				Vector2 difference = ClickPos.transform.position - transform.position;
				//Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
				float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0, 0, rotZ + Offset);
				//transform.LookAt(ClickPos);
				//Debug.Log("P1");
			}

		//}
	}

	void PositionTurret2()
	{
		if (GM.RotateGameObject.tag == "P2/Right" || GM.RotateGameObject.tag == "P2/Left")
		{
			return;
		}
		else
		{
			Debug.Log("okkkk");
			if (this.GetComponentInParent<PlayerStats>().cells > 0)
			{
				//Vector2 difference = ClickPos.transform.position - transform.position;
				Vector3 difference = G.transform.position - transform.position;
				float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0, 0, rotZ + Offset);
			}
		}
	}
}
