using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshAgentMover : MonoBehaviour {
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Vector3 _debugDestination;

	public Vector3 Velocity => _agent.velocity;
	public bool HasPath => _agent.hasPath;

	public void SetDestination(Vector3 worldPos) {
		_agent?.SetDestination(worldPos);
	}

	public void Start() {
		SetDestination(_debugDestination);
	}

	public void Stop() {
		_agent?.ResetPath();
	}
}
