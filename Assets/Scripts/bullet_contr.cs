using UnityEngine;
using System.Collections;

public class bullet_contr : MonoBehaviour {

	public float bulletDurat = 0.5f; //how long until we kill bullet
	private float bulletStart = Mathf.Infinity; //stores starting time of bullet
	private float bullKillStart = Mathf.Infinity;
	private float bullKillDelay = 0.1f;


	private float LEFT, RIGHT, TOP, BOTTOM, WIDTH, HEIGHT;
	// Use this for initialization
	void Start () {

		// Find Borders
		LEFT = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		RIGHT = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
		TOP = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
		BOTTOM = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
		
		WIDTH = RIGHT - LEFT;
		HEIGHT = BOTTOM - TOP;

		bulletStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//screen wrap bullet
		if (transform.position.x < LEFT) // reaches left border
			transform.position += new Vector3(WIDTH, 0, 0); // move to right border
		if (transform.position.x > RIGHT) // reaches right border
			transform.position -= new Vector3(WIDTH, 0, 0); // move to left border
		if (transform.position.y < TOP) // reaches top border
			transform.position += new Vector3(0, HEIGHT, 0); // move to bottom border
		if (transform.position.y > BOTTOM) // reaches bottom border
			transform.position -= new Vector3(0, HEIGHT, 0); // move to top border

		if (bulletStart + bulletDurat < Time.time)
			Destroy (gameObject);

		//added a delay to killing because it wasn't fully transferring
		//momentum to collided asteroid if it was hitting it at an angle
		if(bullKillStart + bullKillDelay < Time.time)
			Destroy (gameObject);
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		bullKillStart = Time.time;
	}
}
