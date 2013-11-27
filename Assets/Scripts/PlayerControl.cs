using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float rotationalForce;
	public float maxAngVel;
	public float moveForce;
	public float maxVel;
	public float pullForce;
	public float shootForce;
	public float shootSpinForce;
	private string axisName = "Horizontal";
	private RaycastHit2D grabTarget;
	private bool hasTarget = false;

	/*  ========TODO========
	 *  check obj type when pulling
	 *  apply tangental force or velocity to pulled obj
	 *  ====================
	 */

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {


		//rotate ship
		rigidbody2D.AddTorque(Input.GetAxis(axisName)*-rotationalForce);

		if (rigidbody2D.angularVelocity > maxAngVel)
			rigidbody2D.angularVelocity = maxAngVel;
		else if (rigidbody2D.angularVelocity < -maxAngVel)
			rigidbody2D.angularVelocity = -maxAngVel;


		//calculate the direction the ship is currently facing
		float rotInRads = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 shipDir = new Vector2(Mathf.Cos (rotInRads), Mathf.Sin (rotInRads));


		//propel ship in the angle its facing
		if (Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce (shipDir * moveForce);

		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;


		//pull in asteroid towards ship
		if (Input.GetKey (KeyCode.Space)) {
			//should check if it's an asteroid type when we get them
			if (!hasTarget){
				grabTarget = Physics2D.Raycast (transform.position, shipDir);
				if (grabTarget.collider != null){
					Debug.DrawRay (transform.position, shipDir);
					hasTarget = true;
				}
			}
			else{
				grabTarget.rigidbody.AddForce(-shipDir * pullForce);
				//missing tangent force on target if ship has angular velocity
				//so the target currently doesn't spin with the ship
			}

		}


		//shoot the asteroid being grabbed
		if (Input.GetKeyUp(KeyCode.Space)){
			if (hasTarget){
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
