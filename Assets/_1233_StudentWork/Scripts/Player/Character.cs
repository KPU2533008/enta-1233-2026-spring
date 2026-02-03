using Assets._1233_StudentWork.Scripts.Enum;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {

    [SerializeField] private CinemachineCamera orbitalCam;
    [SerializeField] private CinemachineCamera fixedCam;

    private CharacterController _characterController;
	private Vector2 moveDirection = new();
    private CharacterRelativeMovementMode movementMode = CharacterRelativeMovementMode.Camera;

	private float _gravity = 9.81f;

    private float SMALL_BUT_NOT_ZERO = 0.1f;
    private float _yVelocity = 0.0f;

	void Awake() {
        _characterController = GetComponent<CharacterController>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()  {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnMove(InputValue value) {
        Vector2 moveVector = value.Get<Vector2>();
        moveDirection = moveVector;
    }

    void OnJump() {
        if (_characterController.isGrounded)
            _yVelocity = 5;
    }

    void OnCameraToggle() {
        if ( orbitalCam.gameObject.activeSelf ) {
            orbitalCam.gameObject.SetActive(false);
            fixedCam.gameObject.SetActive(true);
        } else {
            orbitalCam.gameObject.SetActive(true);
            fixedCam.gameObject.SetActive(false);
        }
    }

    void OnMovementMode() {
        movementMode = movementMode == CharacterRelativeMovementMode.Camera ? CharacterRelativeMovementMode.Character : CharacterRelativeMovementMode.Camera;
    }

    private void StepGravity() {
        if ( _characterController.isGrounded && _yVelocity < -SMALL_BUT_NOT_ZERO ) {
            _yVelocity = -SMALL_BUT_NOT_ZERO;
            return;
        }
        _yVelocity -= _gravity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        StepGravity();

        Vector3 rightVector = Vector3.right;

        if ( movementMode == CharacterRelativeMovementMode.Character ) {
            rightVector = transform.right;
        } else if ( movementMode == CharacterRelativeMovementMode.Camera ) {
            Camera camera = CameraMgr.Instance._mainCamera;
            rightVector = camera.transform.right;
        }

        Quaternion quat = Quaternion.LookRotation(Vector3.Cross(rightVector, Vector3.up), Vector3.up);
        _characterController.Move(quat * new Vector3(moveDirection.x, _yVelocity, moveDirection.y) * Time.deltaTime * 4);
    }
}
