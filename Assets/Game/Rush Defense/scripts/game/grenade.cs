using UnityEngine;
using System.Collections;

public class grenade : MonoBehaviour {

	// Use this for initialization
	GameObject explode;
	float damage  = 50;
	void Start () {
		explode = GameObject.Find("explode");
		damage += damage*.1f*GameData.getInstance().hero1State[1];	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.name == "ground") {
			GameObject newExplode = Instantiate(explode,transform.position,Quaternion.identity) as GameObject;
//			RaycastHit2D[] hit2ds = Physics2D.CircleCastAll(transform.position,.5f,new Vector2(0,0));
			Collider2D[] hit2ds = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - .3f,transform.position.y+.3f),new Vector2(transform.position.x+.3f,transform.position.y-.3f));


			foreach(Collider2D hit2d in hit2ds){
				GameObject thit2d = hit2d.gameObject;
				if(thit2d.tag == "enemy"){
					thit2d.gameObject.SendMessage("hurt",damage);

				}
			}
			GameManager.getInstance().playSfx("explosion");
		


			Destroy(newExplode,1.2f);
			Destroy(gameObject);
		}
	}
}
