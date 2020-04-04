using UnityEngine;
using System.Collections;
//using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
public class HeroBase : MonoBehaviour {
	
	// Use this for initialization
	protected float speed = .005f;
	protected float detectDis = .2f;
	protected float detectDisMin = 0;//some wont attack too close enemy
	protected float damage = 20;
	protected int dieScore = 200;
	protected Animator anim;

	protected string attackSFX = "";
	protected string startSFX = "";
	protected float hp = 100;
	
	Animator bodyAnim;
	Animator legAnim;
	GameObject leg,body;
	GameObject shadow;

	GameObject PanelTopBar;
	Vector3 enemyStartPos;//record the enemy start postion.

	GameObject enemyHPbar;
	void Start () {
		PanelTopBar = GameObject.Find ("PanelTopBar");
		enemyStartPos = GameObject.Find ("enemy").transform.position;
		enemyHPbar = GameObject.Find ("enemyhppbr");
		//		startMove ();
		leg = transform.Find("leg").gameObject;
		body = transform.Find("body").gameObject;
		
		leg.GetComponent<SpriteRenderer>().sortingOrder = 1000000 - (int)(transform.position.y*100);
		body.GetComponent<SpriteRenderer>().sortingOrder = 1000000 - (int)(transform.position.y*100)+1;
		
		bodyAnim = body.GetComponent<Animator> ();
		legAnim = leg.GetComponent<Animator> ();

		shadow = transform.Find ("shadow").gameObject;
		shadow.GetComponent<SpriteRenderer> ().sortingOrder = 0;




		initOther ();


		//this switch initial the power up of heroes if they are upgraded
		switch (name) {
		case "new_blademan":
			hp += hp*.1f*GameData.getInstance().hero0State[0];	
			damage += damage*.1f*GameData.getInstance().hero0State[1];	
			speed += speed*.1f*GameData.getInstance().hero0State[2];


			bodyAnim.speed += speed*.1f*GameData.getInstance().hero0State[2];
			legAnim.speed = bodyAnim.speed;
		
			break;
		case "new_pistolman":
			hp += hp*.1f*GameData.getInstance().hero1State[0];	
			damage += damage*.1f*GameData.getInstance().hero1State[1];	
			speed += speed*.1f*GameData.getInstance().hero1State[2];
			
			
			bodyAnim.speed += speed*.1f*GameData.getInstance().hero1State[2];
			legAnim.speed = bodyAnim.speed;
			break;
		case "new_bombman":
			hp += hp*.1f*GameData.getInstance().hero2State[0];	
			damage += damage*.1f*GameData.getInstance().hero2State[1];	
			speed += speed*.1f*GameData.getInstance().hero2State[2];
			
			
			bodyAnim.speed += speed*.1f*GameData.getInstance().hero2State[2];
			legAnim.speed = bodyAnim.speed;
			break;
		case "new_gunman":
			hp += hp*.1f*GameData.getInstance().hero3State[0];	
			damage += damage*.1f*GameData.getInstance().hero3State[1];	
			speed += speed*.1f*GameData.getInstance().hero3State[2];
			
			
			bodyAnim.speed += speed*.1f*GameData.getInstance().hero3State[2];
			legAnim.speed = bodyAnim.speed;
			break;
		case "new_sawman":
			hp += hp*.1f*GameData.getInstance().hero4State[0];	
			damage += damage*.1f*GameData.getInstance().hero4State[1];	
			speed += speed*.1f*GameData.getInstance().hero4State[2];
			
			
			bodyAnim.speed += speed*.1f*GameData.getInstance().hero4State[2];
			legAnim.speed = bodyAnim.speed;
			break;
			
		}
	}
	
	virtual protected void initOther(){
		
	}
	
	
	
	
	public void idle(){
		legAnim.SetInteger ("State", 0);
	}
	
	public void walk(){
		legAnim.SetInteger ("State", 1);
	}
	
	// Update is called once per frame
	bool canmove = false;
	int n = 0;
	/// <summary>
	/// Enemys keeps moving until they find an object to attack
	/// </summary>
	void FixedUpdate () {
		if (canmove) {
			transform.Translate (new Vector3 (speed, 0, 0));
		}

		otherUpdate ();
	}
	
	protected virtual void otherUpdate(){
		
	}
	public void startMove(){
		if (isDead)
			return;
	
		anim = GetComponent<Animator> ();

		StartCoroutine ("playStartSFX");

		
		
		
		canmove = true;
		StartCoroutine("detect");
	}

