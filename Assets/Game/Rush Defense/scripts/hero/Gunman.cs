using UnityEngine;
using System.Collections;

public class Gunman : HeroBase {

	protected override void initOther ()
	{
		base.initOther ();
		speed = .005f;
		detectDis = 3f;
		damage = 30;
		hp = 150;
		dieScore = 800;
		attackSFX = "gun";
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
