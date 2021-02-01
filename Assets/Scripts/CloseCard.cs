using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCard : MonoBehaviour
{
	public bool clicked;

	public PolygonCollider2D polygonCollider2D;

	void Start()
	{
		polygonCollider2D = GetComponent<PolygonCollider2D>();
	}
    void OnMouseDown()
	{
		clicked = true;
	}
}
