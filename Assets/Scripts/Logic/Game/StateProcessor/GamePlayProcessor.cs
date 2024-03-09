using UnityEngine;
using System.Collections;

public class GamePlayProcessor : IStateProcessor
{
	float m_givenTime = 61f;
	float m_countdownTime;
	float m_remindTime;

	bool m_countdownFinished;
	bool m_gameSetFinished;

	public void Begin()
	{
		UIManager.Instance.Open (UIType.kGame);

		GameManager.Instance.ResetGameScore();

 		m_countdownTime = 4f;
		m_remindTime = m_givenTime;

		m_countdownFinished = false;
		m_gameSetFinished = false;
	}
	
	public void Update(float dt)
	{
		if(0 <= m_countdownTime)
		{
			UIManager.Instance.UIGame.SetCountdownRemind((int)m_countdownTime);
			m_countdownTime -= dt;

			return ;
		}
		else if(0 >= m_countdownTime && false == m_countdownFinished)
		{
			m_countdownFinished = true;
		}

		if(true == m_countdownFinished && false == m_gameSetFinished )
		{
			if(!GameObject.Find("Plates"))
			{
				Plates.Create();
			}
			else
			{
				Plates.Instance.Reset();
			}
			
			m_gameSetFinished = true;
		}

		if(true == m_gameSetFinished)
		{
			m_remindTime -= dt;
		}
		

		//Debug.Log(m_remindTime);

		UIManager.Instance.UIGame.SetTimeRemind((int)m_remindTime);

		if(0 >= m_remindTime && false == Plates.Instance.PoopGoing)
		{
			GameManager.Instance.ChangeState(GameState.kResult);
		}
	}
	
	public void End()
	{
		UIManager.Instance.Close (UIType.kGame);
	}
}
