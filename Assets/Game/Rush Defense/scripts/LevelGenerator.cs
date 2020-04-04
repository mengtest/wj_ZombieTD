using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
		StartCoroutine("zombieGenerator");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	int genGap = 5;//every 5 seconds,we generate a new wave
	int[] maxLimits = {10,20,30,40,60,100,100,100,100,100,100,100,100};//the maxium enemy population of each level
	/// <summary>
	/// If the quantity of enemy not reaches the max limits,keep generating...
	/// </summary>
	void generate(){
		
		if (GameData.getInstance ().isLock)
			return;
		
		int nEnemy = GameObject.FindGameObjectsWithTag ("enemy").Length;
		
		int maxLimit = 20;
		if (GameData.getInstance ().cLevel != -1) {
			maxLimit = maxLimits [GameData.getInstance ().cLevel];
		}
		if (nEnemy >= maxLimit)
			return;
		
		
		switch (GameData.getInstance ().cLevel) {
		case 0:
			generateLevel0();
			break;
		case 1:
			
			generateLevel1();
			break;
		case 2:
			
			generateLevel2();
			break;
		case 3:
			generateLevel3();
			break;
		case 4:
			generateLevel4();
			break;
		case 5:
			generateLevel5();
			break;
		case 6:
			break;
			
		case -1:
			generateTest();
			break;
			
		}
		
	}
	int nGameTime = 0;
	IEnumerator zombieGenerator(){
		while (true) {
			yield return new WaitForSeconds (genGap);
			generate();
			nGameTime++;
		}
	}
	/// <summary>
	/// The most important function to classify different kind of enmeies.
	/// </summary>
	/// <returns>The zombie.</returns>
	/// <param name="zombieType">Zombie type.</param>
	GameObject createZombie(int zombieType){
		GameObject tzombie;
		GameObject tnewZombie = null;
		float toffsetY;
		switch (zombieType) {
		case 1:
			tzombie = GameObject.Find("zombie1");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 2:
			tzombie = GameObject.Find("faster");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 3:
			tzombie = GameObject.Find("wolf");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 4:
			tzombie = GameObject.Find("thrower");
			toffsetY = Random.Range(-.1f,.1f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 5:
			tzombie = GameObject.Find("ghost");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 6:
			tzombie = GameObject.Find("exploder");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		case 7:
			tzombie = GameObject.Find("gaint");
			toffsetY = Random.Range(-.05f,0.05f);
			tnewZombie = GameObject.Instantiate(tzombie,transform.position+new Vector3(0,toffsetY,0),Quaternion.identity) as GameObject;
			tnewZombie.SendMessage("startMove");
			break;
		}
		tnewZombie.tag = "enemy";
		return tnewZombie;
	}


	/// <summary>
	/// for test level only
	/// </summary>
	void generateTest(){
		
		int hugeGap = 1;
		int hugeNum = Random.Range (5, 7);
		if(nGameTime % hugeGap == 0){
			for(int i = 0;i<hugeNum;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(+.3f*i,0,0));
			}
		}
	}


	/// <summary>
	/// wave description for level 1
	/// </summary>
	void generateLevel0(){
		int zombiePerSec = 1;
		int hugeGap = Random.Range (10, 20);
		int hugeNum = Random.Range (1, 4);
		int specialGap = Random.Range (8, 12);
		int specialNum = Random.Range (1, 3);
		int[] speicalType1 = {2}; 
		
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			createZombie ((int)Random.Range (1, 2));
		}
		
		
		
		if(nGameTime % hugeGap == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(-.1f*i,0,0));
			}
		}
		
		// gen a special enemy
		if (nGameTime % specialGap == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum;i++){
				createZombie(speicalType1[(int)Random.Range(0,speicalType1.Length)]);
			}
		}
	}
	
	
	/// <summary>
	/// wave description for level 2
	/// </summary>
	void generateLevel1(){
		int zombiePerSec = Random.Range(1,3);//how much enemy would be created each time
		int hugeGap1 = Random.Range (8, 12);//after 8-12 seconds randomly,there would be a huge wave
		int hugeNum1 = Random.Range (8, 12);//how much would be created each huge wave.

		//after serval senconds randomly,create a special enemy(always strong in this level);
		int specialGap1 = Random.Range (4, 8);
		int specialGap2 = Random.Range (6, 15);
		int specialNum1 = Random.Range (1, 4);
		int specialNum2 = Random.Range (1, 3);
		int[] speicalType1 = {2,3}; //the special enemy type is No.2(faster zombie) or the No.3(wolf)
		int[] speicalType2 = {3}; 
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			createZombie ((int)Random.Range (1, 2));
		}
		
		
		//create a huge
		if(nGameTime % hugeGap1 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum1;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		
		// gen a special enemy
		if (nGameTime % specialGap1 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum1;i++){
				GameObject tZombie = createZombie(speicalType1[(int)Random.Range(0,speicalType1.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy 2
		if (nGameTime % specialGap2 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum2;i++){
				GameObject tZombie = createZombie(speicalType2[(int)Random.Range(0,speicalType2.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
	}
	/// <summary>
	/// wave description for level 3
	/// </summary>
	void generateLevel2(){
		
		int zombiePerSec = Random.Range(1,3);
		int hugeGap1 = Random.Range (5, 12);
		int hugeNum1 = Random.Range (5, 8);
		
		int hugeGap2 = Random.Range (15, 30);
		int hugeNum2 = Random.Range (15,20);
		
		//		int specialGap1 = Random.Range (4, 8);
		int specialGap2 = Random.Range (6, 8);
		//		int specialNum1 = Random.Range (1, 4);
		int specialNum2 = Random.Range (1, 2);
		//		int[] speicalType1 = {2,3}; 
		int[] speicalType2 = {4}; 
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			if(GameData.getInstance().timeelaspe > 40){
				createZombie ((int)Random.Range (1, 4));
			}else{
				createZombie ((int)Random.Range (1, 2));
			}
			
		}
		
		
		//create a huge
		if(nGameTime % hugeGap1 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum1;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// create a huge
		if(nGameTime % hugeGap2 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum2;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}

		
		// gen a special enemy 2
		if (nGameTime % specialGap2 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum2;i++){
				GameObject tZombie = createZombie(speicalType2[(int)Random.Range(0,speicalType2.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
	}
	
	/// <summary>
	/// wave description for level 4
	/// </summary>
	void generateLevel3(){
		int zombiePerSec = Random.Range(1,3);
		zombiePerSec += Mathf.FloorToInt (nGameTime / 20);
		int hugeGap1 = Random.Range (5, 12);
		
		//		hugeGap1 -= Mathf.FloorToInt (nGameTime / hugeGap1)*2;
		//		hugeGap1 = hugeGap1 >= 5 ? hugeGap1 : 5;
		
		
		int hugeNum1 = Random.Range (5, 8);
		
		
		
		int hugeGap2 = Random.Range (15, 30);
		
		
		
		int hugeNum2 = Random.Range (15,20);
		
		int specialGap1 = Random.Range (6, 8);
		int specialGap2 = 5;
		
		int specialGap3 = 20;
		specialGap3 -= Mathf.FloorToInt (nGameTime / specialGap3)*2;
		specialGap3 = specialGap3 > 5 ? specialGap3 : 5;
		int specialNum1 = Random.Range (1, 4);
		int specialNum2 = Random.Range (2, 4);
		int specialNum3 = Random.Range (1, 2);
		int[] speicalType1 = {3,4}; 
		int[] speicalType2 = {5}; 
		int[] speicalType3 = {7}; 
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			GameObject tzombie;
			if(GameData.getInstance().timeelaspe > 40){
				tzombie = createZombie ((int)Random.Range (1, 4));
			}else{
				tzombie = createZombie ((int)Random.Range (1, 2));
			}
			tzombie.transform.Translate(.1f,0,0);
		}
		
		
		//create a huge
		if(nGameTime % hugeGap1 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum1;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// create a huge
		if(nGameTime % hugeGap2 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum2;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy
		if (nGameTime % specialGap1 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum1;i++){
				GameObject tZombie = createZombie(speicalType1[(int)Random.Range(0,speicalType1.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy 2
		if (nGameTime % specialGap2 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum2;i++){
				GameObject tZombie = createZombie(speicalType2[(int)Random.Range(0,speicalType2.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy 3 gaint
		if (nGameTime % specialGap3 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum3;i++){
				GameObject tZombie = createZombie(speicalType3[(int)Random.Range(0,speicalType3.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
				
			}
		}
	}
	/// <summary>
	/// wave description for level 5
	/// </summary>
	void generateLevel4(){
		int zombiePerSec = Random.Range(2,4);
		zombiePerSec += Mathf.FloorToInt (nGameTime / 10);
		int hugeGap1 = Random.Range (5, 12);
		
	
		
		int hugeNum1 = Random.Range (6, 8);
		
		
		
		int hugeGap2 = Random.Range (15, 30);
		
		
		
		int hugeNum2 = Random.Range (15,20);
		
		int specialGap1 = Random.Range (6, 12);
		int specialGap2 = Random.Range (4, 10);
		
		int specialGap3 = 20;
		specialGap3 -= Mathf.FloorToInt (nGameTime / specialGap3)*2;
		specialGap3 = specialGap3 > 5 ? specialGap3 : 5;
		int specialNum1 = Random.Range (1, 4);
		int specialNum2 = Random.Range (1, 2);
		int specialNum3 = Random.Range (1, 3);
		int[] speicalType1 = {3,4}; 
		int[] speicalType2 = {6}; 
		int[] speicalType3 = {7}; 
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			if(GameData.getInstance().timeelaspe > 40){
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}else{
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
			
		}
		
		
		//create a huge
		if(nGameTime % hugeGap1 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum1;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// create a huge
		if(nGameTime % hugeGap2 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum2;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		// gen a special enemy 1
		if (nGameTime % specialGap1 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum1;i++){
				GameObject tZombie = createZombie(speicalType1[(int)Random.Range(0,speicalType1.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		
		// gen a special enemy 2
		if (nGameTime % specialGap2 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum2;i++){
				GameObject tZombie = createZombie(speicalType2[(int)Random.Range(0,speicalType2.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy 3 gaint
		if (nGameTime % specialGap3 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum3;i++){
				GameObject tZombie = createZombie(speicalType3[(int)Random.Range(0,speicalType3.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
				
			}
		}
	}
	
	/// <summary>
	/// wave description for level 6
	/// </summary>
	void generateLevel5(){
		int zombiePerSec = Random.Range(2,4);
		zombiePerSec += Mathf.FloorToInt (nGameTime / 10);
		int hugeGap1 = Random.Range (5, 12);

		
		int hugeNum1 = Random.Range (6, 8);
		
		
		
		int hugeGap2 = Random.Range (10, 30);
		
		
		
		int hugeNum2 = Random.Range (10,20);
		
		int specialGap1 = Random.Range (10, 15);
		int specialGap2 = Random.Range (4, 10);
		
		int specialGap3 = 20;
		specialGap3 -= Mathf.FloorToInt (nGameTime / specialGap3)*2;
		specialGap3 = specialGap3 > 5 ? specialGap3 : 5;
		int specialNum1 = Random.Range (1, 4);
		int specialNum2 = Random.Range (1, 5);
		int specialNum3 = Random.Range (1, 3);
		//		int[] speicalType1 = {3,4}; 
		int[] speicalType2 = {4,5,6}; 
		int[] speicalType3 = {7}; 
		//gen 1 zombie/second
		for (int i = 0; i<zombiePerSec; i++) {
			if(GameData.getInstance().timeelaspe > 40){
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}else{
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
			
		}
		
		
		//create a huge
		if(nGameTime % hugeGap1 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum1;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 2));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// create a huge
		if(nGameTime % hugeGap2 == 0 && nGameTime > 0){
			for(int i = 0;i<hugeNum2;i++){
				
				GameObject tnewZombie = createZombie ((int)Random.Range (1, 4));
				tnewZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special magic
		if (nGameTime % specialGap1 == 0 && nGameTime > 0) {
			GameObject.Find("wind").SendMessage("startMove");
		}
		
		// gen a special enemy 2
		if (nGameTime % specialGap2 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum2;i++){
				GameObject tZombie = createZombie(speicalType2[(int)Random.Range(0,speicalType2.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
			}
		}
		
		// gen a special enemy 3 gaint
		if (nGameTime % specialGap3 == 0 && nGameTime > 0) {
			for (int i=0;i<specialNum3;i++){
				GameObject tZombie = createZombie(speicalType3[(int)Random.Range(0,speicalType3.Length)]);
				tZombie.transform.Translate(new Vector3(.1f*i,0,0));
				
			}
		}
	}
	
}
