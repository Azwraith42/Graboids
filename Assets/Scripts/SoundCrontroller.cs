using UnityEngine;
using System.Collections;


public class ShipDestroy : BaseEvent{}


public class SoundCrontroller : MonoBehaviour, IEventListener {

	public AudioSource[] CrashSources;

	public bool HandleEvent(IEvent e){
		return false;
	}

	// Use this for initialization
	void Start () {
		EventManager.instance.AddListener (this, "ShipDestroy", this.onShipDestroy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool onShipDestroy(IEvent e){
		CrashSources [0].Play ();
		return true;
	}
}