	IEnumerator playStartSFX(){
		yield return new WaitForSeconds (.1f);
		GameManager.getInstance ().playSfx (startSFX);
	}

	protected GameObject cEnemy = null;
	protected float enemyDis = 0;
	/// <summary>
	/// Enemy/base detected.
	/// </summary>
	IEnumerator detect(){
		
		while (true) {
			
			yield return new WaitForSeconds(.2f);
			if(transform.position.x > enemyStartPos.x){
				Destroy(gameObject);
				if (GameData.getInstance ().nPeople - 1 >= 0) {
					GameData.getInstance().nPeople--;		
					PanelTopBar.SendMessage("refreshView");
					GameData.getInstance ().heroDied++;
					if(!GameData.getInstance().isLock)
					enemyHPbar.SendMessage("hurt",name);
				}
				GameData.getInstance().unitScore+=dieScore;
			}
//			RaycastHit2D[] ray2ds = Physics2D.RaycastAll(transform.position,new Vector2(1*Mathf.Sign(speed),.2f),detectDis);
			Collider2D[] ray2ds = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + detectDis*Mathf.Sign(speed),transform.position.y+.1f),new Vector2(transform.position.x,transform.position.y-.1f));
			bool isDetected = false;
			List<GameObject> newColliders = new List<GameObject> ();
			foreach (Collider2D tcollider in ray2ds) {
				newColliders.Add (tcollider.gameObject);
			}
			newColliders.Sort (CompareCondition);
			for(int i = 0;i<newColliders.Count;i++){
				GameObject ray2d = newColliders[i].gameObject;
				if(ray2d!=null){
					//				print (ray2d.collider.gameObject.name);
					if(ray2d.tag == "enemy"){
						GameObject tcEnemy = ray2d.gameObject;
						enemyDis = tcEnemy.transform.position.x - transform.position.x;
						
//						if(enemyDis > detectDisMin){
							isDetected = true;
							cEnemy = tcEnemy;
							break;
//						}
						

					}
				}
				
			}
			
			if(cEnemy != null){
				canmove = false;
				bodyAnim.SetTrigger("attack");
				legAnim.SetInteger("state",0);
				
			}else{
				canmove = true;
				bodyAnim.SetTrigger("walk");
				legAnim.SetInteger("state",1);
				
			}
			if(!isDetected){
				cEnemy = null;
				enemyDis = 0;
			}
			
		}
	}
	
	public void hurt(int cHp){
		if (isDead)
			return;
	
		
		hp -= cHp;
		if (hp <= 0) {
			dieAnim();
			StopCoroutine ("detect");
			Destroy(gameObject);		
		}
		
	}
	
	
	
	virtual public void attack(){
		if(cEnemy!=null){
			cEnemy.SendMessage("hurt",damage);
			GameManager.getInstance().playSfx(attackSFX);
		}
	}
	
	bool isDead = false;
	void dieAnim(){
		isDead = true;
		if (GameData.getInstance ().nPeople - 1 >= 0) {
			GameData.getInstance().nPeople--;		
			PanelTopBar.SendMessage("refreshView");
		}

		GameData.getInstance().unitScore+=dieScore;
		GameData.getInstance ().heroDied++;
		GameObject hpiece = GameObject.Find("heropieces");
		GameObject tnewPiece = Instantiate(hpiece,transform.position,Quaternion.identity) as GameObject;
		tnewPiece.SendMessage("explode");
		
		int tbloodno = (int)Random.Range (1, 3);
		GameObject tblood = GameObject.Find("blood"+tbloodno);
		GameObject tnewBlood = Instantiate (tblood, transform.position+new Vector3(0,-.1f,0), Quaternion.identity)  as GameObject;
		Destroy (tnewBlood, 5);
		tnewBlood.GetComponent<SpriteRenderer> ().sortingOrder = 0;//100000 - (int)(transform.position.y*100);
	}

	/// <summary>
	/// This function is a parameter for sort,If found more than 1 enemy,attack the cloest
	/// </summary>
	/// <returns>The condition.</returns>
	/// <param name="enemy1">Enemy1.</param>
	/// <param name="enemy2">Enemy2.</param>
	protected int  CompareCondition(GameObject enemy1,  GameObject enemy2) {
		float dis1 = Vector3.Distance (transform.position, enemy1.transform.position);
		float dis2 = Vector3.Distance (transform.position, enemy2.transform.position);
		if (dis1 > dis2) {
			return 1;
		} else {
			return -1;				
		}
		
	}
}
