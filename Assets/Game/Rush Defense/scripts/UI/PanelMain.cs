using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelMain : MonoBehaviour {
	
	
	public Text btnStart,btnMore,btnReview;
	public GameObject titleCN,titleEN;
	public Toggle toggleMusic,toggleSFX;
	GameObject panelFade;
	// Use this for initialization
	void Start () {
//		if (GameManager.getInstance ().isEn) {
//			btnStart.text = "Game Start";
//			btnMore.text = "More Games";
//			btnReview.text = "Rate & Review";
//			titleCN.SetActive(false);
//			titleEN.SetActive(true);
//		} else {
//			btnStart.text = "游戏开始";
//			btnMore.text = "更多游戏";
//			btnReview.text = "评价打分";	
//			titleCN.SetActive(true);
//			titleEN.SetActive(false);
//		}
		btnStart.text = Localization.Instance.GetString ("btnStart");
		btnMore.text = Localization.Instance.GetString ("btnMore");
		btnReview.text = Localization.Instance.GetString ("btnReview");


		panelFade = GameObject.Find ("PanelFade");
		toggleMusic.isOn = GameData.getInstance ().isSoundOn == 1 ? true : false;//0 is on
		toggleSFX.isOn = GameData.getInstance ().isSfxOn == 1 ? true : false;

		GameManager.getInstance ().hideBanner (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject panelShop;
	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnStart":
			panelFade.SendMessage("returnTitle",2);
			break;
		case "btnMore":
			if (Application.platform == RuntimePlatform.WP8Player) {
	
				
			} else {
				//Application.OpenURL ("http://itunes.apple.com/WebObjects/MZSearch.woa/wa/search?submit=seeAllLockups&media=software&entity=software&term=atansoftgame");
				#if (UNITY_IPHONE || UNITY_ANDROID)
				Application.OpenURL ("http://itunes.apple.com/WebObjects/MZSearch.woa/wa/search?submit=seeAllLockups&media=software&entity=software&term=hitcode");
				//Application.OpenURL("http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Ddigital-text&field-keywords=atansoftgame");
//				Application.OpenURL("https://play.google.com/store/search?atansoftgame");
				#endif
			}
			break;
		case "btnReview":
//			UniRate.Instance.RateIfNetworkAvailable();
			break;
		case "btnShop":
			panelShop.SetActive(true);
			break;
		case "btnGC":
			GameManager.getInstance().ShowLeaderboard();
			break;
		}
	}
	
	public void OnToggle(Toggle toggle){
		switch (toggle.gameObject.name) {
		case "ToggleMusic":
			
			GameData.getInstance().isSoundOn = toggle.isOn ? 1 : 0;
			
			if(toggle.isOn){
				GameManager.getInstance().stopBGMusic();
			}else{
				GameManager.getInstance().playMusic("bgsound");
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
