using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimScript : MonoBehaviour
{

    GameObject HEAD;
    float HeadPos;

	[Header("Recoil")]
	public float RecoilAmount;
    bool go;
	[HideInInspector]
	public bool recoil;

	bool Ro;
	[HideInInspector]
	public bool RotateActive;
	[HideInInspector]
	public bool canRotate;
    // Start is called before the first frame update
    void Start()
    {
		HEAD = transform.GetChild(0).gameObject;
		HeadPos = HEAD.transform.localPosition.x;
	}

    // Update is called once per frame
    void Update()
    {
		#region RECOIL
		if (recoil)
		{
			StartCoroutine(Recoil());
		}
		if (go)
		{
			RecoilOpen();
		}
		if (!go)
		{
			RecoilClose();
		}
		#endregion

		#region ROTATE
		if (canRotate)
		{
			if (RotateActive)
			{
				Ro = true;
			}
			if (!RotateActive)
			{
				Ro = false;
			}
			if (Ro)
			{
				RotateOpen();
			}
			if (!Ro)
			{
				RotateClose();
			}
		}

		#endregion
	}

	#region Recoil_Methods
	IEnumerator Recoil()
	{
		Debug.Log("Worls");
		go = true;
		yield return new WaitForSeconds(0.1f);
		go = false;
		recoil = false;
	}

	void RecoilOpen()
	{
		HEAD.transform.localPosition = new Vector2(Mathf.Lerp(HEAD.transform.localPosition.x, RecoilAmount, 0.05f), HEAD.transform.localPosition.y);
	}

	void RecoilClose()
	{
		HEAD.transform.localPosition = new Vector2(Mathf.Lerp(HEAD.transform.localPosition.x, HeadPos, 0.05f), HEAD.transform.localPosition.y);
	}

	#endregion

	#region Roatte_Method
	void RotateOpen()
	{
		if (this.transform.parent.tag == "P1/Left" || this.transform.parent.tag == "P1/Right")
		{
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, -15), 0.05f);
		}

		if (this.transform.parent.tag == "P2/Left" || this.transform.parent.tag == "P2/Right")
		{
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 195), 0.05f);
		}

		transform.parent.GetComponent<AudioSource>().enabled = true;
	}

	void RotateClose()
	{
		if (this.transform.parent.tag == "P1/Left" || this.transform.parent.tag == "P1/Right")
		{
					
		}

		if (this.transform.parent.tag == "P2/Left" || this.transform.parent.tag == "P2/Right")
		{
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 180), 0.05f);
		}
		transform.parent.GetComponent<AudioSource>().enabled = false;
		RotateActive = false;
	}
	#endregion
}
