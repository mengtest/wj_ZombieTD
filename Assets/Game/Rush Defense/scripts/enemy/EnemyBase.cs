using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;
public class EnemyBase : MonoBehaviour {
	
	// Use this for initialization
	protected float speed = -.01f;
	protected float detectDis = .2f;
	protected float damage = 20;
	protected int dieCoin = 10;
	protected int dieRage = 10;
	protected int dieScore = 100;
	
	protected float hp = 80;
	protected float enemyDis = 0;
	protected float detectDisMin = 0;//some wont attack too close enemy
	protected Animator anim;
	
	protected bool canAttack = true;
	protected SpriteRenderer sp;
	
	protected string attackSFX = "zombieattack";
	protected string startSFX = "";
	GameObject shadow;
	protected GameObject panelTopBar;
	protected SpriteRenderer shadowsp;
	protected GameObject enemyBase;
	void Start () {
		panelTopBar = GameObject.Find ("PanelTopBar");
		sp = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		GetComponent<SpriteRenderer> ().sortingOrder = 1000000 - (int)(transform.position.y*100);
		
		shadow = transform.Find ("shadow").gameObject;
		shadow.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		shadowsp = shadow.GetComponent<SpriteRenderer> ();
		initOther ();
		anim.speed *= Random.Range (1f, 1.1f);
		enemyBase = GameObject.Find("enemy");
		if (GameData.getInstance().cLevel > 1) {
			hp = hp * (1.5f);
		}
		
	}
	
	virtual protected void initOther(){
		
	}
	
	// Update is called once per frame
	bool canmove = false;
	int n = 0;
	protected bool isOver = false;
	void FixedUpdate () {
		if (canmove) {
			if (isOver)
				return;
			transform.Translate (new Vector3 (speed, 0, 0));
		}
		
		
		otherUpdate ();
	}
	
	protected virtual void otherUpdate(){
		
	}
	
	
	public void startMove(){
		canmove = true;
		StartCoroutine("detect");
		
		StartCoroutine ("playStartSFX");
	}
	
	
	IEnumerator playStartSFX(){
		yield return new WaitForSeconds (.1f);
		GameManager.getInstance ().playSfx (startSFX);
	}
	
	protected GameObject cEnemy = null;
	protected bool isDetectBack = false;
	IEnumerator detect(){
		while (true) {
			yield return new WaitForSeconds(.3f);
			if(transform.position.x < -10)Destroy(gameObject);
			if(canAttack){
				Collider2D[] ray2ds = null;
				
				ray2ds = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - detectDis,transform.position.y+.1f),new Vector2(transform.position.x,transform.position.y-.1f));	
				
				
				List<GameObject> newColliders = new List<GameObject> ();
				foreach (Collider2D tcollider in ray2ds) {
					newColliders.Add (tcollider.gameObject);
				}
				newColliders.Sort (CompareCondition);
				
				
				bool isDetected = false;
				for(int i = 0;i<newColliders.Count;i++){
					GameObject ray2d = newColliders[i].gameObject;
					if(ray2d!=null){
						
						//				print (ray2d.collider.gameObject.name);
						if(ray2d.tag == "Player"){
							
							GameObject tcEnemy = ray2d.gameObject;
							enemyDis = transform.position.x - tcEnemy.transform.position.x ;
							
							
							//							transform.localScale = new Vector3(Mathf.Sign(enemyDis),1,1);
							
							if(Mathf.Abs(enemyDis) > detectDisMin){
								isDetected = true;
								cEnemy = tcEnemy;
								break;
							}
							
						}
					}
					
				}
				if(!isOver){
					if(cEnemy != null){
						canmove = false;
						anim.SetTrigger("attack");
					}else{
						canmove = true;
						anim.SetTrigger("run");
						
					}
					if(!isDetected){
						cEnemy = null;
					}
				}
			}
		}
	}
	
	public void hurt(int cHp){
		if (isDead)
			return;
		if (!canAttack)
			return;
		hp -= cHp;
		if (hp <= 0) {
			die();
			dieAnim();
			StopCoroutine ("detect");
			Destroy(gameObject);		
		}
	}
	bool isDead = false;
	void dieAnim(){
		isDead = true;
		GameManager.getInstance().playSfx("diesound");
		GameObject hpiece = GameObject.Find("zombiepieces");
		GameObject tnewPiece = Instantiate(hpiece,transform.position,Quaternion.identity) as GameObject;
		tnewPiece.SendMessage("explode");
		
		int tbloodno = (int)Random.Range (3, 5);
		GameObject tblood = GameObject.Find("blood"+tbloodno);
		GameObject tnewBlood = Instantiate (tblood, transform.position+new Vector3(0,-.1f,0), Quaternion.identity)  as GameObject;
		Destroy (tnewBlood, 5);
		tnewBlood.GetComponent<SpriteRenderer> ().sortingOrder = 0;//100000 - (int)(transform.position.y*100);
	}
	
	virtual public void attack(){
		if(cEnemy!=null){
			cEnemy.SendMessage("hurt",damage,SendMessageOptions.DontRequireReceiver);
			GameManager.getInstance().playSfx(attackSFX);
		}
	}
	virtual public void die(){
		
		
		GameData.getInstance ().coin += dieCoin;
		GameData.getInstance ().rage += dieRage;
		GameData.getInstance ().enemyKilled++;
		GameData.getInstance ().unitScore += dieScore;
		if(panelTopBar!=null)panelTopBar.SendMessage("refreshView");
		
	}
	
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
