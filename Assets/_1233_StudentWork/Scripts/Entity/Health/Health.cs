using System;
using UnityEngine;

public struct HealthModifyInfo {
	public GameObject Source;
	public int Amount;
	public bool CanResurrect;

	public Vector3 HitPos;
	public Vector3 HitNormal;
	// We can add DamageType here later if needed
}

public class Health : MonoBehaviour {
	[SerializeField] private int _maxHealth = 100;
	[SerializeField] private bool _isInvulnerable;

	public int Current { get; private set; }
	public int Max => _maxHealth;
	public float Alpha => _maxHealth <= 0 ? 0f : (float)Current / _maxHealth;
	public bool IsDead { get; private set; }

	void Awake() {
		ResetHealth();
	}

	public event Action<HealthModifyInfo> Damaged;
	public event Action Died;
	public event Action Healed;
	public event Action Reset;
	public event Action<Health> Changed;

	public void ResetHealth() {
		Current = _maxHealth;
		IsDead = false;
		Reset?.Invoke();
		Changed?.Invoke(this);
	}

	public void TakeDamage(HealthModifyInfo info) {
		if ( IsDead || _isInvulnerable )
			return;

		Current = Math.Max(Current - info.Amount, 0);
		Damaged?.Invoke(info);
		Changed?.Invoke(this);

		if ( Current <= 0 )
			Die();
	}

	public void Heal(int amount) {
		if ( IsDead )
			return;

		Current = Math.Min(Current + amount, _maxHealth);
		Healed?.Invoke();
		Changed?.Invoke(this);
	}

	private void Die() {
		IsDead = true;
		Died?.Invoke();
	}

	public void SetInvulnerable(bool invulnerable) {
		_isInvulnerable = invulnerable;
	}

}
