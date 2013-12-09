using UnityEngine;
using System.Collections;


public class ShipDestroy : BaseEvent{}
public class LargeAsteroid : BaseEvent{}
public class SmallAsteroid : BaseEvent{}
public class Move : BaseEvent{}
public class Stop : BaseEvent{}
public class Shoot : BaseEvent{}



public class SoundCrontroller : MonoBehaviour, IEventListener {

	public AudioSource[] CrashSources;
	public AudioSource[] AsteroidSources;
	public AudioSource[] MoveSources;
	public AudioSource[] WeaponSources;

	public bool HandleEvent(IEvent e){
		return false;
	}

	// Use this for initialization
	void Start () {
		EventManager.instance.AddListener (this, "ShipDestroy", this.onShipDestroy);
		EventManager.instance.AddListener (this, "LargeAsteroid", this.onLargeAsteroid);
		EventManager.instance.AddListener (this, "SmallAsteroid", this.onSmallAsteroid);
		EventManager.instance.AddListener (this, "Move", this.onMove);
		EventManager.instance.AddListener (this, "Stop", this.onStop);
		EventManager.instance.AddListener (this, "Shoot", this.onShoot);
	}

	//plays when you press space to fire the gun
	public bool onShoot(IEvent e){
		WeaponSources[0].Play();
		return true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//when you crash into an asteroid
	public bool onShipDestroy(IEvent e){
		CrashSources [0].Play ();
		return true;
	}

	//when you destroy a large asteroid
	public bool onLargeAsteroid(IEvent e){
		AsteroidSources[0].Play();
		return true;
	}

	//when you destroy a medium or small asteroid
	public bool onSmallAsteroid(IEvent e){
		AsteroidSources[1].Play();
		return true;
	}

	//played while the up arrow is held down
	public bool onMove(IEvent e){
		//Debug.Log("Key Down");
		MoveSources[0].Play();
		return true;
	}
	public bool onStop(IEvent e){
		//Debug.Log("Key Up");
		MoveSources[0].Stop();
		return true;
	}
}
