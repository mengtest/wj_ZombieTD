using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelTopBar : MonoBehaviour {
	
	// Use this for initialization
	Text textCoin,textPeople,textRage,textTime,textNsuperbox,textMenu;
	Transform mainBG;
	Toggle toggleMusic,toggleSFX;

	public GameObject lock1,lock2,lock3,lock4,lock5,lock6,locksupport;
	void OnEnable () {
		GameManager.getInstance ();
		GameData.getInstance ().init ();

		mainBG = transform.Find ("mainbg");
		textCoin = mainBG.Find("TextCoin").GetComponent<Text>();
		textPeople = mainBG.Find("TextPeople").GetComponent<Text>();
		textRage = mainBG.Find("TextRage").GetComponent<Text>();
		textTime =mainBG.Find("TextTime").GetComponent<Text>();
		textMenu = mainBG.Find ("btnGameMenu").GetComponentInChildren<Text> ();
		textNsuperbox = GameObject.Find("superBox").GetComponentInChildren<Text>();

		toggleMusic = mainBG.Find ("ToggleMusic").GetComponent<Toggle> ();
		toggleSFX = mainBG.Find ("ToggleSfx").GetComponent<Toggle> ();
		refreshView ();
		refreshTime ();
		


		textMenu.text = Localization.Instance.GetString ("menu");
		StartCoroutine ("tick");

		toggleMusic.isOn = GameData.getInstance ().isSoundOn == 1 ? true : false;//0 is on
		toggleSFX.isOn = GameData.getInstance ().isSfxOn == 1 ? true : false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator tick(){
		while (true) {
			yield return new WaitForSeconds(1);
			if(!GameData.getInstance().isPause){
				GameData.getInstance ().timeelaspe++;
				refreshTime();
			}
		}
	}
	
	void refreshTime(){
		int tcTime = GameData.getInstance ().timeelaspe;
		int tmin = Mathf.FloorToInt (tcTime / 60);
		int tsec = tcTime - tmin * 60;
		string tminstr = "", tsecstr = "";
		
		if (tmin < 10) {
			tminstr = "0" + tmin.ToString ();
		} else {
			tminstr = tmin.ToString();		
		}
		if (tsec < 10) {
			tsecstr = "0" + tsec.ToString ();
		} else {
			tsecstr = tsec.ToString ();
		}
		textTime.text =tminstr + ":" + tsecstr;
		
	}
	
	
	
	public void upSpeed(GameObject target){
		Toggle toggle = target.GetComponent<Toggle> ();
		bool tison = toggle.isOn;
		if (tison) {
			Time.timeScale = 3;		
		} else {
			Time.timeScale = 1;		
		}
		GameData.getInstance ().timeScale = Time.timeScale;
		target.transform.Find ("Background").GetComponent<Image> ().enabled = !tison;
	}
	
	public void refreshView(){

		if (GameData.getInstance ().cLevel >= 1) {//level 2
			lock1.SetActive(false);
			lock4.SetActive(false);	
			locksupport.SetActive(false);	
		} 
		if (GameData.getInstance ().cLevel >= 2) {//level 3
			lock2.SetActive(false);
			lock5.SetActive(false);	
		}
		if (GameData.getInstance ().cLevel >= 3) {//level 4
			lock3.SetActive(false);
			lock6.SetActive(false);	
		}
		
		textCoin.text = GameData.getInstance ().coin.ToString();
		textPeople.text = GameData.getInstance().nPeople +"/"+ GameData.getInstance ().people.ToString ();
		if (GameData.getInstance ().nPeople > GameData.getInstance ().people) {
			textPeople.color = Color.red;		
		} else {
			textPeople.color = Color.black;				
		}
		textRage.text = GameData.getInstance ().rage.ToString ();
		textNsuperbox.text = "x" + GameData.getInstance ().nSuperBox;


	}
	
	public GameObject panelGameMenu;
	public void OnClickMainMenu(){
		GameData.getInstance ().timeScale = Time.timeScale;
		panelGameMenu.SetActive (true);
		Time.timeScale = 0;
		//		panelGameMenu.SendMessage("refreshview");
	}

	public void OnToggleMusic(Toggle toggle){
		switch (toggle.gameObject.name) {
		case "ToggleMusic":

			GameData.getInstance().isSoundOn = toggle.isOn ? 1 : 0;
		
			if(toggle.isOn){
				GameManager.getInstance().stopBGMusic();
			}else{
				GameManager.getInstance().playMusic("bgmusic");
			}
			PlayerPrefs.SetInt("sound",GameData.getInstance().isSoundOn);

			break;
		case "ToggleSfx":
			GameData.getInstance().isSfxOn = toggle.isOn ? 1 : 0;
			if(toggle.isOn){
				GameManager.getInstance().stopAllSFX();
			}
			PlayerPrefs.SetInt("sfx",GameData.getInstance().isSfxOn);
			break;
		}
	}
}
