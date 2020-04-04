using UnityEngine;
using System.Collections;

public class Sawman : HeroBase {
	
	protected override void initOther ()
	{
		base.initOther ();
		speed = .005f;
		damage = 50;
		hp = 500;
		dieScore = 2000;
		startSFX = "bigmancome";
		attackSFX = "saw";
	}
	bool canDir = false;

	int cn = 0;
	protected override void otherUpdate ()
	{	
		base.otherUpdate ();

			if (transform.position.x + speed > 3 && cn% 2 == 0) {
				speed *= -1;	
			cn++;
			}
		if (transform.position.x + speed < -2 && cn % 2 == 1) {
			speed *= -1;	
			cn++;
		}
			transform.localScale = new Vector3(Mathf.Sign(speed),1,1);

	}
	
	
}
