using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	//awake runs before start
	void Awake(){
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//GUI
	void OnGUI(){
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 50 , Screen.height / 2, 100, 300));

			if (GUILayout.Button ("New Game")) {
				Application.LoadLevel("Graboids");
			}

			if (GUILayout.Button ("How To Play")) {
				Application.LoadLevel("HowToPlay");
				//Debug.Log("WoW so press");
			}

			if (GUILayout.Button ("Credits")) {
				Application.LoadLevel("Credits");
				//Debug.Log("WoW so press");
			}

		GUILayout.EndArea ();

//		//GUILayout.BeginArea (new Rect (Screen.width / 2 - 50 , Screen.height / 2 + 50, 100, 30));
//		
//		if (GUILayout.Button ("How To Play")) {
//			//Application.LoadLevel("HowToPlay");
//			Debug.Log("WoW so press");
//		}
//		
//		//GUILayout.EndArea ();
	}
}
