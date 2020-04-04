using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
public class PanelWatchVideo : MonoBehaviour {
	
	// Use this for initialization
	public Text title,notCachedTitle;
	public Text btnYes,btnNo,btnOK;
	void Start () {
//		if (GameManager.getInstance ().isEn) {
//			title.text = "Would you  like to watch a video\nand get 2 crystals?";
//			btnYes.text = "Yes";
//			btnNo.text = "No";
//			btnOK.text = "OK";
//			notCachedTitle.text = "Oops,Video not ready yet.\nTry again later.";
//		} else {
//			title.text = "现在观看一段精彩视屏\n可以免费获得2升级水晶";
//			btnYes.text = "好的";
//			btnNo.text = "不用";	
//			btnOK.text = "知道了";
//			notCachedTitle.text = "哦，现在视屏还么有准备好。\n过一会再试试看吧。";
//		}

		title.text = Localization.Instance.GetString("videoRewardTip");
		btnYes.text = Localization.Instance.GetString("btnYes");
		btnNo.text = Localization.Instance.GetString("btnNo");
		btnOK.text = Localization.Instance.GetString("btnOK");
		notCachedTitle.text = Localization.Instance.GetString("adsNotReady");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject notCached;
	public void OnClick(GameObject g){
		switch (g.name) {
		case "btnYes":
			if(GameManager.getInstance().isCachedVideo){
				Chartboost.showRewardedVideo(CBLocation.Default);
			}else{
				Chartboost.cacheRewardedVideo(CBLocation.Default);
				notCached.SetActive(true);
			}
			break;
		case "btnNo":
			gameObject.SetActive(false);
			break;
		case "btnOK":
			notCached.SetActive(false);
			gameObject.SetActive(false);
			break;
		}
	}
}
