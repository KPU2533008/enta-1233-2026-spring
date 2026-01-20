using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {


    private CharacterController _characterController;
    private Vector2 moveDirection = new();
    private float _gravity = 9.81f;

    private float SMALL_BUT_NOT_ZERO = 0.1f;
    private float _yVelocity = 0.0f;

	void Awake() {
        _characterController = GetComponent<CharacterController>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()  {
        
    }

    void OnMove(InputValue value) {
        Vector2 moveVector = value.Get<Vector2>();
        moveDirection = moveVector;
    }

    void OnJump() {
        if (_characterController.isGrounded)
            _yVelocity = 5;
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
        Quaternion quat = Quaternion.LookRotation(gameObject.transform.forward, Vector3.up);
        _characterController.Move(quat * new Vector3(moveDirection.x, _yVelocity, moveDirection.y) * Time.deltaTime);
    }
}
