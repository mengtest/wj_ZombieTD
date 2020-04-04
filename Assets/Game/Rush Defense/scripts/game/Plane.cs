using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {
	
	// Use this for initialization
	Vector3 startPos;
	bool canmove = false;
	GameObject airbomb;
	void Start () {
		startPos = transform.position;
		airbomb = GameObject.Find("airbomb");
//		canmove = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	float speed = .1f;
	int gap = 25;
	int nTime = 0;
	void FixedUpdate(){
		if (!canmove)
			return;
		gap--;
		if (gap <= 0) {
			if (transform.position.x + speed < startPos.x || transform.position.x > 4) {
				speed*=-1;	
				GameManager.getInstance().playSfx("plane");
				nTime++;
				if (nTime >= 3) {

					nTime = 0;
					canmove =false;
					transform.position = startPos;
				}
			}

			GameObject new_airbomb = Instantiate (airbomb, transform.position, Quaternion.identity) as GameObject;
			new_airbomb.GetComponent<Rigidbody2D>().isKinematic = false;
			new_airbomb.GetComponent<Rigidbody2D>().AddForce (Vector3.down);
			new_airbomb.transform.localScale = new Vector3(Mathf.Sign(speed),1,1);
			gap = 25;		
		}
		transform.Translate (speed, 0, 0);

	}

	void startMove(){
		canmove = true;
		GameManager.getInstance().playSfx("plane");
	}
	
}
