using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public static bool StartTutorial = false;

	public Animator TutorialAnimator;
	public Animator RobotAnimator;

	public GameObject[] Thrusters;

	public AudioSource ThrusterSound;

	public GameObject PopUpHandler;
    // Start is called before the first frame update
    void Start()
    {
		PopUpHandler.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (StartTutorial)
		{
			StartCoroutine(TutorialStart());
		}

    }

	IEnumerator TutorialStart()
	{
		yield return new WaitForSeconds(2f);
		RobotAnimator.SetBool("RobotIn", true);
		ThrusterSound.enabled = true;
		Thrusters[0].SetActive(true);
		Thrusters[1].SetActive(true);
		yield return new WaitForSeconds(3.5f);
		TutorialAnimator.SetTrigger("DialogueOpen");
		yield return new WaitForSeconds(2f);
		PopUpHandler.SetActive(true);
		StartTutorial = false;
		//RobotAnimator.SetBool("RobotIn", false);
	}
}
