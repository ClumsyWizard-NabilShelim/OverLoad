using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroy : MonoBehaviour
{
	public GameOver game;
    public void ShipOutOfScene()
	{
		game.CanExplode = false;
	}
}
