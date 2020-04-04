using UnityEngine;
using System.Collections;

public class attackDetecetor : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void attackTrigger(){
		if (name == "body") {
			if (transform.parent != null)
				transform.parent.SendMessage ("attack", SendMessageOptions.DontRequireReceiver);
		} else {
			transform.SendMessage ("attack", SendMessageOptions.DontRequireReceiver);	
		}
	}
}
