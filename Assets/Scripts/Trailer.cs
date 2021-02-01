using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
	public GameObject stuff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("s"))
		{
			stuff.GetComponent<SceneTransButtons>().Play();
		}
    }
}
