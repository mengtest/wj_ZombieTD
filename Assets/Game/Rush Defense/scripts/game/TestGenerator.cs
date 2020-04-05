using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using DG.Tweening;
public class TestGenerator : MonoBehaviour {
	
	// Use this for initialization
	GameObject panelTopBar;
	GameObject bg;
	Vector3 bgStartPos;
	public bool isTesting = false;
	void Start () {
		GameManager.getInstance ();
		StartCoroutine("testGenerator");

		float tradio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
		
		
		
		if(tradio< 1030f/768f){
		//if(UnityEngine.iOS.Device.generation.ToString().IndexOf("iPad")!=-1){
			Camera.main.orthographicSize = 2.3f;
		}else{
			if(tradio > 1130/640)
				Camera.main.orthographicSize = 2f;
		}

		

		//Camera.main.orthographicSize = 2f;
	
		
		#if UNITY_EDITOR
		//		Camera.main.orthographicSize = 2.3f;
		//		GameObject.Find ("basement").transform.Translate (.4f, 0, 0);
		#endif
		transform.parent = transform.parent.parent;
		
		
		panelTopBar = GameObject.Find("PanelTopBar");


		GameManager.getInstance ().CacheInterestial();
		GameManager.getInstance ().CacheVideo();
		if (SceneManager.GetActiveScene().name == "Game") {
			GameManager.getInstance ().playMusic ("bgmusic", true);
			// GameManager.getInstance().showBanner();
			GameManager.getInstance ().hideBanner (false);
		} else if (SceneManager.GetActiveScene().name == "MainMenu") {
			GameManager.getInstance ().playMusic ("bgsound");
			GameManager.getInstance().hideBanner(true);
		}
		bg = GameObject.Find("bg");
		bgStartPos = bg.transform.position;
		
		if (isTesting) {
			GameData.getInstance().cLevel = -1;
			StartCoroutine("autoHero");		
			StopCoroutine("testGenerator");
		}
	}
	
	IEnumerator autoHero(){
		while (true) {
			yield return new WaitForSeconds (4);
			int trnd = Random.Range(1,6);
			if(trnd == 5 ){
				createHero (2);
			}else{
				createHero (1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	int nTime = 0;
	int[] maxPeople = {20,30,50,100,100,120};
	int addPopuGap = 20;
	IEnumerator testGenerator(){
		while (true) {
			yield return new WaitForSeconds (1f);
			nTime++;
			if(nTime % addPopuGap == 0 && nTime > 0){
				int maxPeopleThisLevel = maxPeople[GameData.getInstance().cLevel];
				if(GameData.getInstance().people < maxPeopleThisLevel){
					GameData.getInstance().people+=5;
					panelTopBar.SendMessage("refreshView");
					addPopuGap+=20;
				}
			}
			
			if(nTime % 5 == 0){
				GameData.getInstance().coin+=2;
				panelTopBar.SendMessage("refreshView");
			}
		}
	}
	public GameObject panelTip;
	public GameObject createHero(int heroType){
		GameObject tHero = null;
		GameObject tnewHero = null;
		float toffsetY;
		
		
		
		switch (heroType) {
		case 1:
			tHero = GameObject.Find("blademan");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_blademan";
			GameData.getInstance().coin -= 20;
			
			break;
		case 2:
			tHero = GameObject.Find("pistolman");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_pistolman";
			GameData.getInstance().coin -= 40;
			break;
		case 3:
			tHero = GameObject.Find("bombman");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_bombman";
			GameData.getInstance().coin -= 100;
			break;
		case 4:
			tHero = GameObject.Find("gunman");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_gunman";
			GameData.getInstance().coin -= 80;
			break;
		case 5:
			tHero = GameObject.Find("sawman");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_sawman";
			GameData.getInstance().coin -= 200;
			break;
		case 1111:
			tHero = GameObject.Find("blademan");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_blademan";
			break;
		case 1112:
			tHero = GameObject.Find("pistolman");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewHero = GameObject.Instantiate(tHero,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewHero.SendMessage("startMove",SendMessageOptions.DontRequireReceiver);
			tnewHero.name = "new_blademan";
			break;
		}
		tnewHero.tag = "Player";
		
		if(panelTopBar!=null)panelTopBar.SendMessage("refreshView");
		
		return tnewHero;
	}
	public GameObject panelGameOver;
	public void gameWin(){
		if (GameData.getInstance ().isFail)
			return;
		if (GameData.getInstance ().cLevel >= GameData.getInstance ().levelPassed) {
			GameData.getInstance().nCrystal+=3;
		} else {
			
			GameData.getInstance().nCrystal+=1;
		}
		PlayerPrefs.SetInt ("cystalnum", GameData.getInstance ().nCrystal);
		
		panelGameOver.SetActive (true);
		GameData.getInstance ().isWin = true;
		GameData.getInstance ().isFail = false;
		panelGameOver.SendMessage("refreshView");
//		Camera.main.DOShakePosition (1f, .5f, 10).SetUpdate (true).OnComplete(resetPos);
		ATween.ShakePosition(Camera.main.transform.gameObject, 
			ATween.Hash("x",1, "time", 1, "delay",0,"easetype", ATween.EaseType.easeInOutQuint));
		
		if (GameData.getInstance ().cLevel == GameData.getInstance ().levelPassed) {
			GameData.getInstance().levelPassed++;
			PlayerPrefs.SetInt ("levelPassed", GameData.getInstance().levelPassed);
		}
        //google广告
		// GameManager.getInstance ().ShowInterestitial ();
        //播放穿山甲广告
GameObject.Find("ADmanager").GetComponent<Example>().MyLoadAD();
	}
	
	public void gameFailed(){
		if (GameData.getInstance ().isWin)
			return;
		panelGameOver.SetActive (true);
		GameData.getInstance ().isWin = false;
		GameData.getInstance ().isFail = true;
		panelGameOver.SendMessage("refreshView");
//		Camera.main.DOShakePosition(1f,.5f,10).SetUpdate (true).OnComplete(resetPos);
		ATween.ShakePosition(Camera.main.transform.gameObject, 
			ATween.Hash("x",1, "time", 1, "delay",0,"easetype", ATween.EaseType.easeInOutQuint));

            //TODO 播放广告
		// GameManager.getInstance ().ShowInterestitial ();
	}
	
	void resetPos(){
		bg.transform.position = bgStartPos;
	}
}
