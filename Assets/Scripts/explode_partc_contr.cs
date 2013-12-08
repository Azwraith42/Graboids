using UnityEngine;
using System.Collections;

public class explode_partc_contr : MonoBehaviour {

	public float partcDurat = 0.1f; //how long we will emit particles

	private float partcStart = 0; //stores starting time of emission

	//delay after we stop emitting particles to kill object
	//we use a delay so we can let the emitted particles 
	//naturally die before killing off everything
	private float partcKillDelay =  1f; 

	// Use this for initialization
	void Start () {
		partcStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (partcStart + partcDurat < Time.time)
			particleSystem.enableEmission = false;
		else if (partcStart + partcDurat + partcKillDelay < Time.time)
			Destroy (gameObject);
	
	}
}
