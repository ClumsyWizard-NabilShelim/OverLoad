using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundAttributes : MonoBehaviour
{
	GameManager GM;
	public bool nukeChange;
	public GameObject NuKedVersion_BG;

	[HideInInspector]
	public Image[] NukedVersion;

	[ColorUsage(false)]
	public Color Normal;
	[ColorUsage(false)]
	public Color Nuked;

	Camera Camera;
	SkyColor cam;

	public GameObject[] Envi_Element_Object;
	public Objectgenerator ObjectGenerator;
	public GameObject[] Envi_Element_Pos;

	public float DelaySet;
	// Start is called before the first frame update
	void Start()
    {
		Camera = Camera.main;
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		cam = Camera.main.GetComponentInParent<SkyColor>();
		//if (nukeChange)
		//{
			GM.BG = this.gameObject;
			
			NukedVersion = new Image[NuKedVersion_BG.transform.childCount];
			for (int i = 0; i < NuKedVersion_BG.transform.childCount; i++)
			{
				NukedVersion[i] = NuKedVersion_BG.transform.GetChild(i).GetComponent<Image>();

			}
			
		//}
		//if (!nukeChange)
		//{
		//	GM.BG = null;
		//}

	}

    // Update is called once per frame
    void Update()
    {
	//	transform.position = new Vector2(transform.position.x, 0);
		GM.BG = this.gameObject;
		cam.NukeSKY = Nuked;
		cam.NormalSKY = Normal;
		ObjectGenerator.Objects = Envi_Element_Object;
		ObjectGenerator.SpawnPoints = Envi_Element_Pos;
		ObjectGenerator.Delay = DelaySet;
	}
}
