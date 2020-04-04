using UnityEngine;
using System.Collections;

public class Ghoster : EnemyBase {

	GameObject dust;
	protected override void initOther ()
	{
		base.initOther ();
	
		damage = 50;
		hp = 200;
		speed = -.005f;


		dieCoin = 100;
		dieRage = 50;
		dieScore = 500;


	}
	
	IEnumerator unvisible(){
		
		yield return new WaitForSeconds(1f);		
		sp.enabled = false;
		shadowsp.enabled = false;
		canAttack = false;
		cEnemy = null;
		GameObject tdis = Instantiate (dust, transform.position, Quaternion.identity) as GameObject;
		tdis.GetComponentInChildren<SpriteRenderer> ().sortingOrder = sp.sortingOrder;
		Destroy (tdis,1);
		StartCoroutine("revisible");
		tag = "Untagged";
		speed = -.02f;

		
		
	}
	
	IEnumerator revisible(){
		yield return new WaitForSeconds (Random.Range(3,5));
//		StartCoroutine("unvisible");
		sp.enabled = true;
		shadowsp.enabled = true;
		canAttack = true;
		tag = "enemy";
		speed = -.005f;
	}


	int cn = 0;
	protected override void otherUpdate ()
	{	
		base.otherUpdate ();
		
		if (transform.position.x >= enemyBase.transform.position.x-1) {
			if(transform.position.x + speed <= enemyBase.transform.position.x-1){
				StartCoroutine("unvisible");
				dust = GameObject.Find ("disappear");
		
			}
		}
		
	}
}
