using UnityEngine;
using System.Collections;
using System;

public class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	private TStateController<GameState> m_stateController;

	private bool m_isDisposed;

	private int m_gameScore;
	public int GameScore
	{
		get
		{
			return m_gameScore;
		}
		set
		{
			m_gameScore += value;
			UIManager.Instance.UIGame.SetScorePoint(m_gameScore);
		}
	}

	public void ResetGameScore()
	{
		m_gameScore = 0;
		UIManager.Instance.UIGame.SetScorePoint(m_gameScore);
	}

	public void Dispose()
	{
		if (true == m_isDisposed)
		{
			return;
		}

		// Do Dispose

		m_isDisposed = true;
	}

	public override void Initialize ()
	{
		m_isDisposed = false;
		m_stateController = new TStateController<GameState> ();
		m_stateController.AddState (GameState.kIdle, new GameIdleProcessor ());
		m_stateController.AddState (GameState.kPlay, new GamePlayProcessor ());
		m_stateController.AddState (GameState.kResult, new GameResultProcessor ());

		ChangeState (GameState.kIdle);
	}

	public override void Destroy ()
	{
		Dispose ();
	}

	public GameState GetState()
	{
		return m_stateController.CurrentState;
	}

	public IStateProcessor ChangeState(GameState state)
	{
		return m_stateController.ChangeState(state);
	}

	public TStateController<GameState> GetStateController()
	{
		return m_stateController;
	}

	void Update()
    {
        float updateTime = Time.deltaTime;

        m_stateController.Update(updateTime);
    }
}
