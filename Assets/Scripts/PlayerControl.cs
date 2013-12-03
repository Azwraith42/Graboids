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
	private string axisName = "Horizontal";
	private RaycastHit2D grabTarget;
	private bool hasTarget = false;

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
		wrap = new Vector3(wrapmin.x-4,wrapmin.y-4, 0);
		Vector3 wrapmax = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)); //0 on this line changed from Camera.main.transform.position.y
		wrapMax = new Vector3 (wrapmax.x + 4, wrapmax.y + 4, 0);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		//screen wrap -------------  HOW THE FUCK DO POSITION CHANGE???
		/*
		if (rigidbody2D.transform.position.x > wrapMax.x) {
			rigidbody2D.transform.position.x = wrap.x;
		}
		if (rigidbody2D.transform.position.x < wrap.x) {
			rigidbody2D.transform.position.x = wrapMax.x;
		}
		if (rigidbody2D.transform.position.y > wrapMax.y) {
			rigidbody2D.transform.position.y = wrap.y;
		}
		if (rigidbody2D.transform.position.y < wrap.y) {
			rigidbody2D.transform.position.y = wrapMax.y;
		}
		*/

		//rotate ship
		rigidbody2D.AddTorque(Input.GetAxis(axisName)*-rotationalForce);


		if (rigidbody2D.angularVelocity > maxAngVel)
			rigidbody2D.angularVelocity = maxAngVel;
		else if (rigidbody2D.angularVelocity < -maxAngVel)
			rigidbody2D.angularVelocity = -maxAngVel;


		//calculate the direction the ship is currently facing
		float rotInRads = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 shipDir = new Vector2(Mathf.Cos (rotInRads), Mathf.Sin (rotInRads));



		//angle that the asteroid will move if ship pulls it while spinning
		float astrDirRad = -99999999999;
		if (rigidbody2D.angularVelocity > 0)
			astrDirRad = (transform.eulerAngles.z + 90)* Mathf.Deg2Rad;
		else
			astrDirRad = (transform.eulerAngles.z - 90)* Mathf.Deg2Rad;

		Vector2 astrDir = Vector2.zero;
		if (rigidbody2D.angularVelocity != 0)
			astrDir = new Vector2(Mathf.Cos (astrDirRad), Mathf.Sin (astrDirRad));



		//propel ship in the angle its facing
		if (Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce (shipDir * moveForce);

		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;


		//pull in asteroid towards ship
		if (Input.GetKey (KeyCode.Space)) {
			//should check if it's an asteroid type when we get them
			Debug.DrawRay (transform.position, shipDir);
			if (!hasTarget){
				grabTarget = Physics2D.Raycast (transform.position, shipDir);
				if (grabTarget.collider != null){
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
			transform.position = Vector2.zero;
			rigidbody2D.angularVelocity = 0;
			rigidbody2D.velocity = Vector2.zero;
		}
		
	}
}
