using UnityEngine;
using System.Collections;

public class Bombman : HeroBase {
	GameObject bomb;
	protected override void initOther ()
	{
		base.initOther ();
		speed = .01f;
		detectDis = 3;
		detectDisMin = .5f;
		damage = 0;
		hp = 180;
		
		bomb = GameObject.Find("bomb");


	}
	
	public override void attack ()
	{
		base.attack ();
		if (cEnemy != null) {
			GameObject newbomb = GameObject.Instantiate (bomb, transform.position, Quaternion.identity) as GameObject;
			newbomb.transform.Translate(new Vector3(0,.4f,0));
			newbomb.GetComponent<Rigidbody2D>().isKinematic = false;
			float throwDis = Mathf.Abs(enemyDis*100);
		
			if(throwDis < 100)throwDis = 100;
			if(throwDis > 300)throwDis = 300;

			newbomb.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (100,throwDis, 0),Vector3.left/10);
//			Destroy (newbomb, 2);
		}
	}
	
}
