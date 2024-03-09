using UnityEngine;
using System.Collections;

public class PlateBase : MonoBehaviour
{
	private int m_indexX, m_indexY;
	private PlateType m_plateType;
	private float m_posGap;

	private Vector2 m_basePos;


	public int IndexX
	{
    	get
    	{
        	return m_indexX;
    	}
	}

	public int IndexY
	{
    	get
    	{
        	return m_indexY;
    	}
	}

	public PlateType PlateType
	{
		get
		{
			return m_plateType;
		}
	}



	private void m_SetBasePos(Vector2 basePos)
	{
		m_basePos = basePos;
	}

	private void m_SetPosGap(float posGap)
	{
		m_posGap = posGap;
	}

	private void m_SetPos()
	{
		Vector3 newPos = new Vector3(0, 0, 0);

		newPos.x = m_basePos.x + m_indexX * m_posGap;
		newPos.y = m_basePos.y - m_indexY * m_posGap;

		this.gameObject.transform.position = newPos;
	}

	private void m_SetIndex(int indexX, int indexY)
	{
		m_indexX = indexX;
		m_indexY = indexY;

		m_SetPos();
	}

	public void SetIndex(int indexX, int indexY)
	{
		m_SetIndex(indexX, indexY);

		m_SetPos();
	}

	public void SetIndex(int indexX, int indexY, float posGap, Vector2 basePos)
	{
		m_SetBasePos(basePos);
		m_SetPosGap(posGap);
		SetIndex(indexX, indexY);
	}

	public void SetPos()
	{
		m_SetPos();
	}

	public void SetPlateType(PlateType plateType)
	{
		m_SetPlateType((int)plateType);
	}

	private void m_SetPlateType(int plateType)
	{
		m_plateType = (PlateType)plateType;

		this.gameObject.GetComponent<SpriteRenderer>().sprite = PlateSprite.Instance.plateSprite[plateType];
	}

	public void InitPlate(int indexX, int indexY, float posGap, Vector2 basePos, int plateType, bool reset)
	{
		if(false == reset)
		{
			this.gameObject.AddComponent<SpriteRenderer>();
		}
		m_SetBasePos(basePos);
		m_SetPosGap(posGap);
		SetIndex(indexX, indexY);
		m_SetPlateType(plateType);
	}
}