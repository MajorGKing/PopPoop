using UnityEngine;
using System.Collections;

public class ApplicationMain : MonoBehaviour
{
	void Awake ()
	{
		UIManager.Create ();
		GameManager.Create ();
		TouchPoint.Create();
		PlateSprite.Create();
	}
}
