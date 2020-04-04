using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelBuy : MonoBehaviour {
	
	// Use this for initialization
	

	string[] num = {"x20","x45","x70","x100"};
	
//	string[] itemNameCN = {"超级援助","超级援助","超级援助","超级援助"};
//	string[] priceCN = {"￥6 (去广告)","￥12 (去广告)","￥18 (去广告)","￥25 (去广告)"};
	void Start () {
		GameManager.getInstance ();
		Transform tbg = transform.Find("bg");	
		Text txt_titleMain = tbg.Find ("Text_title").GetComponent<Text> ();
		Text txt_OK = tbg.Find ("btnOK").GetComponentInChildren<Text> ();
		for (int i = 0; i<4; i++) {
			Transform tsel = tbg.transform.Find("selbg"+i);
			Text txt_price = tsel.transform.Find("Text_price").GetComponent<Text>();
			Text txt_title = tsel.transform.Find("Text_title").GetComponent<Text>();
			Text txt_num = tsel.transform.Find("Text_num").GetComponent<Text>();
			Text txt_buy = tsel.transform.Find("btnBuy").GetComponentInChildren<Text>();
//			if (GameManager.getInstance ().isEn) {
//				txt_price.text = priceEN[i];
//				txt_title.text = itemNameEN[i];
//				txt_buy.text = "BUY";
//			}else{
//				txt_price.text = priceCN[i];
//				txt_title.text = itemNameCN[i];
//				txt_buy.text = "购买";
//			}
			txt_price.text = Localization.Instance.GetString ("price"+i);
			txt_title.text = Localization.Instance.GetString ("superSupport");
			txt_buy.text = Localization.Instance.GetString ("BUY");
			txt_num.text = num [i];
		}
//		if (GameManager.getInstance ().isEn) {
//			txt_titleMain.text = "ITEM SHOP";	
//			txt_OK.text = "OK";
//		} else {
//			txt_titleMain.text = "战争商店";		
//			txt_OK.text = "关闭";
//		}
		txt_titleMain.text = Localization.Instance.GetString("itemShop");
		txt_OK.text = Localization.Instance.GetString("btnClose");

		refreshView ();
	}
	
	public void OnOKClick(){
		gameObject.SetActive (false);
		Time.timeScale = GameData.getInstance ().timeScale;
		if(Application.loadedLevelName == "Game")
		GameManager.getInstance().hideBanner(false);
	}

	public void OnBuy(int n){
		GameManager.getInstance ().buy (n);
	}

	void refreshView(){
		Text nTresue = GameObject.Find ("superBox").GetComponentInChildren<Text> ();
		nTresue.text = "x" + GameData.getInstance ().nSuperBox;

	}
}
