using UnityEngine;
using System.Collections;

public class airBomb : MonoBehaviour {
	
	// Use this for initialization
	GameObject explode;
	float damage  = 50;
	Vector3 basePos;
	void Start () {
		explode = GameObject.Find("tankfire");
		GameObject base1 = GameObject.Find ("basement");
		if (base1 != null) {
			basePos = base1.transform.position;
		} else {
			Destroy(gameObject);		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.name == "ground") {
			GameObject newExplode = Instantiate(explode,transform.position,Quaternion.identity) as GameObject;
			RaycastHit2D[] hit2ds = Physics2D.CircleCastAll(transform.position,.5f,Vector2.up);
			if (newExplode.transform.position.x < basePos.x+1) {
				newExplode.GetComponent<SpriteRenderer>().sortingOrder = 0;		
			}
			
			foreach(RaycastHit2D hit2d in hit2ds){
				GameObject thit2d = hit2d.collider.gameObject;
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
