using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Settings")]
	public float moveForce = 10000f;
	public float maxSpeed = 3f;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// We use FixedUpdate for physics calculations (AddForce)
	void FixedUpdate()
	{
		Vector3 inputDirection = Vector3.zero;

		// 1. Check Input using KeyCode
		if (Input.GetKey(KeyCode.W)) inputDirection.z = 1;
		if (Input.GetKey(KeyCode.S)) inputDirection.z = -1;
		if (Input.GetKey(KeyCode.A)) inputDirection.x = -1;
		if (Input.GetKey(KeyCode.D)) inputDirection.x = 1;

		if (Input.GetKey(KeyCode.W)) print("Movement");
		// 2. Apply Force
		// We use .normalized so moving diagonally isn't faster than moving straight
		rb.AddForce(inputDirection.normalized * moveForce * Time.fixedDeltaTime);

		// 3. Speed Limiter
		// If current velocity exceeds maxSpeed, cap it
		if (rb.linearVelocity.magnitude > maxSpeed)
		{
			rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
		}
	}
}