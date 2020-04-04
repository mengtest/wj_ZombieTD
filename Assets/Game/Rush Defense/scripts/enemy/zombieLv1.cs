using UnityEngine;
using System.Collections;

public class zombieLv1 :EnemyBase {

	protected override void initOther ()
	{
		base.initOther ();
		speed = -.005f;
		hp = 60;
		dieCoin = 10;
	}

	

}
