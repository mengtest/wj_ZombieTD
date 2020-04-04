using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelGameMenu : MonoBehaviour {

	public Button btnResume,btnTitle,btnLevel,btnTutor,btnRetry;
	public GameObject PanelTutor,panelFade;
	// Use this for initialization
	void Start () {
		refreshView ();


	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnResume":
			Time.timeScale = GameData.getInstance().timeScale;
			break;
		case "btnRetry":
			GameData.getInstance ().init ();
			panelFade.SetActive (true);
			panelFade.SendMessage ("returnTitle", 3);
			Time.timeScale = GameData.getInstance ().timeScale;
		
			break;
		case "btnTitle":
			GameData.getInstance().init();
			panelFade.SetActive(true);
			panelFade.SendMessage("returnTitle",-1);
			Time.timeScale = GameData.getInstance().timeScale;
			GameData.getInstance ().cLevel = 0;
			break;
		case "btnLevel":
			GameData.getInstance().init();
			panelFade.SetActive(true);
			panelFade.SendMessage("returnTitle",2);
			Time.timeScale = GameData.getInstance().timeScale;
			break;
		case "btnTutorial":
			PanelTutor.SetActive(true);
			PanelTutor.SendMessage("refresh");
			break;
		}

		gameObject.SetActive(false);
	}

	public void refreshView(){
		//		if (GameManager.getInstance ().isEn) {
		//			btnResume.GetComponentInChildren<Text>().text = "Resume";
		//			btnRetry.GetComponentInChildren<Text>().text = "Restart";
		//			btnTitle.GetComponentInChildren<Text>().text = "Main Menu";
		//			btnLevel.GetComponentInChildren<Text>().text = "Select Level";
		//			btnTutor.GetComponentInChildren<Text>().text = "Tutorial";
		//		} else {
		//			btnResume.GetComponentInChildren<Text>().text = "继续游戏";
		//			btnRetry.GetComponentInChildren<Text>().text = "重玩本关";
		//			btnTitle.GetComponentInChildren<Text>().text = "回主菜单";
		//			btnLevel.GetComponentInChildren<Text>().text = "选择关卡";
		//			btnTutor.GetComponentInChildren<Text>().text = "观看教程";		
		//		}

		btnResume.GetComponentInChildren<Text>().text = Localization.Instance.GetString ("btnResume");
		btnRetry.GetComponentInChildren<Text>().text = Localization.Instance.GetString ("btnRestart");
		btnTitle.GetComponentInChildren<Text>().text = Localization.Instance.GetString ("btnMainMenu");
//		btnLevel.GetComponentInChildren<Text>().text = Localization.Instance.GetString ("btnSelectLevel");
		btnTutor.GetComponentInChildren<Text>().text = Localization.Instance.GetString ("btnTutorial");

	}
}
