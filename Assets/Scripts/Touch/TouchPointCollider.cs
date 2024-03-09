using UnityEngine;
using System.Collections;

public class TouchPointCollider : MonoBehaviour 
{
	private int m_firstIndexX;
	private int m_firstIndexY;
	private int m_secondIndexX;
	private int m_secondIndexY;

	private bool hasValue;
	private bool inputStop;

	
	// Use this for initialization
	void Start () 
	{
		m_ValueReset();

		inputStop = false;
	}

	public void InputStopReset()
	{
		 m_InputStopReset();
	}

	private void m_InputStopReset()
	{
		inputStop = false;
	}

	private void m_ValueReset()
	{
		m_firstIndexX = -1;
		m_firstIndexY = -1;
		m_secondIndexX = -1;
		m_secondIndexY = -1;

		hasValue = false;

		inputStop = true;

		//Debug.Log("Reset Values");
	}
	private void m_ChangePlate()
	{
		//Debug.Log("Change");

		Plates.Instance.ChangePlate(m_firstIndexX, m_secondIndexX, m_firstIndexY, m_secondIndexY);

		m_ValueReset();

		//TouchPoint.Instance.PosReset();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Enter");

		//Debug.Log("POS X : " + gameObject.transform.position.x);
        //Debug.Log("POS Y : " + gameObject.transform.position.y);

		if("Plate" == other.name  && false == inputStop)
		{
			PlateBase plateBase = (PlateBase)other.GetComponent(typeof(PlateBase));
			//Debug.Log("X : " + indexX);
			//Debug.Log("Y : " + indexY);

			if(PlateType.kNotUse != plateBase.PlateType && PlateType.kEmpty != plateBase.PlateType)
			{
				if(false == hasValue)
				{
					hasValue = true;

					m_firstIndexX = plateBase.IndexX;
					m_firstIndexY = plateBase.IndexY;

					//Debug.Log("First");
					//Debug.Log("X : " + m_firstIndexX + " Y : " + m_firstIndexY);
				}

				else if(true == hasValue)
				{
					m_secondIndexX = plateBase.IndexX;
					m_secondIndexY = plateBase.IndexY;

					//Debug.Log("Second");
					//Debug.Log("X : " + m_secondIndexX + " Y : " + m_secondIndexY);

					if(m_secondIndexX == m_firstIndexX -1 && m_secondIndexY == m_firstIndexY)
					{
						m_ChangePlate();
					}

					else if(m_secondIndexX == m_firstIndexX +1 && m_secondIndexY == m_firstIndexY)
					{
						m_ChangePlate();
					}

					else if(m_secondIndexX == m_firstIndexX && m_secondIndexY == m_firstIndexY - 1)
					{
						m_ChangePlate();
					}

					else if(m_secondIndexX == m_firstIndexX && m_secondIndexY == m_firstIndexY + 1)
					{
						m_ChangePlate();
					}
					else
					{
						m_ValueReset();
					}
				}
			}

		}
    }
}
