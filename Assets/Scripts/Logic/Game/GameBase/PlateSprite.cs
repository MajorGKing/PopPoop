using UnityEngine;
using System.Collections;

public class PlateSprite : TMonoSingleton<PlateSprite>  
{
	private int m_plateCount = 7;	// from PlateType.cs

	public Sprite[] plateSprite;
	

	public override void Initialize()
    {
		gameObject.name = "PlateSprite";

		plateSprite = new Sprite[m_plateCount];

		plateSprite[0] = (Sprite)Resources.Load<Sprite>("Plate/grayBox");
		plateSprite[1] = (Sprite)Resources.Load<Sprite>("Plate/whiteBox");
		plateSprite[2] = (Sprite)Resources.Load<Sprite>("Plate/yellowBox");
		plateSprite[3] = (Sprite)Resources.Load<Sprite>("Plate/redBox");
		plateSprite[4] = (Sprite)Resources.Load<Sprite>("Plate/greenBox");
		plateSprite[5] = (Sprite)Resources.Load<Sprite>("Plate/blueBox");
		plateSprite[6] = (Sprite)Resources.Load<Sprite>("Plate/orangeBox");
	}

	public override void Destroy()
	{

	}

}
