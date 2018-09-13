using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	#region Variables
	[Header("Visuals")]
	public GameObject model;
	public float rotatingSpeed = 2f;
	[Header("Movement")]
	public float movingVelocity;
	public float jumpingVelocity = 5f;

	[Header("Equipments")]
	public Sword sword;
	RaycastHit hit;
	public GameObject bombPrefab;
	public float throwingSpeed;
	public int bombAmount = 5;

	private bool canJump;
	private Rigidbody playerRigidBody;
	private Quaternion targetModelRotation;
	#endregion Variables

	#region Functions
	private void Start()
	{
		playerRigidBody = GetComponent<Rigidbody>();
		targetModelRotation = Quaternion.Euler(0,0,0);
	}
	private void FixedUpdate()
	{
		model.transform.rotation = Quaternion.Lerp(model.transform.rotation,
									  targetModelRotation, Time.deltaTime * rotatingSpeed);
		ProcessInput();

		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f))
		{
			Debug.Log("Is not jumping");
			canJump = true;
		}
	}

	#endregion Functions

	#region Methods

	public void ProcessInput()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(
			0,
			playerRigidBody.velocity.y,
			0
			);
		if (Input.GetKey(KeyCode.D)| Input.GetKey(KeyCode.RightArrow))
		{
			playerRigidBody.velocity = new Vector3(movingVelocity,
										playerRigidBody.velocity.y,
										playerRigidBody.velocity.z);
//			transform.position += Vector3.right * movingSpeed * Time.deltaTime;
			targetModelRotation = Quaternion.Euler(0,90,0);
		}
		if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
		{
			playerRigidBody.velocity = new Vector3( -movingVelocity,
										 playerRigidBody.velocity.y,
										 playerRigidBody.velocity.z);
//			transform.position += Vector3.left * movingSpeed * Time.deltaTime;
			targetModelRotation = Quaternion.Euler(0,270,0);
//			model.transform.localEulerAngles = new Vector3(0,90, 0);
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x,
													playerRigidBody.velocity.y,
													movingVelocity);
//			transform.position += Vector3.forward * movingSpeed * Time.deltaTime;
			targetModelRotation = Quaternion.Euler(0,0,0);
//			model.transform.localEulerAngles = new Vector3(0,180,0);
		}
		if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
		{
			playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x,
												  playerRigidBody.velocity.y,
												 -movingVelocity);

//			transform.position += Vector3.back * movingSpeed * Time.deltaTime;
			targetModelRotation = Quaternion.Euler(0,180,0);
//			model.transform.localEulerAngles = new Vector3(0,0,0);
		}

		if (canJump && Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Is jumping");
			canJump = false;
			playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x,
				jumpingVelocity,
				playerRigidBody.velocity.z);
		}

		// Check equipment interaction
		if(Input.GetKeyDown("z"))
		{
			sword.Attack();
		}

		if(Input.GetKeyDown(KeyCode.X))
		{
			ThrowBomb();
		}
	}

	private void ThrowBomb()
	{
		if(bombAmount <= 0)
		{
			return;
		}
		GameObject bombObject = Instantiate(bombPrefab); // A bomb will appear on the scene.
		bombObject.transform.position = transform.position + model.transform.forward;

		Vector3 throwingDirection = (model.transform.forward + Vector3.up);

		bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);

		bombAmount--;
	}
	#endregion Methods

} // Main Class
