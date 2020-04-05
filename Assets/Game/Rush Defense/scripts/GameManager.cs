using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;
using UnityEngine.UI;

public class GameManager {


	public static GameManager instance;
	public static GameManager getInstance(){
		if (instance == null) {

			instance = new GameManager();

			loadPrefs ();
			//store
			instance.initStore();
			instance.initAds();
			instance.initLocalize();
			instance.initGameCenter();

			instance.music = GameObject.Find ("music");

		
		}
		return instance;

	}
	public static void loadPrefs(){
		//		PlayerPrefs.DeleteAll ();
		for (int  i=0; i<5; i++) {
			string tstate = PlayerPrefs.GetString ("hero"+i+"state", "0_0_0");
			processState (i,tstate);
			//			Debug.Log(tstate);
		}

		GameData.getInstance ().nCrystal = PlayerPrefs.GetInt ("cystalnum", 0);

		GameData.getInstance ().nSuperBox = PlayerPrefs.GetInt ("nSuperBox", 2);
		GameData.getInstance ().levelPassed = PlayerPrefs.GetInt ("levelPassed", 0);



		GameData.getInstance().isSoundOn = (int)PlayerPrefs.GetInt("sound",0);
		GameData.getInstance ().isSfxOn = (int)PlayerPrefs.GetInt ("sfx", 0);

		GameData.isAds = (int)PlayerPrefs.GetInt ("isAds", 0) == 0 ? true : false ;

	}

	// Update is called once per frame
	void Update () {

	}

	public static void processState(int no,string state){
		string[] tstates = state.Split("_"[0]);

		int[] tstateInt = new int[3];
		for (int i = 0; i<tstateInt.Length; i++) {
			tstateInt[i] = int.Parse(tstates[i]);	
		}

		switch (no) {
		case 0:
			GameData.getInstance().hero0State = tstateInt;
			break;
		case 1:
			GameData.getInstance().hero1State = tstateInt;
			break;
		case 2:
			GameData.getInstance().hero2State = tstateInt;
			break;
		case 3:
			GameData.getInstance().hero3State = tstateInt;
			break;
		case 4:
			GameData.getInstance().hero4State = tstateInt;
			break;

		}
	}

	public string fontName;
	public static string systemLanguage;
	public bool isEn = true;
	public void initLocalize(){
		Localization.Instance.SetLanguage (GameData.getInstance().GetSystemLaguage());
	}


	GameObject music;//sound control instance
	/// <summary>
	/// Plaies the music.
	/// </summary>
	/// <param name="str">String.</param>
	/// <param name="isforce">If set to <c>true</c> isforce.</param>
	public void playMusic(string str,bool isforce = false){

		//do not play the same music againDebug.Log (musicName+"__"+str);
		if (!isforce) {
			if (bgMusic != null && musicName == str) {
				return;
			}
		}


		if (!music)
			return;


		AudioSource tmusic = null;

		AudioClip clip = (AudioClip)Resources.Load ("sound\\"+str, typeof(AudioClip));//调用Resources方法加载AudioClip资源

		Debug.Log (clip);
		if (GameData.getInstance ().isSoundOn == 0) {
			if (bgMusic)
				bgMusic.Stop ();
			tmusic = music.GetComponent<musicScript> ().PlayAudioClip (clip,true);
			if (str.Substring (0, 2) == "bg") {
				musicName = str;
				bgMusic = tmusic;

			}
		}

	}






	List<AudioSource> currentSFX = new List<AudioSource>();
	Dictionary<string,int> sfxdic = new Dictionary<string,int>();

