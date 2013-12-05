using UnityEngine;
using System.Collections;

public class AsteroidControl : MonoBehaviour {
	public Rigidbody2D asteroid;

	public static int numAsteroids = 0;
	public int maxAsteroids = 8;
	public float maxInitialVelocity;

	public float spawnTimer;
	private float spawnTime = 0;
	
	private float LEFT, RIGHT, TOP, BOTTOM, WIDTH, HEIGHT;

	/*  ========TODO========
	 *  score for different sizes
	 *  spawn checks overlapping
	 *  ====================
	 */


	// Use this for initialization
	void Start () {
		LEFT = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		RIGHT = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
		TOP = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
		BOTTOM = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
		
		WIDTH = RIGHT - LEFT;
		HEIGHT = BOTTOM - TOP;

		numAsteroids = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTime > spawnTimer && numAsteroids < maxAsteroids)
		{
			// spawn asteroid
			Rigidbody2D asteroidClone = (Rigidbody2D) Instantiate(asteroid, new Vector3(LEFT, Random.value * HEIGHT, 0), Quaternion.identity);
			asteroidClone.gameObject.layer = 9;
			asteroidClone.velocity = Random.insideUnitCircle * maxInitialVelocity;
			spawnTime = 0;
			numAsteroids++;
		}	
		else
			spawnTime += Time.deltaTime * (Time.timeSinceLevelLoad / 10);
	}
}
