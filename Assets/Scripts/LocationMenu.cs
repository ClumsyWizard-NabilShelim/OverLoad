using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationMenu : MonoBehaviour
{
	public GameObject[] Locations;
	public Image[] LocationBanner;
	public int LocationIndex;
	Animator LocationAnimator;

	public Sprite PlanetStar_MainMenu;
	public Sprite Star_MainMenu;

	public Image MainMenuIMG;

	GameObject LO;
    // Start is called before the first frame update
    void Start()
    {
		MainMenuIMG.sprite = GameObject.FindGameObjectWithTag("MainMenu").GetComponentInChildren<Image>().sprite;
		MainMenuIMG.sprite = PlanetStar_MainMenu;
		LocationAnimator = this.GetComponent<Animator>();
		LocationBannerUpdate();
		LocationUpdate();
	}

    // Update is called once per frame
    void Update()
	{
		if (LO != null && LO.tag == "Star")
		{
			MainMenuIMG.sprite = Star_MainMenu;
		}
		else
		{
			MainMenuIMG.sprite = PlanetStar_MainMenu;
		}
        if(LocationIndex > LocationBanner.Length - 1)
		{
			LocationIndex = 0;
			LocationBannerUpdate();
			LocationUpdate();
		}
		if (LocationIndex < 0)
		{
			LocationIndex =  LocationBanner.Length - 1;
			LocationBannerUpdate();
			LocationUpdate();
		}
	}

	public void IncreaseIndex()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		LocationIndex++;
		LocationBannerUpdate();
		LocationUpdate();
	}


	public void DecreaseIndex()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		LocationIndex--;
		LocationBannerUpdate();
		LocationUpdate();
	}

	public void OpenLocationMenu()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		LocationAnimator.SetBool("In", true);
	}

	public void CloseLocationMenu()
	{
		FindObjectOfType<AudioManager>().Play("SelectionMenuClick");
		LocationAnimator.SetBool("In", false);
	}

	void LocationUpdate()
	{
		int i = 0;
		foreach (GameObject LocationGO in Locations)
		{
			if(i == LocationIndex)
			{
				Locations[i].SetActive(true);
				LO = Locations[i];
			}
			else
			{
				Locations[i].SetActive(false);
			}
			i++;
		}

	}

	void LocationBannerUpdate()
	{
		int i = 0;
		foreach (Image LocationIMG in LocationBanner)
		{
			if (i == LocationIndex)
			{
				LocationBanner[i].enabled = true;
			}
			else
			{
				LocationBanner[i].enabled = false;
			}
			i++;
		}
	}
}