	AudioSource cWalk = new AudioSource (); //sometime for continous sound like walk steps.
	/// <summary>
	/// Plaies the sfx.
	/// </summary>
	/// <returns>The sfx.</returns>
	/// <param name="str">String.</param>
	public AudioSource playSfx(string str){
		AudioSource sfxSound = null;

		if (!music)
			return null;
		AudioClip clip = (AudioClip)Resources.Load ("sound\\"+str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
		if (GameData.getInstance ().isSfxOn == 0) {
			sfxSound = music.GetComponent<musicScript> ().PlayAudioClip (clip);
			if (sfxSound != null) {
				if (sfxdic.ContainsKey (str) == false || sfxdic [str] != 1) {
					currentSFX.Add (sfxSound);

					sfxdic [str] = 1;
					if(str == "walk"){
						cWalk = sfxSound;
					}
				}
			}	
		}	

		return sfxSound;


	}


	AudioSource bgMusic = new AudioSource();//record background music
	public string musicName = "";
	/// <summary>
	/// Stops the background music.
	/// </summary>
	public void stopBGMusic(){
		if(bgMusic){
			bgMusic.Stop();
			musicName = "";
		}
	}
	/// <summary>
	/// Stops all sound effect.
	/// </summary>
	public void stopAllSFX(){
		foreach(AudioSource taudio in currentSFX){
			if(taudio!=null)taudio.Stop();
		}
		currentSFX.Clear ();
		sfxdic.Clear ();
	}

	/// <summary>
	/// detect a certain sound whether is playing
	/// </summary>
	/// <returns><c>true</c>, if playing sfx was ised, <c>false</c> otherwise.</returns>
	/// <param name="str">String.</param>
	public bool isPlayingSfx(string str){
		bool isPlaying = false;
		if (sfxdic.ContainsKey(str) && sfxdic [str] == 1) {
			isPlaying = true;
		}
		return isPlaying;

	}

	/// <summary>
	/// Stops the music.
	/// </summary>
	/// <param name="musicName">Music name.</param>
	public void stopMusic(string musicName = ""){
		if (music) {
			AudioSource[] as1 = music.GetComponentsInChildren<AudioSource> ();
			foreach (AudioSource tas in as1) {
				if(musicName == ""){
					tas.Stop ();
					break;
				}else{
					if(tas && tas.clip){
						string clipname = (tas.clip.name);
						if(clipname == musicName){
							tas.Stop();


							musicName = "";
							if(sfxdic.ContainsKey(clipname)){
								sfxdic[clipname] = 0;
								if (clipname == "walk") {
									if (cWalk != null) {
										cWalk.Stop ();
										cWalk = null;
									}
								}
							}
							break;
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// switch the sound.
	/// </summary>
	public void toggleSound(){


		int soundState  = GameData.getInstance().isSoundOn;




	}



	public void submitGameCenter(){
		if(!isAuthored) {
			//Debug.Log("authenticating...");
			//initGameCenter();
		}else{
			//			Debug.Log("submitting score...");
			//			int totalScore = getAllScore();
			int bestScore = GameData.getInstance().bestScore;			
			ReportScore(Const.LEADER_BOARD_ID,bestScore);
			//atansoft
		}

	}

	//=================================GameCenter======================================
	//	LeaderboardDesc leaderboardWp8;
	public void initGameCenter(){
		Social.localUser.Authenticate(HandleAuthenticated);

	}


	private bool isAuthored = false;
	private void HandleAuthenticated(bool success)
	{
		if (success) {


			isAuthored = true;
			submitGameCenter();

		}



	}





	public void ShowAchievements()
	{
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI();
		}
	}

	// leaderboard

	public void ReportScore(string leaderboardId, long score)
	{
		Debug.Log("submitting score to GC...");
		if (Social.localUser.authenticated) {
			// Social.ReportScore(score, leaderboardId, HandleScoreReported);
		}
	}

	public void HandleScoreReported(bool success)
	{
		//        Debug.Log("*** HandleScoreReported: success = " + success);
	}

	public void ShowLeaderboard()
	{
		Debug.Log("showLeaderboard");
		if (Social.localUser.authenticated) {
			Social.ShowLeaderboardUI();
		}
	}
	
    
    //ads
	void initAds(){

		hideBanner (true);

		Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;


		ShowInterestitial();
	}

	public void hideBanner(bool isHidden){
	}
	public void showBanner(){
		// if(GameData.isAds){
		// 	float tradio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
		// }
	}


	public void CacheInterestial(){
		// if (GameData.isAds) {
		// 	Chartboost.cacheInterstitial(CBLocation.Default);
		// }
	}

	public void ShowInterestitial(){
		// if (GameData.isAds) {
		// 	Chartboost.showInterstitial(CBLocation.Default);		
		// }
	}

	public void CacheVideo(){
		// Chartboost.cacheRewardedVideo(CBLocation.Default);
	}

	//chartboost

	void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
	}

	void didDismissRewardedVideo(CBLocation location) {
		Debug.Log("didDismissRewardedVideo: " + location);
	}

	void didCloseRewardedVideo(CBLocation location) {
		Debug.Log("didCloseRewardedVideo: " + location);
	}

	void didClickRewardedVideo(CBLocation location) {
		Debug.Log("didClickRewardedVideo: " + location);
	}
	public bool isCachedVideo = false;
	void didCacheRewardedVideo(CBLocation location) {
		Debug.Log("didCacheRewardedVideo: " + location);
		isCachedVideo = true;
	}

	bool shouldDisplayRewardedVideo(CBLocation location) {
		Debug.Log("shouldDisplayRewardedVideo: " + location);
		return true;
	}

	void didCompleteRewardedVideo(CBLocation location, int reward) {
		Debug.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));


		//add 10 coins;
		GameData.getInstance ().nCrystal += 2;
		PlayerPrefs.SetInt ("cystalnum", GameData.getInstance ().nCrystal);
		GameObject panelupgrade = GameObject.Find("PanelUpgrade");
		if(panelupgrade != null){
			panelupgrade.SendMessage("refreshView");
		}
		PlayerPrefs.Save ();


		Chartboost.cacheInterstitial(CBLocation.Default);

		isCachedVideo = false;
	}

	void didDisplayRewardedVideo(CBLocation location){
		Debug.Log("didDisplayRewardedVideo: " + location);
	}



	//=============================================GameCenter=========================


	//in app
	//		public const string NON_CONSUMABLE0 = "com.xxx.unlockall";//only use this for this version
	public const string CONSUMABLE0 = "20T_deathsquads";
	public const string CONSUMABLE1 = "45T_deathsquads";
	public const string CONSUMABLE2 = "70T_deathsquads";
	public const string CONSUMABLE3 = "100T_deathsquads";

	// public static Purchaser purchaser;
	void initStore(){

		GameObject music  =  GameObject.Find ("music");
		if(music!=null){
			// purchaser =music.GetComponent<Purchaser> ();
		}
	}


	//only for google store if have one.Otherwise just ignore.
	public const string publishKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2O/9/H7jYjOsLFT/uSy3ZEk5KaNg1xx60RN7yWJaoQZ7qMeLy4hsVB3IpgMXgiYFiKELkBaUEkObiPDlCxcHnWVlhnzJBvTfeCPrYNVOOSJFZrXdotp5L0iS2NVHjnllM+HA1M0W2eSNjdYzdLmZl1bxTpXa4th+dVli9lZu7B7C2ly79i/hGTmvaClzPBNyX+Rtj7Bmo336zh2lYbRdpD5glozUq+10u91PMDPH+jqhx10eyZpiapr8dFqXl5diMiobknw9CgcjxqMTVBQHK6hS0qYKPmUDONquJn280fBs1PTeA6NMG03gb9FLESKFclcuEZtvM8ZwMMRxSLA9GwIDAQAB";

	public bool test = true;
	/// <summary>
	/// Buy item
	/// </summary>
	/// <param name="index">Index.</param>
	public void buy(int index){
		if (test) {
			purchansedCallback ("pack" + index);
		} else {

			switch (index) {
			case 0:
				// purchaser.BuyConsumable ("pack0");
				break;
			case 1:
				// purchaser.BuyConsumable ("pack1");
				break;
			case 2:
				// purchaser.BuyConsumable ("pack2");
				break;
			case 3:
				// purchaser.BuyConsumable ("pack3");
				break;
			}





		}
	}








	public void restore(){
		// purchaser.RestorePurchases ();
	}

	/// <summary>
	/// This will be called when a purchase completes.
	/// </summary>
	public void purchansedCallback(string id) {
		Debug.Log(id);
		bool buyenough = false;
		switch (id) {
		case "pack0":
			buyenough = true;
			GameData.getInstance().nSuperBox+=20;
			break;
		case "pack1":
			buyenough = true;
			GameData.getInstance().nSuperBox+=45;
			break;
		case "pack2":
			buyenough = true;
			GameData.getInstance().nSuperBox+=70;
			break;
		case "pack3":
			buyenough = true;
			GameData.getInstance().nSuperBox+=100;
			break;
		}


		PlayerPrefs.SetInt ("nSuperBox",GameData.getInstance().nSuperBox);
		//removeAds
		//uncomment these if your want the ads to be disabled after a real money purchanse
//		PlayerPrefs.SetInt ("isAds", 1);
//		GameData.isAds = false;
//		hideBanner (true);
//		PlayerPrefs.Save ();


		GameObject topBar = GameObject.Find("PanelTopBar");
		if (topBar != null) {
			topBar.SendMessage("refreshView");		
		}
		GameObject panelBuy = GameObject.Find("PanelBuy");
		if (panelBuy != null) {
			panelBuy.SendMessage("refreshView",SendMessageOptions.DontRequireReceiver);
		}

	}
}
