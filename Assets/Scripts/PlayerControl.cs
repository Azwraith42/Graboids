using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float rotationalForce;
	public float maxAngVel;
	public float moveForce;
	public float maxVel;
	private string axisName = "Horizontal";


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


		//propel ship in the angle its facing
		float xComponent = Mathf.Cos (transform.eulerAngles.z * Mathf.Deg2Rad) * moveForce;
		float yComponent = Mathf.Sin (transform.eulerAngles.z * Mathf.Deg2Rad) * moveForce;
		if (Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce (new Vector2 (xComponent, yComponent));

		if (rigidbody2D.velocity.magnitude > maxVel)
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxVel;

	}
}
