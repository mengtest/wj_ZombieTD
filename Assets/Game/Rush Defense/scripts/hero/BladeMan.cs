using UnityEngine;
using System.Collections;

public class BladeMan : HeroBase {

	protected override void initOther ()
	{
		base.initOther ();
		attackSFX = "blade";
	}
}
