using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;
public class EnemyHpBar : MonoBehaviour {
	
	// Use this for initialization
	Image enemyHPbar;
	int[] HPs = {230,1000,1500,2000,5000,8000};
	int HP;
	int OriginHP;
	public Text txtenemy;
	void Start () {
		OriginHP = HPs [GameData.getInstance().cLevel];
		HP = OriginHP;
		enemyHPbar = GameObject.Find ("enemyhppbr").GetComponent<Image> ();
//		if (GameManager.getInstance ().isEn) {
//			txtenemy.text = "Enemy";		
//		} else {
//			txtenemy.text = "敌军势力";	
//			
//		}

		txtenemy.text = Localization.Instance.GetString("enemy");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject panelGameOver;
	void hurt(string tname){
		switch(tname){
		case "new_blademan":
			HP -= 20;
			break;
		case "new_pistolman":
			HP -= 40;
			break;
		case "new_bombman":
			HP -= 60;
			break;
		case "new_gunman":
			HP -= 60;
			break;
		case "new_sawman":
			HP -= 200;
			break;
		case "tank":
			HP -= 300;
			break;
		}
		float tamout = (float)HP/(float)OriginHP;
		enemyHPbar.fillAmount = tamout;
		if (tamout <= 0) {
			GameData.getInstance().isWin = true;
			GameData.getInstance().isLock = true;
			GameObject[] enemys  = GameObject.FindGameObjectsWithTag("enemy");
			foreach(GameObject enemy in enemys){
				enemy.SendMessage("hurt",10000);
			}
			
			GameObject.Find("GameMain").SendMessage("gameWin");
			
			
		}
	}
}
