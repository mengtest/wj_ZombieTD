using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using DG.Tweening;
public class PanelFade : MonoBehaviour {

	// Use this for initialization
	Image image;
	public GameObject panelTutor;
	public GameObject panelExit;
	public GameObject panelTip;
	void Start () {
		init ();

//		fadeOut ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isFadeOut) {
			float ta = image.color.a;
			ta-= .02f;
			
			image.color = new Color(0,0,0,ta);	
			if(image.color.a <= 0){
				image.color = new Color(0,0,0,0);
				isFadeOut = false;
				image.enabled = false;
//				if(GameData.getInstance().cLevel == 0){
//					if(panelTutor!=null)panelTutor.SetActive(true);
//				}
			
//				if(panelTip!=null){
//					panelTip.SetActive(true);
//					panelTip.SendMessage("showLevel");
//				}
			}
		}
		if (isFadeIn) {

			float ta = image.color.a;
			ta+= .02f;

			image.color = new Color(0,0,0,ta);				
			if(image.color.a >= 1f){
				image.color = Color.black;
				isFadeIn =false;
				GameData.getInstance().init();
				if (returnType == -1) {//game with title
					SceneManager.LoadSceneAsync ("Game");
				} else if (returnType == 1) {//mainmenu
					SceneManager.LoadSceneAsync  ("MainMenu");
				} else if (returnType == 2) {//level
					SceneManager.LoadSceneAsync  ("LevelMenu");
				} else if (returnType == 3) {//game without title
					SceneManager.LoadScene  ("Game");

				}
			}
		}
	}

	bool isFadeIn,isFadeOut;

	void fadeIn(){
	
		image.enabled = true;
		isFadeIn = true;
		isFadeOut = false;
		image.color = new Color (0, 0, 0, 0);
	}
	void fadeOut(){


		image.enabled = true;
		isFadeOut = true;
		isFadeIn = false;
		image.color = new Color (0, 0, 0, 1);

	}

	void init(){
		image = GetComponent<Image> ();
		isFadeIn = false;isFadeOut = false;
		image.enabled = false;
	}
	public static int returnType = -100;
	public void returnTitle(int cType){
		GameManager.getInstance ().stopAllSFX ();
		returnType = cType;
		fadeIn ();

	}

	void exit(){
		panelExit.SetActive (true);
	}

}
