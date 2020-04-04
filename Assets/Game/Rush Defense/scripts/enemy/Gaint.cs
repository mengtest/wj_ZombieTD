using UnityEngine;
using System.Collections;

public class Gaint  :EnemyBase {

	GameObject explode;
	protected override void initOther ()
	{
		base.initOther ();
		damage = 0;
		hp = 2000;
		speed = -0.004f;

		dieCoin = 300;
		dieRage = 200;
		dieScore = 1000;
		attackSFX = "";
		startSFX = "bigboss";
		explode = GameObject.Find("groundattack");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void attack ()
	{
		base.attack ();
		GameObject newExplode = Instantiate(explode,transform.position,Quaternion.identity) as GameObject;
		RaycastHit2D[] hit2ds = Physics2D.CircleCastAll(transform.position,.5f,Vector2.up);
		
		
		foreach(RaycastHit2D hit2d in hit2ds){
			GameObject thit2d = hit2d.collider.gameObject;
			if(thit2d.tag == "Player"){
				thit2d.gameObject.SendMessage("hurt",80,SendMessageOptions.DontRequireReceiver);
				GameManager.getInstance().playSfx("za");
			}
		}
		
		
		
		
		Destroy(newExplode,1.2f);
	}
}
