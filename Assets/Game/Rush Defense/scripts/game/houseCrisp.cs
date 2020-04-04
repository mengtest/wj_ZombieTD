using UnityEngine;
using System.Collections;

public class houseCrisp : MonoBehaviour {

	// Use this for initialization
	GameObject p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12;
	void Start () {

		explode ();
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
		p6 = transform.Find ("p6").gameObject;
		p7 = transform.Find ("p7").gameObject;
		p8 = transform.Find ("p8").gameObject;
		p9 = transform.Find ("p9").gameObject;
		p10 = transform.Find ("p10").gameObject;




		p1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p3.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p4.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p5.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p6.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p7.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p8.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p9.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		p10.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


		int tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p1.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p2.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p3.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p4.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p5.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p6.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p7.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p8.GetComponent<Rigidbody2D>().AddForceAtPosition (new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p9.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
		tsign = Random.Range (-10, 10) > 0 ? 1 : -1;
		p10.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector3 (Random.Range(100,200)*tsign, Random.Range(200,400), 0), new Vector3 (0, 0, 0));
	




		Destroy (gameObject, 2);

	}
}
