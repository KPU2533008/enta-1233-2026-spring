using Assets._1233_StudentWork.Scripts.FSM;
using Assets._1233_StudentWork.Scripts.PlayerCharacter.States;
using Assets._1233_StudentWork.Scripts.PlayerCharacter.Transitions;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Character_MX02 : Character {

	[SerializeField] private Animator _animator;

	private static Dictionary<Type, int> STATE_POSE_ID_MAP = new() {
		{ typeof(Idling), 0 },
		{ typeof(Locomoting), 1 },
		{ typeof(Jumping), 2 },
		{ typeof(Freefalling), 2 }
	};

	public FSM<Character_MX02, PlayerCharacterInput> StateMachine { get; private set; }
	public Vector3 velocity = Vector3.zero;
	public float walkSpeed = 1f;
	public float jumpPower = 1f;

	void Awake() {}

	void Start() {
		Player player = PlayerService.Instance.GetPlayerFromCharacter(this);

		List<MX02StateBase> states = new() {
			new Idling(),
			new Locomoting(),
			new Jumping(),
			new Landed(),
			new Freefalling(),
		};

		List<Type> transitions = new() {
			typeof(Freefall),
			typeof(Jump),
			typeof(Land),
			typeof(LocomotionStart),
			typeof(LocomotionStop),
		};

		StateMachine = new(this, states, transitions, states[0], player.CharacterInputs);
	}

	void Update() {
		float dt = Time.deltaTime;
		Player player = PlayerService.Instance.GetPlayerFromCharacter(this);
		StateMachine.Step(dt, player.CharacterInputs);
		Controller.Move(velocity * dt);

		FSM_State<Character_MX02, PlayerCharacterInput> currentState = StateMachine.GetState();
		if ( STATE_POSE_ID_MAP.TryGetValue(currentState.GetType(), out int poseId))
			_animator.SetInteger("PoseId", poseId);
	}

}
