using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshAgentMover {
	[SerializeField] private NavMeshAgent _agent;

	public Vector3 Velocity => _agent.velocity;
	public bool HasPath => _agent.hasPath;

	public void SetDestination(Vector3 worldPos) {
		_agent?.SetDestination(worldPos);
	}

	public void Stop() {
		_agent?.ResetPath();
	}
}
