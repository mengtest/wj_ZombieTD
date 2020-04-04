using UnityEngine;
using System.Collections;

public class Faster : EnemyBase {
	protected override void initOther ()
	{
		base.initOther ();
		speed = -.01f;
		hp = 150;
		dieCoin = 20;
		dieRage = 15;
		dieScore = 150;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
