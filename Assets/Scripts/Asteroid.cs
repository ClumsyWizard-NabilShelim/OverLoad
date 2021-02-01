using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{


	public Animator animator;
    float start;
	public float startValue;
	// Start is called before the first frame update
	void Start()
    {
		start = startValue;
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(start <= 0)
		{
			int a = Random.Range(0, 2);
			if (a == 0)
			{
				animator.SetTrigger("A1");
				start = startValue;
			}
			if (a == 1)
			{
				animator.SetTrigger("A2");
				start = startValue;
			}
		}
		else
		{
			start -= Time.deltaTime;
		}
	
	}
}
