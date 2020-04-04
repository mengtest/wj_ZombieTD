using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelConfirmSupport : MonoBehaviour {
	
	// Use this for initialization
	public Text txt_title,txt_context,txt_btnYes,txt_btnNo,txt_btnBuy;
	public Image imageCoin, imageTick;
	public GameObject PanelBuy;
	void Start () {

		refreshView ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnYes":
			if(GameData.getInstance().nSuperBox > 0){
				GameData.getInstance().nSuperBox --;
				GameData.getInstance().coin+=2000;
				GameData.getInstance().rage+=2000;
				
				GameObject.Find("superBox").transform.Find("iconmask").GetComponent<Image>().fillAmount = 1;
				
				GameObject.Find("PanelTopBar").SendMessage("refreshView");
				PlayerPrefs.SetInt ("nSuperBox",GameData.getInstance().nSuperBox);
				PlayerPrefs.Save();
			}
			Time.timeScale = GameData.getInstance ().timeScale;
			break;
		case "btnNo":
			Time.timeScale = GameData.getInstance ().timeScale;
			break;
		case "btnBuy":
			PanelBuy.SetActive(true);
			GameManager.getInstance().hideBanner(true);
			break;
		
		}
		

		gameObject.SetActive (false);		
		
	}
	GameObject btnBuy,btnYes;
	public void refreshView(){
		btnBuy = transform.Find ("btnBuy").gameObject;
		btnYes = transform.Find ("btnYes").gameObject;
		if (GameData.getInstance ().nSuperBox > 0) {
			imageTick.enabled = true;imageCoin.enabled=false;
//			if (GameManager.getInstance ().isEn) {
//				txt_title.text = "Use a supper support??";
//				txt_context.text = "Using a super support will add 2000 golds and 2000 rage immidiently.";
//				txt_btnYes.text = "Yes";
//				txt_btnNo.text = "No";
//			} else {
//				txt_title.text = "使用一个超级援助么？";	
//				txt_context.text = "使用超级援助可以立刻获得\n2000金币和 2000愤怒值。";
//				txt_btnYes.text = "好的";
//				txt_btnNo.text = "不用了";
//			}
			txt_title.text =  Localization.Instance.GetString("askSupport");
			txt_context.text =  Localization.Instance.GetString("supportRule");
			txt_btnYes.text =  Localization.Instance.GetString("btnYes");
			txt_btnNo.text =  Localization.Instance.GetString("btnNo");
		
			btnBuy.GetComponent<Image>().enabled =false;
			btnBuy.GetComponentInChildren<Text>().enabled = false;

			btnYes.GetComponent<Image>().enabled =true;
			btnYes.GetComponentInChildren<Text>().enabled = true;

		} else {
			imageTick.enabled = false;imageCoin.enabled=true;
//			if (GameManager.getInstance ().isEn) {
//				txt_title.text = "Oops.No super support left?";
//				txt_context.text = "Using a super support will add 2000 golds and 2000 rage immidiently.\nGet some from the shop now??";
//				txt_btnYes.text = "Yes";
//				txt_btnNo.text = "No";
//				txt_btnBuy.text = "Shop";
//			} else {
//				txt_title.text = "矮油，没有超级援助可用了哎！";	
//				txt_context.text = "使用超级援助可以立刻获得\n2000金币和 2000愤怒值。\n马上去商店搞一些么？？";
//				txt_btnYes.text = "好的";
//				txt_btnNo.text = "不用了";
//				txt_btnBuy.text = "去商店";
//			}
			txt_context.text =  Localization.Instance.GetString("NoSupport");
			txt_context.text =  Localization.Instance.GetString("UseSupport");
			txt_btnYes.text =  Localization.Instance.GetString("btnYes");
			txt_btnNo.text =  Localization.Instance.GetString("btnNo");
			txt_btnBuy.text = Localization.Instance.GetString ("Shop");

			btnYes.GetComponent<Image>().enabled =false;
			btnYes.GetComponentInChildren<Text>().enabled = false;

			btnBuy.GetComponent<Image>().enabled =true;
			btnBuy.GetComponentInChildren<Text>().enabled = true;
		}
	}
	
}
