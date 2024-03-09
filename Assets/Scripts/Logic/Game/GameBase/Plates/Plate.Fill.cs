using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Plates : TMonoSingleton<Plates>  
{
	private void m_FillPlate()
	{
		if(GameState.kPlay != GameManager.Instance.GetState())
		{
			return ;
		}

		PlateBase checkPlate;

		for(int i = MAXWIDTH - 1; i >= 0; i--)
		{
			for(int j = 0; j <= MAXWIDTH - 1; j++)
			{
				checkPlate = (PlateBase)m_plates[j, i].GetComponent(typeof(PlateBase));

				if(PlateType.kEmpty == checkPlate.PlateType)
				{
					if(0 < i)
					{
						PlateBase FromPlate = (PlateBase)m_plates[j, i].GetComponent(typeof(PlateBase));

						if(PlateType.kNotUse != FromPlate.PlateType)
						{
							m_ChangePlate(j, j, i, i - 1);
							//Debug.Log("Change!!");
						}
					}
					else if(0 == i)
					{
						int plateTypeValue = Random.Range(0, 5) + 2;
						//Debug.Log(plateTypeValue);
						checkPlate.SetPlateType((PlateType)plateTypeValue);
						//Debug.Log("create : " + i + " " + j);
					}
				}
			}
		}

		bool fillFinsihed = true;

		for(int i = 0; i < MAXWIDTH; i++)
		{
			for(int j = 0; j < MAXWIDTH; j++)
			{
				checkPlate = (PlateBase)m_plates[i, j].GetComponent(typeof(PlateBase));

				if(PlateType.kEmpty == checkPlate.PlateType)
				{
					fillFinsihed = false;
					//Debug.Log("Empty : " + i + " " + j);
					break;
				}
			}
		}

		if(false == fillFinsihed)
		{
			m_FillPlate();
		}
	}

	IEnumerator FillTimeWait(float waitTime, bool pooped) 
	{
		do
		{
			yield return new WaitForSeconds(waitTime * 3);
			m_FillPlate();
				
			pooped = m_PoopCheck();
			
			m_poopGoing = true;
		} while (true == pooped);

		bool canPoop;
		canPoop = m_CanPoopCheck();

		if(false == canPoop)
		{
			//Debug.Log("Reset!!!");
			Reset();
		}

		m_poopGoing = false;
    }
}
