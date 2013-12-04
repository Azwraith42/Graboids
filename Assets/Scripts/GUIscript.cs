using UnityEngine;
using System.Collections;

public class GUIscript : MonoBehaviour {

	int Score = 0;
	int asts = 1;

	//GUI
	void OnGUI(){
		GUILayout.Label ("Score: " + Score);
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 35, 10, 150, 30));
		GUILayout.Label ("Asteroids found: " + asts);
		GUILayout.EndArea ();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void IncScore(int ammount){
		Score += ammount;
	}

	void IncAsts(){
		asts += 1;
	}
}
