using UnityEngine;
using System.Collections;

public class Exploder : EnemyBase {
	
	protected override void initOther ()
	{
		base.initOther ();
		damage = 0;
		hp = 1200;
		speed = -.02f;
		dieCoin = 80;
		dieRage = 40;
		dieScore = 300;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public override void attack ()
	{
		base.attack ();

		//			canAttack = false;
		shadowsp.enabled = false;
		Destroy (gameObject,2);

		isOver = true;
		
		Collider2D[] ray2ds;
		
		
		//ray2ds = Physics2D.CircleCastAll(transform.position,detectDis,new Vector2(0,1));	
		ray2ds = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - .2f,transform.position.y+.1f),new Vector2(transform.position.x+.2f,transform.position.y-.1f));	

		GameManager.getInstance().playSfx("explosion");
		foreach(Collider2D ray2d in ray2ds){
			if(ray2d!=null){
				
				if(ray2d.gameObject.tag == "Player"){
					
					ray2d.gameObject.SendMessage("hurt",200,SendMessageOptions.DontRequireReceiver);


				}
			}
			
		}
	}

}
