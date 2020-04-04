using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	// Use this for initialization
	Vector3 startPos;
	void Start () {
		startPos = transform.position;
	}
	bool canMove = false;
	// Update is called once per frame
	void FixedUpdate () {
		if (!canMove)
						return;
		transform.Translate (-.02f, 0, 0);
		if (transform.position.x < -5) {
			canMove = false;
			GameManager.getInstance().stopMusic("wind");
		}
	}

	void startMove(){
		if (transform.position.x < startPos.x-1f && transform.position.x > -5f)
						return;
		transform.position = startPos;
		canMove = true;
		GameManager.getInstance().playSfx("wind");
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Player") {
			collider.SendMessage("hurt",120,SendMessageOptions.DontRequireReceiver);		
		}
	}
}
