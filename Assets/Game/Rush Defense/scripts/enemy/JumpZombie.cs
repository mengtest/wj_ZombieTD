using UnityEngine;
using System.Collections;

public class JumpZombie : EnemyBase {


	
	// Update is called once per frame

	void Update () {

	}

	protected override void initOther ()
	{
		base.initOther ();
		speed = -.03f;
		detectDis = .2f;
		hp = 100;
		dieCoin = 50;
		dieRage = 30;
		dieScore = 200;
//		damage = 100;
	}

	bool isJump = false;

	float jumpYspd = .12f;
	float jumpStartY = 0;
	protected override void otherUpdate ()
	{
		base.otherUpdate ();
		if (cEnemy != null) {
			int trnd = (int)Random.Range(1,100);
			if(trnd > 10){
				if(!isJump){
				if(transform.position.x > -.3f)jump();
				}
			}
		}
		if (isJump) {
			transform.Translate(new Vector3(speed,jumpYspd,0));
			jumpYspd-=.01f;
			if(jumpYspd <-.1f){
				isJump = false;
				transform.position = new Vector3(transform.position.x,jumpStartY,0);
			}
		}
	}
	
	void jump(){
		cEnemy = null;
		isJump = true;
		jumpStartY = transform.position.y;
	}


}
