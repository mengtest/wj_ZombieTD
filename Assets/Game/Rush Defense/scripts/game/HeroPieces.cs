using UnityEngine;
using System.Collections;

public class HeroPieces : MonoBehaviour {

	// Use this for initialization
	GameObject p1,p2,p3,p4,p5;
	void Start () {

//		explode ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void explode(){
		p1 = transform.Find ("p1").gameObject;
		p2 = transform.Find ("p2").gameObject;
		p3 = transform.Find ("p3").gameObject;
		p4 = transform.Find ("p4").gameObject;
		p5 = transform.Find ("p5").gameObject;

		p1.GetComponent<Rigidbody2D>().isKinematic = false;
		p2.GetComponent<Rigidbody2D>().isKinematic = false;
		p3.GetComponent<Rigidbody2D>().isKinematic = false;
		p4.GetComponent<Rigidbody2D>().isKinematic = false;
		p5.GetComponent<Rigidbody2D>().isKinematic = false;

		p1.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (0, 200, 0), new Vector3 (0, 0, 0));
		p2.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (-20, 220, 0), new Vector3 (0, 0, 0));
		p3.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (20, 300, 0), new Vector3 (0, 0, 0));
		p4.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (20, 250, 0), new Vector3 (0, 0, 0));
		p5.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (-20, 300, 0), new Vector3 (0, 0, 0));


		Destroy (gameObject, 2);

	}
}
