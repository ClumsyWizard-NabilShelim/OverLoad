using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLink : MonoBehaviour
{
    public void Youtube()
	{
		Application.OpenURL("https://www.youtube.com/channel/UCCTCBF1Viwl6dkTn0BQ1foQ?view_as=subscriber");
	}

	public void Twitter()
	{
		Application.OpenURL("https://twitter.com/Nabil_shelim");
	}
}
