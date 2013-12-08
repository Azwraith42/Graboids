using UnityEngine;
using System.Collections;

public class AsteroidInfo : MonoBehaviour {
	public enum Size {Small=1, Medium=2, Large=4}
	public Size astrSize = Size.Large;

	public float maxVel, breakVel;

	public int health = (int)Size.Large;
	
	private float LEFT, RIGHT, TOP, BOTTOM, WIDTH, HEIGHT;


	public float maxInitialVelocity = 1;
	public float medScale = 0.2f;
	public float smallScale = 0.15f;

	/*  ========TODO========
	 *  asteroid division
	 *  different sizes
	 *  asteroid hp -> visual "cracking"
	 *  better wrapping
	 *  ====================
	 */


	// Use this for initialization
	void Start () {
		// Find Borders
		LEFT = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		RIGHT = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
		TOP = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
		BOTTOM = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
		
		WIDTH = RIGHT - LEFT;
		HEIGHT = BOTTOM - TOP;
	}
	
	// Update is called once per frame
	void Update () {
		// limit to maximum speed;
		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;
	
		// have asteroid wrap when it reaches the edge of the screen
		if (transform.position.x < LEFT) // reaches left border
			transform.position += new Vector3(WIDTH, 0, 0); // move to right border
		if (transform.position.x > RIGHT) // reaches right border
			transform.position -= new Vector3(WIDTH, 0, 0); // move to left border
		if (transform.position.y < TOP) // reaches top border
			transform.position += new Vector3(0, HEIGHT, 0); // move to bottom border
		if (transform.position.y > BOTTOM) // reaches bottom border
			transform.position -= new Vector3(0, HEIGHT, 0); // move to top border

		if (health <= 0)
			split ();

	}



	void split()
	{
		switch (astrSize) 
		{
			case Size.Large:
			{
				transform.localScale = new Vector2(medScale, medScale);
				rigidbody2D.velocity = Random.insideUnitCircle * maxInitialVelocity;
				rigidbody2D.angularVelocity = 0;
				astrSize = Size.Medium;
				health = (int)Size.Medium;

				//GameObject asteroidClone = (GameObject)Instantiate(gameObject, transform.position, Quaternion.identity);
				//asteroidClone.rigidbody2D.velocity = Random.insideUnitCircle * maxInitialVelocity;
				break;
			}

			case Size.Medium:
			{
				transform.localScale = new Vector2(smallScale, smallScale);
				rigidbody2D.velocity = Random.insideUnitCircle * maxInitialVelocity;
				rigidbody2D.angularVelocity = 0;
				astrSize = Size.Small;
				health = (int)Size.Small;

				//GameObject asteroidClone = (GameObject)Instantiate(gameObject, transform.position, Quaternion.identity);
				//asteroidClone.rigidbody2D.velocity = Random.insideUnitCircle * maxInitialVelocity;
				break;
			}

			case Size.Small:
			{
				AsteroidControl.numAsteroids--;
				Destroy(gameObject);
				break;
			}

		}
	}


	// Asteroid takes damage and bounces or breaks when hit hard enough
	void OnCollisionEnter2D(Collision2D coll) {

		// asteroid hits player ship, destroy player
		if (coll.gameObject.name == "ship")
		{
		}
		// else if hits another asteroid
		else if (coll.gameObject.tag == "Asteroid")
		{
			// check relative velocity/force
			if (coll.relativeVelocity.magnitude >= breakVel)
			{
				var collidingAstr = coll.gameObject.GetComponent<AsteroidInfo>();
				//asteriod splits in update if health <= 0
				health = health - (int)collidingAstr.astrSize;
				GUIscript.score += 5;

			}

		}

	}

	// Asteroid splits
	void divide() {

	}
}
