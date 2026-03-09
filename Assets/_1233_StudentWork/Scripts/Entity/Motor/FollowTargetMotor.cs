using UnityEngine;

[RequireComponent(typeof(ITargetProvider))]
public class FollowTargetMotor : Motor {

	[SerializeField] private float _minimumFollowDistance = 1f;

	private ITargetProvider _targetProvider;

	private void Start() {
		_targetProvider = GetComponent<ITargetProvider>();
	}
}