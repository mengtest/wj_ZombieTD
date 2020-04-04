using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PanelTip : MonoBehaviour {
	
	// Use this for initialization
	Text mytext;
	Image image;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFadeOut) {
			float ta = image.color.a;
			ta -= .01f;
			image.color = new Color (1, 1, 1, ta);
			mytext.color = image.color;
			if(image.color.a <=0){
				isFadeOut = false;
				gameObject.SetActive(false);
			}
		}
	}
	bool isFadeOut = false;
	public void PeopleReachLimit(){
		//		if (isFadeOut)
		//						return;
		init ();
		mytext.text = "populationExceed";
//		if (GameManager.getInstance ().isEn) {
//			mytext.text = "Population Exceeded";
//		} else {
//			mytext.text = "人数已达上限";	
//		}
		image.color = Color.white;
		isFadeOut = true;
	}
	
	public void notUnlocked(int clv){
		//		if (isFadeOut)
		//			return;
		init ();
		mytext.text =  Localization.Instance.GetString ("willUnlock")+" "+clv;
		image.color = Color.white;
		isFadeOut = true;
	}

	public void showLevel(){
		init ();
//		print ("clevel"+GameData.getInstance().cLevel);
		mytext.text =  Localization.Instance.GetString ("levelname"+GameData.getInstance().cLevel);
		image.color = Color.white;
		isFadeOut = true;
	
	}
	
	void init(){
		mytext = GetComponentInChildren<Text> ();
		image = GetComponent<Image> ();
	}
}
