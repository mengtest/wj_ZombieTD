using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class musicScript : MonoBehaviour {
	
	public static GameObject instance = null;
	void Start () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this.gameObject;
		}
		DontDestroyOnLoad (gameObject);
		asgroups = new List<AudioSource> ();
		StartCoroutine("recycle");
	}
		
	List<AudioSource> asgroups;
	IEnumerator recycle(){
		while (true) {
			yield return new WaitForSeconds(.1f);

			if(asgroups.Count > 30){
				for(int i = 0;i < 15;i++){

					Destroy(asgroups[0]);
					asgroups.RemoveAt(0);
				}
			}
		}
	}




	void OnApplicationPause(bool pauseStatus)
	{ 
//		Application.LoadLevel (Application.loadedLevelName);


//		if(!pauseStatus){
//			if(GameData.back2main % 3 == 0){
//				GameManager.getInstance ().showInterestitial ();
//				GameData.back2main++;
//			}
//
//		}


	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Time.timeScale = 1;
			//			GameData.getInstance().init();
			string tName = SceneManager.GetActiveScene().name;
			Debug.Log(SceneManager.GetActiveScene().name);
			if (tName == "level") {

				SceneManager.LoadScene ("LevelMenu");
			} else if(tName == "LevelMenu" ){

				SceneManager.LoadScene ("MainMenu");
			}else if(tName == "MainMenu"){
				

			}
			
			
			
			
		}
	}


	public AudioSource PlayAudioClip(AudioClip clip,bool isloop = false)
	{
		if (clip == null)return null;


		AudioSource	source;

		if (isloop) {
			bool tExist = false;
			AudioSource[] as1 = GetComponentsInChildren<AudioSource>();
			foreach(AudioSource tas in as1){
				if(tas && tas.clip){
					string clipname = (tas.clip.name);
					if(clipname == clip.name){
						source = tas;
						tExist = true;
						source.Play();
						return source;
						break;
					}
				}
			}
		}

		source = (AudioSource)gameObject.AddComponent<AudioSource>();




		source.clip = clip;source.minDistance = 1.0f;source.maxDistance = 50;source.rolloffMode = AudioRolloffMode.Linear;
		source.transform.position = transform.position;
		source.loop = isloop;
		source.Play();
		if (!isloop) {//not bg
			asgroups.Add (source);
		}
		return source;
	}

}
