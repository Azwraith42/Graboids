using UnityEngine;
using System.Collections;

public class HowToScript : MonoBehaviour {

	//GUI
	void OnGUI(){
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 50, Screen.height - 50, 100, 300));

		if (GUILayout.Button ("Back")) {
			Application.LoadLevel("MainMenu");
		}

		GUILayout.EndArea ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
