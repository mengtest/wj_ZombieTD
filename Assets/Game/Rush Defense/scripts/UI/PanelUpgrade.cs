using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PanelUpgrade : MonoBehaviour {
	
	// Use this for initialization
	List<GameObject> profiles;
	GameObject coinTab;
	Text txtCoin;
	public Text btnOK,btnTitle,btnFreeExp;
	void OnEnable () {
		
		GameManager.getInstance ();
		GameData.getInstance ();
		GameObject blademanUI = GameObject.Find("blademanUI");
		GameObject pistolmanUI = GameObject.Find("pistolmanUI");
		GameObject bombmanUI = GameObject.Find("bombmanUI");
		GameObject gunmanUI = GameObject.Find("gunmanUI");
		GameObject sawmanUI = GameObject.Find("sawmanUI");
		
		coinTab = GameObject.Find("coinTab");
		txtCoin = coinTab.GetComponentInChildren<Text> ();

		
		profiles = new List<GameObject> ();
		profiles.Add (blademanUI);
		profiles.Add (pistolmanUI);
		profiles.Add (bombmanUI);
		profiles.Add (gunmanUI);
		profiles.Add (sawmanUI);
		
		hideAll ();
		profiles [cpage].SetActive (true);
		initView ();
	}
	GameObject[] clights;
	int npages = 5;
	string[] upTextEN  ={"Health","Damage","Speed"};
	void initView(){
		txtCoin.text = "x "+GameData.getInstance ().nCrystal.ToString();
		clights = new GameObject[3];
		
		for (int i=0; i<3; i++) {
			GameObject tsel = GameObject.Find ("sel" + i);
			if(tsel == null)break;	
			
			clights[i] = tsel.transform.Find("lights").gameObject;
			

			tsel.transform.Find("lb_type").GetComponent<Text>().text = Localization.Instance.GetString(upTextEN[i]);
		}
		setLights ();
		btnOK.transform.parent.gameObject.SetActive(true);


		btnOK.text = Localization.Instance.GetString("NextLevel");
		btnTitle.text = Localization.Instance.GetString("btnTitle");
		if(btnFreeExp!=null)
			btnFreeExp.text = Localization.Instance.GetString("FREEEXP");

		if(Application.loadedLevelName == "LevelMenu"){

			btnOK.text = Localization.Instance.GetString("btnClose");
		}
		if (GameData.getInstance ().cLevel >= GameData.getInstance ().totalLevel - 1) {
			btnOK.transform.parent.gameObject.SetActive(false);
		}
		
		if (GameData.getInstance ().isFail) {
			btnOK.text = Localization.Instance.GetString("btnRetry");
		}
		
	}
	// Update is called once per frame
	void Update () {
		
	}
	int cpage = 0;
	public void pageLeft(){
		if (cpage - 1 < 0)
			return;
		hideAll ();
		profiles [cpage - 1].SetActive (true);
		cpage-=1;
		setLights ();
	}
	
	public void pageRight(){
		if (cpage + 1 > npages - 1)
			return;
		hideAll ();
		profiles [cpage + 1].SetActive (true);
		cpage++;
		setLights ();
	}
	
	void hideAll(){
		foreach (GameObject tprofile in profiles) {
			tprofile.SetActive(false);		
		}
	}
	
	void setLights(){
		switch (cpage) {
		case 0:
			setLight(GameData.getInstance().hero0State);
			
			break;
		case 1:
			setLight(GameData.getInstance().hero1State);
			break;
		case 2:
			setLight(GameData.getInstance().hero2State);
			break;
		case 3:
			setLight(GameData.getInstance().hero3State);
			break;
		case 4:
			setLight(GameData.getInstance().hero4State);
			break;
		}
		
	}
	
	
	void plusLights(int no){
		if (GameData.getInstance ().nCrystal <= 0)
			return;
		bool added = false;
		switch (cpage) {
		case 0:
			if(GameData.getInstance().hero0State[no] < 6){
				GameData.getInstance().hero0State[no]++;
				processNum2String(GameData.getInstance().hero0State,0);
				added = true;
			}
			break;
		case 1:
			if(GameData.getInstance().hero1State[no] < 6){
				GameData.getInstance().hero1State[no]++;
				processNum2String(GameData.getInstance().hero1State,1);
				added = true;
			}
			break;
		case 2:
			if(GameData.getInstance().hero2State[no] < 6){
				GameData.getInstance().hero2State[no]++;
				processNum2String(GameData.getInstance().hero2State,2);
				added = true;
			}
			break;
		case 3:
			if(GameData.getInstance().hero3State[no] < 6){
				GameData.getInstance().hero3State[no]++;
				processNum2String(GameData.getInstance().hero3State,3);
				added = true;
			}
			break;
		case 4:
			if(GameData.getInstance().hero4State[no] < 6){
				GameData.getInstance().hero4State[no]++;
				processNum2String(GameData.getInstance().hero4State,4);
				added = true;
			}
			break;
		}
		if (added && GameData.getInstance ().nCrystal > 0) {
			GameData.getInstance ().nCrystal--;
		}
		txtCoin.text = "x "+GameData.getInstance().nCrystal;
		
		PlayerPrefs.SetInt ("cystalnum", GameData.getInstance ().nCrystal);
		
	}
	
	
	
	void setLight(int[] lightsstate){
		
		for (int i = 0; i<lightsstate.Length; i++) {//each hero have 3 state
			foreach(Transform tclight in clights[i].transform){
				tclight.GetComponent<Image>().enabled = false;
			}
			
			for(int j=0;j<=lightsstate[i];j++){
				Transform tlight = clights[i].transform.Find("crystal"+j);
				if(tlight!=null)tlight.GetComponent<Image>().enabled = true;
			}
		}
	}
	
	public void close(){
		gameObject.SetActive (false);
	}
	
	
	public void OnPlusClick(GameObject g){
		switch (g.transform.parent.name) {
		case "sel0":
			plusLights(0);
			break;
		case "sel1":
			plusLights(1);
			break;
		case "sel2":
			plusLights(2);
			break;
		}
		setLights ();
	}
	
	public void OnOKClick(GameObject g){
		PlayerPrefs.Save ();
		gameObject.SetActive (false);
		
	}
	
	public void OnNextClick(){
		PlayerPrefs.Save ();
		GameObject fadepanel = GameObject.Find ("PanelFade");
		fadepanel.SetActive(true);
		if (GameData.getInstance ().isWin) {
			GameData.getInstance ().cLevel++;
		}
		fadepanel.SendMessage("returnTitle",3);
		GameManager.getInstance().hideBanner(false);
	}
	public GameObject panelWatchVideo;
	public void OnExpClick(){
		panelWatchVideo.SetActive (true);
	}
	
	public void OnTitle(){
		PlayerPrefs.Save ();
		GameObject fadepanel = GameObject.Find ("PanelFade");
		fadepanel.SetActive(true);
		fadepanel.SendMessage("returnTitle",-1);
	}
	
	void refreshView(){
		initView ();
	}
	
	
	void processNum2String(int[] nums,int heroNum){
		
		string tPowerNums = "";
		tPowerNums = nums [0].ToString();
		for (int i = 1; i<nums.Length; i++) {
			tPowerNums += "_"+nums[i].ToString();
		}
		print (tPowerNums);
		PlayerPrefs.SetString ("hero" + heroNum + "state", tPowerNums);
	}
}
