using UnityEngine;
using System.Collections;

public class TankFire : MonoBehaviour {

	// Use this for initialization

	float damage  = 500;
	void Start () {
		RaycastHit2D[] hit2ds = Physics2D.CircleCastAll(transform.position,.5f,Vector2.up);
		
		
		foreach(RaycastHit2D hit2d in hit2ds){
			GameObject thit2d = hit2d.collider.gameObject;
			if(thit2d.tag == "enemy"){
				thit2d.gameObject.SendMessage("hurt",damage);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
