using UnityEngine;
using System.Collections;

public class KongregateScript : MonoBehaviour {
	public static bool playing;
	KongregateAPI kongAPI;

	// Use this for initialization
	void Start () {
		playing = false;
		DontDestroyOnLoad(gameObject);
		
		GameObject kongregateAPIObject = GameObject.Find("KongregateAPI");
		if(kongregateAPIObject != null)
			kongAPI = kongregateAPIObject.GetComponent<KongregateAPI>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//GUI
	void OnGUI(){
		if (!playing)
		{
			// If on Kongregate display welcome message to user.
			if(kongAPI != null){
				if(kongAPI.isKongregate)
					GUILayout.Box("Welcome " + kongAPI.username + "!");
				else
					GUILayout.Box("Kongregate Not Found");
			}
		}
	}

	void OnLevelWasLoaded(int level) {
	if (level == 2) {
			playing = true;
		}
	}

	// Game over, upload stats
	public void GameOver()	{
		kongAPI.SubmitStats("High Score", GUIscript.score); 
	}
}
