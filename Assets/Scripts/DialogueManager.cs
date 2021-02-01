using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

	Queue<string> sentences;

	public TextMeshProUGUI SentenceText;

	public GameObject ContinueButton;

	public TutorialManager TM;
    // Start is called before the first frame update
    void Start()
    {
		ContinueButton.SetActive(false);
		sentences = new Queue<string>();
    }

	public void TriggerConversation(Dialogue dialogue)
	{
		sentences.Clear();

		foreach(string sentence in dialogue.sentence)
		{
			sentences.Enqueue(sentence);
		}

		NextSentence();
	}

	public void NextSentence()
	{
		if(sentences.Count == 0)
		{
			EndConversation();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeText(sentence));
		//SentenceText.text = sentence;
	}

	IEnumerator TypeText(string sentence)
	{
		
		if (SentenceText.text != sentence)
		{
			ContinueButton.SetActive(false);
		}

		SentenceText.text = "";
	
		foreach (char letter in sentence.ToCharArray())
		{

			SentenceText.text += letter;
			FindObjectOfType<AudioManager>().Play("Scifitype");
			yield return null;
		
			if (SentenceText.text == sentence)
			{
				ContinueButton.SetActive(true);
			}
		}
	}

	void EndConversation()
	{
		if (TutorialSequenceHandeler.CardClicked)
		{
			TutorialSequenceHandeler.CardClicked = false;
		}
		if(TutorialSequenceHandeler.TurretClick == true)
		{
			TutorialSequenceHandeler.TurretClick = false;
		}
		TM.RobotAnimator.SetTrigger("RobotOut");
		TM.RobotAnimator.SetBool("RobotIn", false);
	}
}
