using Assets._1233_StudentWork.Scripts.FSM;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour {

    public CharacterController _characterController { get; private set; }

    //private float _gravity = 9.81f;

    //private float SMALL_BUT_NOT_ZERO = 0.1f;
    //private float _yVelocity = 0.0f;

    void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    //void OnJump() {
    //    if ( _characterController.isGrounded )
    //        _yVelocity = 5;
    //}

    //private void StepGravity() {
    //    if ( _characterController.isGrounded && _yVelocity < -SMALL_BUT_NOT_ZERO ) {
    //        _yVelocity = -SMALL_BUT_NOT_ZERO;
    //        return;
    //    }
    //    _yVelocity -= _gravity * Time.deltaTime;
    //}

    //void Update() {
    //    StepGravity();
    //}

}
