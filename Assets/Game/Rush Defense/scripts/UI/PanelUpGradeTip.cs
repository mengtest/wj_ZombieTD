using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelUpGradeTip : MonoBehaviour {
	
	public Text myTxt;
	// Use this for initialization
	void Start () {
//		if (!GameManager.getInstance ().isEn) {
//			#if !UNITY_WP8			
//			myTxt.text = "你可以重复胜利先前关卡来过获得升级晶能,或者在选关菜单获得免费晶能";
//			#else
//			myTxt.text = "你可以过关新的关卡、或反复多次胜利先前关卡来过获得升级晶能。";
//			#endif
//		} else {
//			#if !UNITY_WP8
//			myTxt.text = "You can get crystal to upgrade by win any level again.Or get free crystal on level menu";	
//			#else
//			myTxt.text = "You can get crystal to upgrade by win any level again.";	
//			#endif
//		}
		myTxt.text =  Localization.Instance.GetString ("purchaseTip");
		
		int isShowed = PlayerPrefs.GetInt ("upgradeTipShown", 0);
		if (isShowed == 1) {
			gameObject.SetActive (false);		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Close(){
		gameObject.SetActive (false);
		PlayerPrefs.SetInt ("upgradeTipShown", 1);
	}
}
