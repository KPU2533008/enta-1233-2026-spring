using System.Collections.Generic;
using UnityEngine;

public class PatrolMotor : Motor {

	[SerializeField] private List<Vector3> _waypoints;
	[SerializeField] private int _repeatCount = -1;
	[SerializeField] private bool _reverses = false;

	private Sequence sequence;

	private bool HasReachedEnd => sequence.HasReachedEnd;
	private bool IsFinished => sequence.IsFinished;

	private void Awake() {
		sequence = new Sequence(_waypoints.Count, _repeatCount, _reverses);
	}

	protected override void Start() {
		base.Start();
	}

	private void Update() {
		if ( _mover.IsAtDestination && ( !IsFinished || !HasReachedEnd ) ) {
			sequence.AdvanceToNext();
			_mover.SetDestination(_waypoints[sequence.Current]);
		}
	}

	private void OnDrawGizmosSelected() {
		Sequence gizmoSq = new Sequence(_waypoints.Count, _repeatCount, _reverses);
		Dictionary<bool, Dictionary<int, bool>> seen = new();
		seen[false] = new();
		seen[true] = new();

		Gizmos.color = Color.green;

		for ( int i = 0; i < _waypoints.Count; i++ ) {
			Gizmos.DrawSphere(_waypoints[i], 0.1f);
			Gizmos.color = Color.cyan;
		}

		while ( true ) {
			int current = gizmoSq.Current;

			if ( seen[gizmoSq.IsReversing].TryGetValue(current, out bool _) )
				break;
			seen[gizmoSq.IsReversing][current] = true;
			gizmoSq.AdvanceToNext();

			int next = gizmoSq.Current;

			if ( current == next )
				break;

			Gizmos.color = gizmoSq.IsReversing ? Color.blue : Color.red;
			Gizmos.DrawRay(_waypoints[current], (_waypoints[next] - _waypoints[current]) / 2.5f);
		}
	}
}
