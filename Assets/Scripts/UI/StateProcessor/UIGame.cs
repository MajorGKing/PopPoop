using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIBase
{
	private Text m_lbScore;
	private Text m_lbTimeRemind;
	private Text m_lbScorePoint;
	private Text m_lbCountdown;


	public override void Initialize ()
	{
		m_lbScore = transform.Find ("LbScore").GetComponent<Text> ();
		m_lbTimeRemind = transform.Find ("LbTimeRemind").GetComponent<Text> ();
		m_lbScorePoint = transform.Find ("LbScorePoint").GetComponent<Text> ();
		m_lbCountdown  = transform.Find ("LbCountdown").GetComponent<Text> ();
		m_isInitialized = true;

		//Debug.Log("init");
	}

	public void SetScore(int score)
	{
		m_lbScore.text = score.ToString ();
	}

	public void SetCountdownRemind(int time)
	{
		if(0 <= time)
		{
			m_lbCountdown.text = time.ToString();
		}
		
		if(0 >= time)
		{
			m_lbCountdown.text = "";
		}
	}

	public void SetTimeRemind(int time)
	{
		if(0 <= time)
		{
			m_lbTimeRemind.text = time.ToString();
		}
	}

	public void SetScorePoint(int score)
	{
		m_lbScorePoint.text = score.ToString();
	}
}
