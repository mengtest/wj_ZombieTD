using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

	// Use this for initialization
	float speed = .004f;
	SpriteRenderer sp;
	Animator fireanim;
	void Start () {
		fireanim = transform.Find ("tanksmoke").GetComponent<Animator> ();
		sp = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	bool canMove =false;
	void FixedUpdate () {
		if (!canMove)
						return;
		transform.Translate (speed, 0, 0);
		if (transform.position.y > -1.5f) {
			if(transform.position.x > -2f){
				transform.Translate (0, -.001f, 0);
			}
		}
		if (transform.position.x > endPos) {
			transform.Translate(new Vector3(-10,0,0));
			canMove =false;
			GameObject.Find("enemyhppbr").SendMessage ("hurt", "tank");
			GameManager.getInstance().stopMusic("tank");
		}

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Clicked");
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo)
			{
//				Debug.Log( hitInfo.transform.gameObject.name );
				// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				if (hitInfo.transform.gameObject.name == "tank") {
							Vector3 shootplace = transform.position + new Vector3 (Random.Range (2, 4), -.1f, 0);
							GameObject tfire = GameObject.Find ("tankfire");
							GameObject tnewfire = GameObject.Instantiate (tfire,shootplace,Quaternion.identity) as GameObject;
							Destroy (tnewfire, 2);
							fireanim.SetTrigger("fire");
							GameManager.getInstance().playSfx("explosion");
				}
			}
		}

	}
	float endPos = 5;
	void startMove(){
		Vector3 startPos = GameObject.Find("basement").transform.position;
		transform.position = new Vector3(startPos.x,transform.position.y,transform.position.z);
		GameObject enemyBase = GameObject.Find ("enemy");
		endPos =enemyBase.transform.position.x+1f;

		canMove = true;
		GameManager.getInstance().playSfx("tank");
	}





	void OnTriggerEnter2D(Collider2D collider){
		if (GameData.getInstance ().isLock)
						return;
		if (collider.tag == "enemy") {
			collider.SendMessage("hurt",Random.Range(100,300));	
		
		}
	}
}
