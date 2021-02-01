using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
	GameManager GM;
	public static GameObject cursorGO;
	public Sprite DefaultCursor;
	public Sprite SelectedCursor;
	SpriteRenderer rend;
	//public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
		cursorGO = transform.gameObject;
		GM = GetComponentInParent<GameManager>();
		rend = GetComponent<SpriteRenderer>();
	}

	//Update is called once per frame

	void Update()
	{
		Cursor.visible = false;
		Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = cursorPos;
		if(GM.SelectedGO != null)
		{
			rend.sprite = SelectedCursor;
			transform.rotation = Quaternion.Euler(0, 0, 0);
			//particleEffect.SetActive(false);
		}
		if (GM.SelectedGO == null)
		{
			rend.sprite = DefaultCursor;
			transform.rotation = Quaternion.Euler(0, 0, 35);
			//particleEffect.SetActive(true);
		}
	}
}
