using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;
using System.Collections.Generic;
public class Basement : MonoBehaviour {
	
	// Use this for initialization
	Image houseHp;
	GameObject houseHpContainer;
	int originHP = 5000;
	int hp;
	SpriteRenderer sp,sp2;
	void Start () {
		hp = originHP;
		houseHpContainer = GameObject.Find("househp");
		houseHp = GameObject.Find ("househppbr").GetComponent<Image> ();
		sp = GetComponent<SpriteRenderer> ();
		sp2 = transform.Find ("housecover").GetComponent<SpriteRenderer> ();
		Text houseHpTxt = houseHpContainer.transform.Find ("Text").GetComponent<Text> ();
//		if (GameManager.getInstance ().isEn) {
//			houseHpTxt.text = "Base";		
//		} else {
//			houseHpTxt.text = "基地血量";		
//		}
		houseHpTxt.text = Localization.Instance.GetString ("Base");
//				explodeme ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (canWhite > 0) {
			float tcolor = canWhite % 2 == 0 ? .5f:1f;
			sp.color = new Color(tcolor,tcolor,tcolor);
			sp2.color = sp.color;
			canWhite--;
		}
	}
	
	int canWhite = 0;
	void hurt(int damage){
		if (GameData.getInstance ().isLock)
			return;
		if (damage == 0)
			return;
		hp -= damage;
		houseHp.fillAmount = (float)hp / (float)originHP;
		canWhite = 10;
		if (hp <= 1) {
			GetComponent<Rigidbody2D>().isKinematic = false;
			explodeme();
			GameObject.Find("GameMain").SendMessage("gameFailed");
		}
	}
//	List<SpriteSlicer2DSliceInfo> m_SlicedSpriteInfo = new List<SpriteSlicer2DSliceInfo>();
	void explodeme(){
		Time.timeScale = 1;
//		transform.Translate (1, 0, 0);
//		GetComponent<Rigidbody2D>().isKinematic = false;
//		SpriteSlicer2D.ExplodeSprite (gameObject, 20, 80.0f, true, ref m_SlicedSpriteInfo);
		Destroy(gameObject);
		GameManager.getInstance().playSfx("explosion");
//		
		houseHpContainer.SetActive (false);
		GameObject hc = GameObject.Find("housedestroy");
		GameObject.Instantiate (hc,gameObject.transform);
		hc.transform.position = gameObject.transform.position;
		hc.transform.parent = null;
		hc.GetComponent<houseCrisp> ().enabled = true;

	}
}
