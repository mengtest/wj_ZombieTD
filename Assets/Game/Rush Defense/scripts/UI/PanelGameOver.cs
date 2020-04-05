using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelGameOver : MonoBehaviour {
	
	// Use this for initialization
	public Text txt_title,txt_totalbuild,txt_timeused,txt_totalkilled,txt_unitscore,txt_totalscore,txt_bonus,txt_bonusNum;
	public Text txt_totalbuildNum,txt_timeusedNum,txt_totalkilledNum,txt_unitscoreNum,txt_totalscoreNum;
	public Text txt_btnLeft,txt_btnRight;
	public GameObject panelUpgrade;
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnClick(GameObject g){
                //播放穿山甲广告
        GameObject.Find("ADmanager").GetComponent<Example>().MyLoadAD();
        
		switch (g.name) {
		case "btnleft":
			panelUpgrade.SetActive(true);
			panelUpgrade.SendMessage("refreshView");
			GameManager.getInstance().hideBanner(true);
			break;
		case "btnright":
			GameObject fadepanel = GameObject.Find ("PanelFade");
			fadepanel.SetActive(true);
			if(GameData.getInstance().cLevel < GameData.getInstance().totalLevel - 1){
				if(GameData.getInstance().isWin){
					GameData.getInstance().cLevel++;
				}

				fadepanel.SendMessage("returnTitle",3);
			}else{
				if(GameData.getInstance().isWin){
					GameData.getInstance ().cLevel = 0;
					fadepanel.SendMessage("returnTitle",-1);
				}else{
					fadepanel.SendMessage("returnTitle",3);
				}
			}


			break;
		}
		Destroy(gameObject);
	}
	
	void init(){
		

	
			if(GameData.getInstance().isWin){
//				txt_title.text = "Mission Accomplished";
				txt_title.text = Localization.Instance.GetString ("missionAcc");
			}else{
//				txt_title.text = "Mission Failed";
				txt_title.text = Localization.Instance.GetString ("missionFail");
			}

//			txt_totalbuild.text = "Total Build:";
//			txt_timeused.text = "Time Used:";
//			txt_totalkilled.text = "Total Killed:";
//			txt_unitscore.text = "Unit Score";
//			txt_totalscore.text = "Total Score";
//			txt_bonus.text = "Bonus:";
			txt_totalbuild.text = Localization.Instance.GetString ("totalBuild");
			txt_timeused.text = Localization.Instance.GetString("timeUsed");
			txt_totalkilled.text = Localization.Instance.GetString ("totalKilled");
			txt_unitscore.text = Localization.Instance.GetString ("unitScore");
			txt_totalscore.text = Localization.Instance.GetString ("totalScore");
			txt_bonus.text = Localization.Instance.GetString ("bonus");
//			
//			txt_btnLeft.text = "UPGRADE";
//			txt_btnRight.text = "TITLE";
			if(GameData.getInstance().cLevel < GameData.getInstance().totalLevel-1){
				txt_btnRight.text = Localization.Instance.GetString ("btnContinue");
			}else{
				
			txt_btnRight.text = Localization.Instance.GetString ("btnMainMenu");
			}
			if(GameData.getInstance().isWin == false){
				txt_btnRight.text = Localization.Instance.GetString ("btnRetry");
			}

			

	

		GameData.getInstance ().timeScale = Time.timeScale;
		Time.timeScale = 1;
		
	}
	
	void refreshView(){
//		init ();
		txt_timeusedNum.text = GameData.getInstance ().timeelaspe.ToString()+"s";
		txt_totalbuildNum.text = GameData.getInstance ().heroBuild.ToString();
		txt_totalkilledNum.text = GameData.getInstance ().enemyKilled.ToString();
		txt_unitscoreNum.text = GameData.getInstance ().unitScore.ToString();
		int t_TotalScore = (GameData.getInstance ().unitScore + GameData.getInstance ().otherScore - GameData.getInstance ().timeelaspe);
		txt_totalscoreNum.text = t_TotalScore.ToString();
		GameData.getInstance ().bestScore = t_TotalScore;
		GameManager.getInstance ().submitGameCenter ();

		if (GameData.getInstance ().isWin) {
			if (GameData.getInstance ().cLevel >= GameData.getInstance ().levelPassed) {
				txt_bonusNum.text = "x" + 3;
			}else{
				txt_bonusNum.text = "x" + 1;
			}
			
		} else {
			txt_bonusNum.text = "x" + 0;
		}
	}
	
	
}
