using UnityEngine;
using System.Collections;

public class ship_partc_contr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particleSystem.enableEmission = false;
		if (Input.GetKey(KeyCode.UpArrow)) {
			particleSystem.enableEmission = true;
		}
	}
}
