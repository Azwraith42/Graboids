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

	public Vector3 wrap = new Vector3();
	public Vector3 wrapMax = new Vector3();

	private const float wrapOffset = 1.5f;
	private string axisName = "Horizontal";
	private RaycastHit2D grabTarget;
	private bool hasTarget = false;

	//for playtest purposes
	//used to teleport last shot object back with 'Q' 
	private RaycastHit2D debugTargetHolder;

	private float LEFT, RIGHT, TOP, BOTTOM, WIDTH, HEIGHT;

	/*  ========TODO========
	 *  check obj type when pulling
	 *  grabbed target doesn't move immediately after being parented
	 *  make grabbed target move towards ship properly
	 *  ====================
	 */


	//awake to guarentee it happends first
	void Awake(){
//		var wrapmin:Vector3=Camera.main.ScreenToWorldPoint(Vector3(0,0,Camera.main.transform.position.y));
//		wrap=Vector3(wrapmin.x-4,wrapmin.z-4);
//		var wrapmax:Vector3=Camera.main.ScreenToWorldPoint(Vector3(Screen.width,Screen.height,Camera.main.transform.position.y));
//		wrapMax=Vector3(wrapmax.x+4,wrapmax.z+4);

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
	}

	// Update is called once per frame
	void Update () {




		//calculate the direction the ship is currently facing
		float rotInRads = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 shipDir = new Vector2(Mathf.Cos (rotInRads), Mathf.Sin (rotInRads));



		//pull in asteroid towards ship
		if (Input.GetKey (KeyCode.Space)) {
			//should check if it's an asteroid type when we get them
			Debug.DrawRay (transform.position, shipDir);
			if (!hasTarget){
				grabTarget = Physics2D.Raycast (transform.position, shipDir);
				if (grabTarget.collider != null){
					debugTargetHolder = grabTarget;
					//Debug.DrawRay (transform.position, shipDir);
					grabTarget.rigidbody.angularVelocity = 0;
					grabTarget.rigidbody.velocity = Vector2.zero;
					grabTarget.transform.parent = transform;
					hasTarget = true;
				}
			}
			else{
				//grabTarget.rigidbody.AddForce(-shipDir * pullForce);
				//Debug.DrawRay (grabTarget.transform.position, astrDir);
				float dist2Target = Vector2.Distance(transform.position, grabTarget.transform.position);
			}

		}



		//screen wrap
		/*if (rigidbody2D.transform.position.x > wrapMax.x) {
			rigidbody2D.transform.position = new Vector3(wrap.x, transform.position.y);
		}
		if (rigidbody2D.transform.position.x < wrap.x) {
			rigidbody2D.transform.position = new Vector3(wrapMax.x, transform.position.y);
		}
		if (rigidbody2D.transform.position.y > wrapMax.y) {
			rigidbody2D.transform.position = new Vector3(transform.position.y, wrap.y);
		}
		if (rigidbody2D.transform.position.y < wrap.y) {
			rigidbody2D.transform.position = new Vector3(transform.position.y, wrapMax.y);
		}*/

		// using this version of wrapping for accuracy purposes
		// have ship wrap when it reaches the edge of the screen
		if (transform.position.x < LEFT) // reaches left border
			transform.position += new Vector3(WIDTH, 0, 0); // move to right border
		if (transform.position.x > RIGHT) // reaches right border
			transform.position -= new Vector3(WIDTH, 0, 0); // move to left border
		if (transform.position.y < TOP) // reaches top border
			transform.position += new Vector3(0, HEIGHT, 0); // move to bottom border
		if (transform.position.y > BOTTOM) // reaches bottom border
			transform.position -= new Vector3(0, HEIGHT, 0);; // move to top border
		
		
		//rotate ship
		rigidbody2D.AddTorque(Input.GetAxis(axisName)*-rotationalForce);
		//rigidbody2D.angularVelocity = Input.GetAxis(axisName)*-maxAngVel;
		
		if (rigidbody2D.angularVelocity > maxAngVel)
			rigidbody2D.angularVelocity = maxAngVel;
		else if (rigidbody2D.angularVelocity < -maxAngVel)
			rigidbody2D.angularVelocity = -maxAngVel;

		
		//propel ship in the angle its facing
		if (Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce (shipDir * moveForce);
		
		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;


		//shoot the asteroid being grabbed
		if (Input.GetKeyUp(KeyCode.Space)){
			if (hasTarget){
				grabTarget.transform.parent = null;
				grabTarget.rigidbody.velocity = Vector2.zero;
				grabTarget.rigidbody.angularVelocity = 0;
				grabTarget.rigidbody.AddTorque(shootSpinForce);
				grabTarget.rigidbody.AddForce(shipDir * shootForce);
				hasTarget = false;
			}
		}



		//for play test: resetting ship loc
		if (Input.GetKey (KeyCode.Q)) {
			transform.position = -2*Vector2.right;
			rigidbody2D.angularVelocity = 0;
			rigidbody2D.velocity = Vector2.zero;
			if (debugTargetHolder.collider != null){
				debugTargetHolder.transform.position = Vector3.zero;
				debugTargetHolder.rigidbody.velocity = Vector2.zero;
				debugTargetHolder.rigidbody.angularVelocity = 0;
			}
		}
		
	}
}
