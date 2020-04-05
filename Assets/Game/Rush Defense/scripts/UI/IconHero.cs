using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IconHero : MonoBehaviour {
	
	// Use this for initialization
	GameObject mask;
	Image maskImage;
	bool clearMask;
	float cooltime = 1f;
	Image img;
	public GameObject panelTip;
	GameObject PanelTopBar;
	TestGenerator generator;
	void Start () {
		PanelTopBar = GameObject.Find("PanelTopBar");
		mask = transform.Find ("iconmask").gameObject;
		maskImage = mask.GetComponent<Image> ();
		generator = GameObject.Find ("GameMain").GetComponent<TestGenerator> ();
		
		img = GetComponent<Image> ();
		maskImage.fillAmount = 0;
		switch (name) {
		case "h1":
			cooltime = 1f/50;
			break;
		case "h2":
			cooltime = 1f/100;
			break;
		case "h3":
			cooltime = 1f/500;
			break;
		case "h4":
			cooltime = 1f/400;
			break;
		case "h5":
			cooltime = 1f/2000;
			break;
		case "s1":
			cooltime = 1f/2100;
			break;
		case "s2":
			cooltime = 1f/1800;
			break;
		case "s3":
			cooltime = 1f/2000;
			break;
		case "superBox":
			cooltime = 1f/50;
			break;
			
		}
		clearMask = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate(){
		if (clearMask) {
			maskImage.fillAmount -= cooltime;		
		}

		int cCoin = GameData.getInstance ().coin;
		int cRage = GameData.getInstance ().rage;
		switch (name) {
		case "h1":
			if(cCoin < 20){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "h2":
			if(cCoin < 40){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "h3":
			if(cCoin < 100){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "h4":
			if(cCoin < 80){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "h5":
			if(cCoin < 200){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "s1":
			if(cRage < 1500){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "s2":
			if(cRage < 2000){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "s3":
			if(cRage < 1000){
				img.color = new Color (1, 1, 1, .5f);
			}else{
				img.color = new Color (1, 1, 1, 1);
			}
			break;
		case "superBox":
//			if(GameData.getInstance().nSuperBox < 1){
//				img.color = new Color (1, 1, 1, .5f);
//			}else{
//				img.color = new Color (1, 1, 1, 1);
//			}
			break;
		}
	}
	public GameObject PanelConfirmSupport;
	public void touchme(){

        //播放穿山甲广告
        // GameObject.Find("ADmanager").GetComponent<Example>().MyLoadAD();

		bool canHire = true;
		switch (name) {
		case "h1":
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			if(!isHirable())return;
			generator.createHero(1);
			maskImage.fillAmount = 1;	
			GameData.getInstance().nPeople++;
			GameData.getInstance ().heroBuild++;
			GameData.getInstance().unitScore+=100;
			break;
		case "h2":
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			if(!isHirable())return;
			generator.createHero(2);
			maskImage.fillAmount = 1;	
			GameData.getInstance().nPeople++;
			GameData.getInstance ().heroBuild++;
			GameData.getInstance().unitScore+=200;
			break;
		case "h3":
			if(GameData.getInstance().cLevel < 1){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",2);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			if(!isHirable())return;

			generator.createHero(3);
			maskImage.fillAmount = 1;	
			GameData.getInstance().nPeople++;
			GameData.getInstance ().heroBuild++;
			GameData.getInstance().unitScore+=500;
			break;
		case "h4":
			if(GameData.getInstance().cLevel < 2){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",3);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			if(!isHirable())return;

			generator.createHero(4);
			maskImage.fillAmount = 1;	
			GameData.getInstance().nPeople++;
			GameData.getInstance().unitScore+=400;
			GameData.getInstance ().heroBuild++;
			break;
		case "h5":
			if(GameData.getInstance().cLevel < 3){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",4);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			if(!isHirable())return;
			generator.createHero(5);
			maskImage.fillAmount = 1;	
			GameData.getInstance().nPeople++;
			GameData.getInstance ().heroBuild++;
			GameData.getInstance().unitScore+=1000;
			break;
		case "s1":
			if(GameData.getInstance().cLevel < 1){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",2);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			GameData.getInstance().otherScore+=1500;
			maskImage.fillAmount = 1;	
			GameData.getInstance().rage -= 1500;
			for(int i=0;i<10;i++){
				GameObject thero = generator.createHero(1111);
				thero.transform.Translate(.1f*i,0,0);
				GameData.getInstance().nPeople++;
			}
			for(int i=0;i<5;i++){
				GameObject thero = generator.createHero(1112);
				thero.transform.Translate(.05f*i-1f,0,0);
				GameData.getInstance().nPeople++;
			}
			break;
		case "s2"://tank
			if(GameData.getInstance().cLevel < 2){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",3);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			GameData.getInstance().otherScore+=2000;
			maskImage.fillAmount = 1;	
			GameData.getInstance().rage -= 2000;

			GameObject tank = GameObject.Find("tank");
			tank.SendMessage("startMove");
			break;
		case "s3":
			if(GameData.getInstance().cLevel < 3){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",4);
				return;
			}
			if (maskImage.fillAmount != 0 || img.color.a != 1)
				return;
			GameData.getInstance().otherScore+=1000;
			maskImage.fillAmount = 1;	
			GameData.getInstance().rage -= 1000;

			GameObject plane = GameObject.Find("plane");
			plane.SendMessage("startMove");
			break;
		case "superBox":
			if(GameData.getInstance().cLevel < 1){
				panelTip.SetActive(true);
				panelTip.SendMessage("notUnlocked",2);
				return;
			}
			Time.timeScale = 0;
			PanelConfirmSupport.SetActive(true);
			PanelConfirmSupport.SendMessage("refreshView");
			GameData.getInstance().otherScore+=1000;
			break;
		}
		PanelTopBar.SendMessage("refreshView");
	}


	bool isHirable(){

		if (GameData.getInstance().nPeople >= GameData.getInstance ().people) {
			panelTip.SetActive(true);
			panelTip.SendMessage("PeopleReachLimit");
			return false;		
		}else{
			return true;
		}
	}
	
	
	
}
