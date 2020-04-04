using UnityEngine;
using System.Collections;

public class HeroPistolMan : HeroBase {

	// Use this for initialization
	

	// Update is called once per frame
	void Update () {
	
	}

	protected override void initOther ()
	{
		base.initOther ();
		detectDis = 1.8f;
		damage = 20;
		hp = 120;
		dieScore = 400;
		attackSFX = "pistol";
	}


}
