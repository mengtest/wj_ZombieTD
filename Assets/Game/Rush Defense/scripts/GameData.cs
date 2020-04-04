using UnityEngine;
using System.Collections;

public class GameData  {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static GameData instance;
	public static GameData getInstance(){
		if (instance == null) {
			instance = new GameData();		
		}
		return instance;
	}
	//general
	public int isSoundOn = 0,isSfxOn = 0;
	public static string lastWindow = "";
	public static bool isAds = true;

	/// <summary>
	/// The 3 states(health,speed,damage) for each hero
	/// </summary>
	public int[] hero0State = {0,0,0};
	public int[] hero1State = {0,0,0};
	public int[] hero2State = {0,0,0};
	public int[] hero3State = {0,0,0};
	public int[] hero4State = {0,0,0};
	
	public float timeScale = 1;
	public bool isLock = false;
	public int cLevel = 0;
	public int totalLevel = 6;
	public int levelPassed = 0;
	//game
	public int coin = 0;
	public int nPeople = 0;
	public int people = 5;
	public int rage = 0;
	public int timeelaspe = 0;
	public int nCrystal = 0;
	
	public int nSuperBox = 0;

	
	//game data
	public int heroBuild = 0;
	public int heroDied = 0;
	public int enemyKilled = 0;
	public int beKilled = 0;
	public int unitScore = 0;
	public int otherScore = 0;//ex. using super attacks

	public int bestScore = 0;
	
	public void init(){
		timeScale = 1;
		coin = 100;nPeople = 0;people = 5;rage = 0;timeelaspe = 0;
		heroBuild = 0;heroDied = 0;enemyKilled = 0;beKilled = 0;unitScore = 0;otherScore = 0;
		isPause = false;
		isLock = false; isWin = false;isFail = false;
	}
	
	public bool isPause = false;
	public bool isWin = false;
	public bool isFail = false;


	/// <summary>
	/// Gets the system laguage.
	/// </summary>
	/// <returns>The system laguage.</returns>
	public int GetSystemLaguage(){
		int returnValue = 0;
		switch (Application.systemLanguage) {
		case SystemLanguage.Chinese:
			returnValue = 1;
			break;
		case SystemLanguage.ChineseSimplified:
			returnValue = 1;
			break;
		case SystemLanguage.ChineseTraditional:
			returnValue = 1;
			break;
		default:
			returnValue = 0;
			break;

		}
		returnValue = 0;//test
		return returnValue;
	}
	
}
