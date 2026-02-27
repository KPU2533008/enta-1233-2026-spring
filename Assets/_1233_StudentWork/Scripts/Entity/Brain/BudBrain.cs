using System;
using UnityEngine;

public class BudBrain : MonoBehaviour {
	public enum FireMode {
		FixedAxis,
		DirectAim,
		ArcFire
	}

	[Header("Components")]
	[SerializeField] private Health _health;
	[SerializeField] private ProjectileWeapon _weapon;
	[SerializeField] private DetectionSystem _detection;
	[SerializeField] private RotateToTarget _rotator;
	[SerializeField] private EnemyAnimatorDriver _animator;

	[Header("Settings")]
	[SerializeField] private FireMode _mode = FireMode.DirectAim;
	[SerializeField] private Vector3 _fixedAxis = Vector3.forward;

	private ITargetProvider _targetProvider;

	void Awake() {
		_targetProvider = GetComponent<ITargetProvider>();
		if ( _health == null ) _health = GetComponent<Health>();
		if ( _animator == null ) _animator = GetComponent<EnemyAnimatorDriver>();
	}

	void Update() {
		if ( _health != null && _health.IsDead )
			return;

		switch ( _mode ) {
			case FireMode.FixedAxis:
				if ( _weapon.CanFire ) {
					_animator?.TriggerAttack2();
					_weapon.Fire(transform.TransformDirection(_fixedAxis));
				}
				break;
			case FireMode.DirectAim:
				FaceAndAttackTarget((Vector3 targetPos) => {
					_animator?.TriggerAttack2();
					_weapon.Fire(targetPos);
				});
				break;
			case FireMode.ArcFire:
				FaceAndAttackTarget((Vector3 targetPos) => {
					_animator?.TriggerAttack1();
					_weapon.FireArc(targetPos);
				});
				break;
		}
	}

	void OnEnable() {
		if ( _health != null )
			_health.OnDied += HandleDied;
	}

	void OnDisable() {
		if ( _health != null )
			_health.OnDied -= HandleDied;
	}

	private void FaceAndAttackTarget(Action<Vector3> andThen) {
		if ( _targetProvider == null || !_targetProvider.HasTarget )
			return;

		Transform target = _targetProvider.GetTarget();
		Vector3 targetPos = _targetProvider.GetTargetPosition();

		if ( _detection.IsTargetInDetectionRange(target) && _detection.HasLineOfSight(target) ) {
			_rotator?.FacePosition(targetPos);
			if ( _weapon.CanFire ) {
				andThen(targetPos);
			}
		}
	}

	private void HandleDied() {
		enabled = false;
	}
}
