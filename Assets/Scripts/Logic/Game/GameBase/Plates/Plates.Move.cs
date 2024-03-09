using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class Plates : TMonoSingleton<Plates>  
{
	public void ChangePlate(int x1, int x2, int y1, int y2)
	{
		if(true == m_poopGoing)
		{
			return ;
		}
			
		m_ChangePlate(x1, x2, y1, y2);

		bool pooped = m_PoopCheck(x1, x2, y1, y2);

		//int i = 0;

		if(false == pooped)
		{
			StartCoroutine(ChagePlateReversTimeWait(x1, x2, y1, y2, 0.25f));
		}
		else if(true == pooped)
		{
			StartCoroutine(FillTimeWait(0.25f, pooped));
		}		
	}

	private void m_ChangePlate(int x1, int x2, int y1, int y2)
	{
		PlateBase plateScript1 = (PlateBase)m_plates[x1, y1].GetComponent(typeof(PlateBase));
		PlateBase plateScript2 = (PlateBase)m_plates[x2, y2].GetComponent(typeof(PlateBase));

		if(PlateType.kNotUse != plateScript1.PlateType && PlateType.kNotUse != plateScript2.PlateType)
		{

			m_tempPlate = m_plates[x1, y1];

			m_plates[x1, y1] = m_plates[x2, y2];
			m_plates[x2, y2] = m_tempPlate;

			plateScript1 = (PlateBase)m_plates[x1, y1].GetComponent(typeof(PlateBase));
			plateScript2 = (PlateBase)m_plates[x2, y2].GetComponent(typeof(PlateBase));

			plateScript1.SetIndex(x1, y1);
			plateScript2.SetIndex(x2, y2);
		}
	}

	IEnumerator ChagePlateReversTimeWait(int x1, int x2, int y1, int y2, float waitTime) 
	{
		//print(Time.time);
		yield return new WaitForSeconds(waitTime);
		m_ChangePlate(x1, x2, y1, y2);
		//print(Time.time);
    }
}