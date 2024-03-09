using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Plates : TMonoSingleton<Plates>  
{
	private static int MAXWIDTH = 7;
	private static float PLATEGAP = 0.8f;
	private bool m_inited = false;
	private bool m_poopGoing = false;

	public bool PoopGoing
	{
		get
		{
			return m_poopGoing;
		}
	}

	public struct XYIndex
	{
		public int x;
		public int y;
	}

	private GameObject[,] m_plates = new GameObject[MAXWIDTH, MAXWIDTH];	

	private	GameObject m_tempPlate;

	public override void Initialize ()
	{
		gameObject.name = "Plates";

		Vector2 basePos = new Vector2(-2.4f, 2.4f);

		TextAsset stage = Resources.Load("StageText/stage01", typeof(TextAsset)) as TextAsset;
		StringReader stageValue = new StringReader(stage.text);

		int[,] stagePlateValue;
		stagePlateValue = new int[MAXWIDTH, MAXWIDTH];
		string[] stageValueStr;
		
		if(false == m_inited)
		{
			m_tempPlate = new GameObject();
		}
		m_poopGoing = false;

		for(int i = 0; i < MAXWIDTH; i++)
		{
			string readLine = stageValue.ReadLine();
			stageValueStr = readLine.Split(' ');
			for(int j = 0; j < MAXWIDTH; j++)
			{
				stagePlateValue[i, j] = int.Parse(stageValueStr[j]);
			}
		}

		for(int i = 0; i < m_plates.GetLength(0); i++)
		{
			//m_Plates[i] = new Plate[7]
			for(int j = 0; j < m_plates.GetLength(1); j++)
			{
				if(false == m_inited)
				{
					m_plates[i, j] = new GameObject();

					m_plates[i, j].name = "Plate";
					m_plates[i, j].AddComponent<PlateBase>();

					m_plates[i, j].AddComponent<BoxCollider2D>();

       		 		m_plates[i, j].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);
       				m_plates[i, j].GetComponent<BoxCollider2D>().isTrigger = true;
				}

				PlateBase plateScript = (PlateBase)m_plates[i, j].GetComponent(typeof(PlateBase));
				
				int plateTypeValue = stagePlateValue[j, i];

				plateScript.InitPlate(i, j, PLATEGAP, basePos, plateTypeValue, m_inited);
			}
		}
	}

	public void Reset()
	{
		m_inited = true;
		Initialize();
	}

	public void EndGame()
	{
		for(int i = 0; i < m_plates.GetLength(0); i++)
		{
			//m_Plates[i] = new Plate[7]
			for(int j = 0; j < m_plates.GetLength(1); j++)
			{
				PlateBase plateScript = (PlateBase)m_plates[i, j].GetComponent(typeof(PlateBase));
				plateScript.SetPlateType(PlateType.kEmpty);
			}
		}
	}

	public override void Destroy ()
	{
		
	}
}
