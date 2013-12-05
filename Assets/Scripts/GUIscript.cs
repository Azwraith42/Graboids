using UnityEngine;
using System.Collections;

public class GUIscript : MonoBehaviour {

	public static int score;
	public static int asts;
	public static bool isDead;

	//GUI
	void OnGUI(){
		GUILayout.Label ("Score: " + score);
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 35, 10, 150, 30));
		GUILayout.Label ("Asteroids Detected: " + AsteroidControl.numAsteroids);
		GUILayout.EndArea ();

		if (isDead){
			GUILayout.BeginArea (new Rect (Screen.width / 2 - 35, Screen.height/2, 150, 50));
			GUILayout.Label ("GAME OVER\nPress 'q' to restart");
			GUILayout.EndArea ();
		}
	}


	// Use this for initialization
	void Start () {
		score = 0;
		asts = 0;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		// press q to restart the level
		if (Input.GetKey (KeyCode.Q)) {
			Application.LoadLevel(Application.loadedLevel);
		}	
	}

	public void IncScore(int amount){
		score += amount;
	}

	public void IncAsts(){
		asts += 1;
	}
}
