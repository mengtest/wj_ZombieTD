using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStartMenu : MonoBehaviour {

	// Use this for initialization
	public GameObject btnStart,btnCon,btnYes,btnNo;
	public Text gameText;
	void Start () {
		
		btnStart.GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnStart");
		btnCon.GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnContinue");
		btnYes.GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnYes");
		btnNo.GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnNo");
		gameText.GetComponent<Text>().text =  Localization.Instance.GetString ("startTip");

		if (PanelFade.returnType == 3) {
			continueLevel ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject panelTutor, panelTip;
	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnStart":


			btnStart.SetActive (false);
			btnCon.SetActive (false);
			btnYes.SetActive (true);
			btnNo.SetActive (true);
			gameText.gameObject.SetActive (true);



			break;
		case "btnContinue":
			continueLevel ();
			break;
		case "btnYes":
			GameData.getInstance ().cLevel = 0;
			gameObject.SetActive (false);
			if (GameData.getInstance ().cLevel == 0) {
				if (panelTutor != null)
					panelTutor.SetActive (true);
			}

			if (panelTip != null) {
				panelTip.SetActive (true);
				panelTip.SendMessage ("showLevel");
			}

			GameObject.Find ("enemy").GetComponent<LevelGenerator> ().enabled = true;
			GameObject.Find ("PanelTopBar").GetComponent<PanelTopBar> ().refreshView ();
			break;
		case "btnNo":
			btnYes.SetActive (false);
			btnNo.SetActive (false);
			gameText.gameObject.SetActive (false);
			btnStart.SetActive (true);
			btnCon.SetActive (true);
			break;
		}

	}
	void continueLevel(){
		GameData.getInstance ().cLevel = GameData.getInstance().levelPassed;
		gameObject.SetActive (false);
		if (GameData.getInstance ().cLevel == 0) {
			if (panelTutor != null)
				panelTutor.SetActive (true);
		}

		if (panelTip != null) {
			panelTip.SetActive (true);
			panelTip.SendMessage ("showLevel");
		}

		GameObject.Find ("enemy").GetComponent<LevelGenerator> ().enabled = true;
		GameObject.Find ("PanelTopBar").GetComponent<PanelTopBar> ().refreshView ();
	}
}
