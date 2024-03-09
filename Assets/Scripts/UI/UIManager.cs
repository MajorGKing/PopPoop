using UnityEngine;
using System.Collections.Generic;

public class UIManager : TMonoSingleton<UIManager>
{
	private Dictionary<UIType, UIBase> m_uiList;

	private UIGame m_uiGame;
	private UIResult m_uiResult;
    public UIGame UIGame
    {
        get
        {
            return m_uiGame;
        }
    }

	public UIResult UIResult
	{
		get
		{
			return m_uiResult;
		}
	}

	public override void Initialize ()
	{
		GameObject uiRoot = GameObject.Find ("Canvas");

		m_uiList = new Dictionary<UIType, UIBase> ();
		m_uiList.Add (UIType.kMain, uiRoot.transform.Find ("UIMain").GetComponent<UIMain> ());
		m_uiList.Add (UIType.kGame, uiRoot.transform.Find ("UIGame").GetComponent<UIGame> ());
		m_uiList.Add (UIType.kResult, uiRoot.transform.Find ("UIResult").GetComponent<UIResult> ());
		

		m_uiGame = uiRoot.transform.Find("UIGame").GetComponent<UIGame>();
		m_uiResult = uiRoot.transform.Find("UIResult").GetComponent<UIResult>();
	}

	public override void Destroy()
	{
	}

	public void Open(UIType type)
	{
		m_uiList [type].Open ();
	}

	public void Close(UIType type)
	{
		m_uiList [type].Close ();
	}
}
