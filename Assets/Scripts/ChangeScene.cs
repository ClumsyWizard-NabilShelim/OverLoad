using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{ 

	public string TutorialScene;
	public string PlayScene;

	public bool NextIsTutorial;

	public bool ClearAllData;
	void Start()
	{

	}
	void Update()
	{
		if (ClearAllData)
		{
			PlayerPrefs.DeleteAll();
		}
		if(PlayerPrefs.GetInt("Isfalse") == 1)
		{
			NextIsTutorial = false;
		}
		if (NextIsTutorial)
		{
			PlayerPrefs.SetString("Scene", TutorialScene);
		}
		if(NextIsTutorial == false)
		{
			PlayerPrefs.SetString("Scene", PlayScene);
		}
	}

	public void ChangeTrigger()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("Scene"));
	}
}
