using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectgenerator : MonoBehaviour
{

	public GameObject[] SpawnPoints;

	public GameObject[] Objects;

	public float Delay;
	float CurrentTime;

	public GameManager Gm;

    // Start is called before the first frame update
    void Start()
    {
		Gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		CurrentTime = Delay;
		//for (int i = 0; i < transform.childCount; i++)
		//{
		//	SpawnPoints[i] = transform.GetChild(i).gameObject;
		//}
    }

    // Update is called once per frame
    void Update()
    {

		//for (int i = 0; i < transform.childCount; i++)
		//{
		//	SpawnPoints[i] = transform.GetChild(i).gameObject;
		//}
		if (ScenetransManager.StartGame)
		{
			if (SpawnPoints != null)
			{
				if (CurrentTime <= 0)
				{
					int I = Random.Range(0, transform.childCount);
					int O = Random.Range(0, Objects.Length);
					Instantiate(Objects[O], SpawnPoints[I].transform.position, SpawnPoints[I].transform.rotation);
					CurrentTime = Delay;
				}
				else
				{
					CurrentTime -= Time.deltaTime;
				}
			}
		}
    }
}
