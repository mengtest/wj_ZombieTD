using UnityEngine;
using System.Collections;

public class stone_W : MonoBehaviour {

	// Use this for initialization
	GameObject explode;
	void Start () {
		explode = GameObject.Find("dust");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.name == "ground") {

			GameObject newExplode = Instantiate(explode,transform.position,Quaternion.identity) as GameObject;

//			RaycastHit2D[] hit2ds = Physics2D.CircleCastAll(transform.position,.5f,Vector2.up);
			Collider2D[] hit2ds = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - .3f,transform.position.y+.3f),new Vector2(transform.position.x+.3f,transform.position.y-.3f));


			foreach(Collider2D hit2d in hit2ds){

				GameObject thit2d = hit2d.gameObject;

				if(thit2d.tag == "Player"){
					if(thit2d.name == "houseDetect"){
						thit2d.transform.parent.gameObject.SendMessage("hurt",50,SendMessageOptions.DontRequireReceiver);
					}else{
						thit2d.gameObject.SendMessage("hurt",50,SendMessageOptions.DontRequireReceiver);
					}

				}
			}

		
			GameManager.getInstance().playSfx("za");

			Destroy(newExplode,1.2f);
			Destroy(gameObject);
		}
	}
}
