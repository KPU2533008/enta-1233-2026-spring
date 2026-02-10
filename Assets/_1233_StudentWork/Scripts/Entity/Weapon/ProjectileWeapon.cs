using UnityEngine;

public class ProjectileWeapon : MonoBehaviour, IWeapon {
	[SerializeField] private Projectile _projectilePrefab;
	[SerializeField] private Transform _muzzle;
	[SerializeField] private float _fireRate = 1f;
	[SerializeField] private float _arcHeight = 2f;

	private float _nextFireTime;

	public bool CanFire => Time.time >= _nextFireTime;

	public void Fire(Quaternion direction) {
		if ( !CanFire )
			return;
		_nextFireTime = Time.time + 1f / _fireRate;
		SpawnProjectile(direction);
	}

	public void Fire(Vector3 targetPosition) {
		// Direct fire by default, or could be configured for arc
		Vector3 direction = ( targetPosition - _muzzle.position ).normalized;
		Fire(Quaternion.LookRotation(direction));
	}

	public void FireArc(Vector3 targetPosition) {
		if ( !CanFire )
			return;
		_nextFireTime = Time.time + 1f / _fireRate;
		Vector3 velocity = CalculateArcVelocity(_muzzle.position, targetPosition, _arcHeight);
		Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, _muzzle.rotation);
		projectile.LaunchWithVelocity(velocity, gameObject);
	}

	private void SpawnProjectile(Quaternion direction) {
		Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, direction);
		projectile.Launch(direction * Vector3.forward, gameObject);
	}

	private Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float height) {
		float displacementY = end.y - start.y;
		Vector3 displacementXZ = new Vector3(end.x - start.x, 0, end.z - start.z);
		float gravity = Physics.gravity.y;

		float time =
			Mathf.Sqrt(-2 * height / gravity) +
			Mathf.Sqrt(2 * ( displacementY - height ) / gravity);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
		Vector3 velocityXZ = displacementXZ / time;

		return velocityXZ + velocityY * -Mathf.Sign(gravity);
	}
}
