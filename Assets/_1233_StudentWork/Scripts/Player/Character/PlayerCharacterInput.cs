using UnityEngine;

public struct PlayerCharacterInput {
	public readonly Vector3 MoveDirection;
	public readonly bool Jump;
	public readonly bool Sprint;

	public PlayerCharacterInput(Vector3 moveDir, bool jump, bool sprint) {
		MoveDirection = moveDir;
		Jump = jump;
		Sprint = sprint;
	}
}
