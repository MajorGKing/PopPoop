using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Plates : TMonoSingleton<Plates>  
{
	private bool m_PoopCheck()
	{
		bool pooped = false;
		bool returnPooped = false;

		for(int i = 0; i < MAXWIDTH; i++)
		{
			for(int j = 0; j < MAXWIDTH; j++)
			{
				pooped = m_PoopCheck(i, j);

				if(true == pooped)
				{
					returnPooped = true;
				}
			}
		}

		return returnPooped;
	}

	private bool m_PoopCheck(int x1, int y1)
	{
		int checkX, checkY;
		int counterRow, counterColumn;
		int totalCounter = 0;

		List<XYIndex> xyIndexRow = new List<XYIndex>();
		List<XYIndex> xyIndexColumn = new List<XYIndex>();

		PlateBase basePlate = (PlateBase)m_plates[x1, y1].GetComponent(typeof(PlateBase));

		if(PlateType.kNotUse == basePlate.PlateType)
		{
			return false;
		}

		checkX = x1;
		checkY = y1;

		PlateBase checkPlate;

		counterRow = 1;

		XYIndex addValue = new XYIndex();

		addValue.x = x1;
		addValue.y = y1;

		xyIndexRow.Add(addValue);

		// row Check
		while(checkX > 0)
		{
			checkX--;

			checkPlate = (PlateBase)m_plates[checkX, checkY].GetComponent(typeof(PlateBase));

			if(basePlate.PlateType == checkPlate.PlateType)
			{
				counterRow++;


				XYIndex newAddValue = new XYIndex();

				newAddValue.x = checkX;
				newAddValue.y = checkY;

				xyIndexRow.Add(newAddValue);
			}

			if(basePlate.PlateType != checkPlate.PlateType)
			{
				break;
			}

		}

		checkX = x1;

		while(checkX < MAXWIDTH - 1)
		{
			checkX++;

			checkPlate = (PlateBase)m_plates[checkX, checkY].GetComponent(typeof(PlateBase));

			if(basePlate.PlateType == checkPlate.PlateType)
			{
				counterRow++;

				XYIndex newAddValue = new XYIndex();

				newAddValue.x = checkX;
				newAddValue.y = checkY;

				xyIndexRow.Add(newAddValue);
			}

			if(basePlate.PlateType != checkPlate.PlateType)
			{
				break;
			}

		}

		if(3 > counterRow)
		{
			xyIndexRow.Clear();

			counterRow = 0;
		}

		//Debug.Log("Row Counter : " + counterRow);

		addValue.x = x1;
		addValue.y = y1;

		xyIndexColumn.Add(addValue);

		counterColumn = 1;
		checkX = x1;
		checkY = y1;
		// column Check
		while(checkY > 0)
		{
			checkY--;

			checkPlate = (PlateBase)m_plates[checkX, checkY].GetComponent(typeof(PlateBase));

			if(basePlate.PlateType == checkPlate.PlateType)
			{
				counterColumn++;

				XYIndex newAddValue = new XYIndex();

				newAddValue.x = checkX;
				newAddValue.y = checkY;

				xyIndexColumn.Add(newAddValue);
			}

			if(basePlate.PlateType != checkPlate.PlateType)
			{
				break;
			}

		}

		checkY = y1;

		while(checkY < MAXWIDTH - 1)
		{
			checkY++;

			checkPlate = (PlateBase)m_plates[checkX, checkY].GetComponent(typeof(PlateBase));

			if(basePlate.PlateType == checkPlate.PlateType)
			{
				counterColumn++;

				XYIndex newAddValue = new XYIndex();

				newAddValue.x = checkX;
				newAddValue.y = checkY;

				xyIndexColumn.Add(newAddValue);
			}

			if(basePlate.PlateType != checkPlate.PlateType)
			{
				break;
			}

		}

		if(3 > counterColumn)
		{
			xyIndexColumn.Clear();
			counterColumn = 0;
		}

		//Debug.Log("Column Counter : " + counterColumn);


		foreach (XYIndex xy in xyIndexRow)
		{
			StartCoroutine(PoopWait(xy.x, xy.y, 0.1f));
		}
		
		foreach (XYIndex xy in xyIndexColumn)
		{
			StartCoroutine(PoopWait(xy.x, xy.y, 0.1f));
		}
		
		xyIndexRow.Clear();
		xyIndexColumn.Clear();

		totalCounter = counterRow + counterColumn;

		if(3 <= totalCounter)
		{
			//
			if(GameState.kPlay == GameManager.Instance.GetState())
			{
				//Debug.Log("Score Added : " + totalCounter);
				GameManager.Instance.GameScore = totalCounter;
			}

			return true;
		}

		return false;
	}

	private bool m_PoopCheck(int x1, int x2, int y1, int y2)
	{
		PlateBase plate1 = (PlateBase)m_plates[x1, y1].GetComponent(typeof(PlateBase));
		PlateBase plate2 = (PlateBase)m_plates[x2, y2].GetComponent(typeof(PlateBase));
		//Debug.Log(plate1.PlateType);
		//Debug.Log(plate2.PlateType);

		if(plate1.PlateType == plate2.PlateType)
		{
			Debug.Log("Same type");
			return false;
		}
		else if(plate1.PlateType != plate2.PlateType)
		{
			bool checkPoop1 = m_PoopCheck(x1, y1);
			bool checkPoop2 = m_PoopCheck(x2, y2);

			if(true == checkPoop1 || true == checkPoop2)
			{
				return true;
			}
		}

		return false;
	}
	
	private bool m_CanPoopCheck()
	{
		for(int i = 0; i < MAXWIDTH; i++)
		{
			for(int j = 0; j < MAXWIDTH; j++)
			{
				bool canPoop = false;

				canPoop = m_CanPoopCheck(i, j);

				if(true == canPoop)
				{
					return true;
				}
			}
		}

		return false;
	}

	private bool m_CanPoopCheck(int x, int y)
	{
		PlateBase orgPlate, plate1, plate2;

		orgPlate = (PlateBase)m_plates[x, y].GetComponent(typeof(PlateBase));

		if(PlateType.kNotUse == orgPlate.PlateType)
		{
			return false;
		}

		if(MAXWIDTH - 2 > x)
		{
			if(MAXWIDTH -1 > y)
			{
				plate1 = (PlateBase)m_plates[x + 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y + 1].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x + 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x + 1, y].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y + 1].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}
			}

			if(0  < y)
			{
				orgPlate = (PlateBase)m_plates[x, y].GetComponent(typeof(PlateBase));
				plate1 = (PlateBase)m_plates[x + 1, y - 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y - 1].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x + 1, y - 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x + 1, y].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 2, y - 1].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}
			}			
		}

		if(MAXWIDTH - 2 > y)
		{
			if(MAXWIDTH -1 > x)
			{
				orgPlate = (PlateBase)m_plates[x, y].GetComponent(typeof(PlateBase));
				plate1 = (PlateBase)m_plates[x + 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 1, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x + 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x + 1, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

			}

			if(0  < x)
			{
				orgPlate = (PlateBase)m_plates[x, y].GetComponent(typeof(PlateBase));
				plate1 = (PlateBase)m_plates[x - 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x - 1, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x - 1, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}

				plate1 = (PlateBase)m_plates[x, y + 1].GetComponent(typeof(PlateBase));
				plate2 = (PlateBase)m_plates[x - 1, y + 2].GetComponent(typeof(PlateBase));

				if(orgPlate.PlateType == plate1.PlateType && orgPlate.PlateType == plate2.PlateType)
				{
					return true;
				}
			}
		}

		return false;
	}

	IEnumerator PoopWait(int x, int y, float waitTime) 
	{
		m_poopGoing = true;
		//print("Tap : " + Time.time);
		yield return new WaitForSeconds(waitTime);
		m_plates[x, y].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0f);
		yield return new WaitForSeconds(waitTime);
		m_plates[x, y].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1f);
		yield return new WaitForSeconds(waitTime);
		m_plates[x, y].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0f);
		yield return new WaitForSeconds(waitTime);
		m_plates[x, y].GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1f);
		yield return new WaitForSeconds(waitTime);
		
		PlateBase changePlate = (PlateBase)m_plates[x, y].GetComponent(typeof(PlateBase));
		changePlate.SetPlateType(PlateType.kEmpty);
    }
}