using UnityEngine;
using System.Collections;

public class Thrower :EnemyBase {
	GameObject stone;
	protected override void initOther ()
	{
		base.initOther ();
		speed = -.01f;
		detectDis = 2;
		damage = 0;
		dieCoin = 80;
		dieRage = 40;
		dieScore = 300;
		attackSFX = "";
		stone = GameObject.Find("stone");
	}
	
	public override void attack ()
	{
		base.attack ();
//		if (cEnemy != null) {
			GameObject newStone = GameObject.Instantiate (stone, transform.position, Quaternion.identity) as GameObject;
			newStone.transform.Translate(0,.3f,0);
			newStone.GetComponent<Rigidbody2D>().isKinematic = false;
			float throwDis = Mathf.Abs(enemyDis*100);
			if(throwDis < 100)throwDis = 100;
			if(throwDis > 300)throwDis = 300;
			newStone.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (-100,throwDis, 0),Vector3.left/10);
//			Destroy (newbomb, 2);
//		}
	}
	
}
