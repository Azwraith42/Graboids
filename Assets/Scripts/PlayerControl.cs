using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	//public GameObject Ship;
	public float rotationalForce;
	public float maxAngVel;
	public float moveForce;
	public float maxVel;
	public float pullForce;
	public float shootForce;
	public float shootSpinForce;
	public float gunRange;
	//public float minAstrDist;

	public Vector3 wrap = new Vector3();
	public Vector3 wrapMax = new Vector3();

	public GameObject death_particle;
	public GameObject bullet_prefab;

	private const float wrapOffset = 1.5f;
	private string axisName = "Horizontal";
	private RaycastHit2D grabTarget;

	private float LEFT, RIGHT, TOP, BOTTOM, WIDTH, HEIGHT;

	private float shootTime = 0;
	public float shootCooldown = 0.5f;

	/*  ========TODO========
	 *  ====================
	 */


	//awake to guarentee it happends first
	void Awake(){

		Vector3 wrapmin = Camera.main.ScreenToWorldPoint (new Vector3 (0, Camera.main.transform.position.y, 0));
		wrap = new Vector3(wrapmin.x-wrapOffset,wrapmin.y-wrapOffset, 0);
		Vector3 wrapmax = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)); //0 on this line changed from Camera.main.transform.position.y
		wrapMax = new Vector3 (wrapmax.x + wrapOffset, wrapmax.y + wrapOffset, 0);
	}

	// Use this for initialization
	void Start () {
		// Find Borders
		LEFT = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		RIGHT = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
		TOP = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
		BOTTOM = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
		
		WIDTH = RIGHT - LEFT;
		HEIGHT = BOTTOM - TOP;

		Physics2D.IgnoreLayerCollision(10, 10);
	}

	// Update is called once per frame
	void Update () {

		

		//calculate the direction the ship is currently facing
		float rotInRads = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 shipDir = new Vector2(Mathf.Cos (rotInRads), Mathf.Sin (rotInRads));


		//shoot bullet
		if (Input.GetKeyDown(KeyCode.Space) && (shootTime + shootCooldown < Time.time)) {
			GameObject bullet = (GameObject)Instantiate(bullet_prefab, transform.position, Quaternion.identity);
			bullet.rigidbody2D.AddForce(shipDir * shootForce);
			shootTime = Time.time;
		}
		

		// using this version of wrapping for accuracy purposes
		// have ship wrap when it reaches the edge of the screen
		if (transform.position.x < LEFT) // reaches left border
			transform.position += new Vector3(WIDTH, 0, 0); // move to right border
		if (transform.position.x > RIGHT) // reaches right border
			transform.position -= new Vector3(WIDTH, 0, 0); // move to left border
		if (transform.position.y < TOP) // reaches top border
			transform.position += new Vector3(0, HEIGHT, 0); // move to bottom border
		if (transform.position.y > BOTTOM) // reaches bottom border
			transform.position -= new Vector3(0, HEIGHT, 0); // move to top border
		
		
		//rotate ship
		rigidbody2D.AddTorque(Input.GetAxis(axisName)*-rotationalForce);


		//propel ship in the angle its facing
		if (Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce (shipDir * moveForce);

		//enforce maximum values
		if (rigidbody2D.angularVelocity > maxAngVel)
			rigidbody2D.angularVelocity = maxAngVel;
		else if (rigidbody2D.angularVelocity < -maxAngVel)
			rigidbody2D.angularVelocity = -maxAngVel;

		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;
		

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Asteroid") {
			GameObject explosion_particle = (GameObject)Instantiate (death_particle, transform.position, Quaternion.identity);
			GUIscript.isDead = true;
			Destroy (this.gameObject);
			EventManager.instance.QueueEvent (new ShipDestroy ());
		}
	}
}
