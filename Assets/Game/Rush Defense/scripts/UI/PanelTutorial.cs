using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelTutorial : MonoBehaviour {
	
	public GameObject tutor0,tutor1,tutor2,tutor3;
	// Use this for initialization
	int cPage = 0;
	int nPage = 4;
	Button btnNext,btnPrev,btnPlay;
	GameObject[] tutorpanels;
	public Text txt0,txt1,txt2,txt3;
	void Start () {
		btnPlay = transform.Find ("btnPlay").GetComponent<Button> ();
		btnNext = transform.Find ("btnNext").GetComponent<Button> ();
		btnPrev = transform.Find ("btnPrev").GetComponent<Button> ();

		btnPrev.interactable = false;
		tutorpanels = new GameObject[]{tutor0,tutor1,tutor2,tutor3};
//		if (GameManager.getInstance ().isEn) {
//			txt0.text = "Hire a soilder from this bar just by touching the icon.Remember this will cost golds. You can do this only when the icon is light. You earn golds by defeat enemy or just waiting.";
//			txt1.text = "This part means your population limit.\nHere means you can hire 25 soilders and now you already have 1. The population grows up gradually during the game.";
//			txt2.text = "Look at these 2 progress bars. Your mission is to keep your base not to be destoryed by the zombies. And,try to push as many units as you can to the enemy base.When the purple bar drains,you win.";
//			txt3.text = "Rages,accumulates automatically during the game.This power can be used to call some 'SUPER ATTACK'.Which is really amazing.Try it when  collected enough.\n(Your can see tutorials from menu button.)";
//			btnNext.gameObject.GetComponentInChildren<Text>().text = "Next";
//			btnPrev.gameObject.GetComponentInChildren<Text>().text = "Prev.";
//			btnPlay.gameObject.GetComponentInChildren<Text>().text = "Play";
//		} else {
//			
//			txt0.text = "点击这里的图标可以雇佣士兵。注意雇佣是消耗金币的。所以你只能在图标亮起的时候使用。金币会随着时间或者你的杀敌数自动增加。";	
//			txt1.text = "这部分指示的是你的人口上限。这里意味着你可以最多雇佣25个士兵，且当前你有一个士兵。人口上限会随着时间慢慢增加。";
//			txt2.text = "注意这两个进度条。你的任务就是把足够多的单位推送到对面而不能让僵尸大军摧毁你的基地。敌人的紫色血条耗尽，你就能过关了。";
//			txt3.text = "愤怒值，会随着杀敌自动增长。这个能量可以用来使用发动一些超强攻击。非常疯狂。\n当它积满时请别忘尝试下。\n(现在你可以通过[菜单]按钮重看教程)";
//			
//			btnNext.gameObject.GetComponentInChildren<Text>().text = "下一条";
//			btnPrev.gameObject.GetComponentInChildren<Text>().text = "上一条";
//			btnPlay.gameObject.GetComponentInChildren<Text>().text = "开始玩";
//		}
		txt0.text = Localization.Instance.GetString ("tutor0");
		txt1.text = Localization.Instance.GetString ("tutor1");
		txt2.text = Localization.Instance.GetString ("tutor2");
		txt3.text = Localization.Instance.GetString ("tutor3");

		btnNext.gameObject.GetComponentInChildren<Text>().text =  Localization.Instance.GetString ("Next");
		btnPrev.gameObject.GetComponentInChildren<Text>().text =  Localization.Instance.GetString ("Prev");
		btnPlay.gameObject.GetComponentInChildren<Text>().text =  Localization.Instance.GetString ("Play");

		refresh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void refresh(){
		cPage = 0;
		if (Time.timeScale != 0) {
			GameData.getInstance ().timeScale = Time.timeScale;
			Time.timeScale = 0;
		}
//		tutorpanels = new GameObject[]{tutor0,tutor1,tutor2,tutor3};
	
		for (int i = 0; i<nPage; i++) {
			if(tutorpanels!=null)
			tutorpanels[i].SetActive(false);	
		}
		if (tutorpanels == null)
						return;
		tutorpanels[0].SetActive(true);
		btnPrev.interactable = false;
		btnNext.gameObject.SetActive(true);
		btnPlay.gameObject.SetActive(false);
	}
	
	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnNext":
			
			tutorpanels[cPage].SetActive(false);
			if(cPage+1 < nPage-1){
				
				btnPrev.interactable = true;
				cPage++;
			}else{
				tutorpanels[nPage-1].SetActive(true);
				btnNext.gameObject.SetActive(false);
				btnPlay.gameObject.SetActive(true);
				cPage = nPage-1;
			}
			tutorpanels[cPage].SetActive(true);
			break;
		case "btnPrev":
			tutorpanels[cPage].SetActive(false);
			btnNext.gameObject.SetActive(true);
			if(cPage - 1 <= 0){
				btnPrev.interactable = false;
				cPage = 0;
			}else{
				
				cPage--;
			}
			tutorpanels[cPage].SetActive(true);
			break;
		case "btnPlay":
			gameObject.SetActive(false);
			Time.timeScale = GameData.getInstance().timeScale;
			break;
		}
	}
}
