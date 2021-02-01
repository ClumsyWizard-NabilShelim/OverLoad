using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public void win()
	{
		FindObjectOfType<AudioManager>().Play("ShipThrusters");
	}
}
