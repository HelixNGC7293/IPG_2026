using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private PlayerInput playerInput; // 2. Reference to the Input Component

	// Input Action references to avoid string lookups in Update
	private InputAction moveAction;
	private InputAction lookAction;
	private InputAction jumpAction;
	private InputAction fireAction;
	private CharacterController charController;
    private float speed = 5;
    [SerializeField]
    private float mouseSensitivity = 35f;

    Transform cameraTrans;
    float cameraPitch = 0;
    float gravityValue = Physics.gravity.y;
    float jumpHeight = -2f;

    float currentYVelocity;

    [SerializeField]
    Transform gunPoint;


    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        cameraTrans = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Move"];
		lookAction = playerInput.actions["Look"];
		jumpAction = playerInput.actions["Jump"];
		fireAction = playerInput.actions["Fire"];
	}

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
			//The New Input System returns Look as a Vector2 Delta (pixel changes)
			Vector2 mouseDelta = lookAction.ReadValue<Vector2>(); 
            transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity * Time.deltaTime);

            //Constraint the camera pitch inbetween -90 to 90
            cameraPitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime;
            cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
            cameraTrans.localEulerAngles = Vector3.right * cameraPitch;
			//cameraTrans.Rotate(Vector3.left * mouseDelta.y * mouseSensitivity);

			Vector2 inputVector = moveAction.ReadValue<Vector2>();
			Vector3 move = transform.rotation * new Vector3(inputVector.x, 0, inputVector.y);

			

            // Changes the height position of the player..
            if (jumpAction.WasPressedThisFrame() && charController.isGrounded)
            {
                currentYVelocity = Mathf.Sqrt(2 * jumpHeight * gravityValue);
            }
            else if (charController.isGrounded)
            {
                currentYVelocity = -0.5f;
            }
            move.y = currentYVelocity;
            currentYVelocity += gravityValue * Time.deltaTime;

            charController.Move(move * Time.deltaTime * speed);

			if (fireAction.WasReleasedThisFrame())
			{
                SpawnerManager.instance.SpawnBullets(gunPoint.position, cameraTrans.rotation);
			}
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameManager.instance.GameOver();
        }
	}
}
