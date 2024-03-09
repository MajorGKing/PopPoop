using UnityEngine;
using UnityEngine.UI;

public class UIResult : UIBase 
{
	private Text m_lbResultScore;
	
	public override void Initialize ()
	{
		m_lbResultScore = transform.Find ("LbResultScore").GetComponent<Text> ();

		m_lbResultScore.text = GameManager.Instance.GameScore.ToString ();
	}

	public void OnClickBtStart()
	{
		GameManager.Instance.ChangeState (GameState.kIdle);
	}
}
