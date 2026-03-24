using System;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

	[SerializeField] private Image _fillImage;
	[SerializeField] private Image _trailImage;

	private Health _health;
	private int _startHpIndex = 0;
	private int _lastHp = 0;

	private float _fill = 0f;
	private float _trail = 0f;
	private float _animateDelay = 0f;

	public void SetStartHpIndex(int index) {
		_startHpIndex = index;
	}

	public void BindToHealth(Health health) {
		if ( _health != null )
			_health.Changed -= OnHealthChanged;

		_health = health;
		_health.Changed += OnHealthChanged;
		_lastHp = _health.Current;
		_fill = _health.Current;
		_trail = _health.Current;
		UpdateImages();
	}

	private void OnHealthChanged(Health health) {
		_animateDelay = 1f;
		if ( health.Current < _lastHp ) {
			_fill = health.Current;
		} else {
			_trail = health.Current;
		}
		_lastHp = health.Current;
		UpdateImages();
	}

	private void UpdateImages() {
		_fillImage.fillAmount = Mathf.Clamp(( _fill - _startHpIndex ) / 4, 0, 1);
		_trailImage.fillAmount = Mathf.Clamp(( _trail - _startHpIndex ) / 4, 0, 1);
	}

	private void Update() {
		float dt = Time.deltaTime;

		if ( _animateDelay > 0 ) {
			_animateDelay -= dt;
			return;
		}

		if ( Mathf.Abs(_health.Current - _fill) > dt )
			_fill += Mathf.Sign(_health.Current - _fill) * dt * 12;
		else
			_fill = _health.Current;

		if ( Mathf.Abs(_health.Current - _trail) > dt )
			_trail += Mathf.Sign(_health.Current - _trail) * dt * 12;
		else
			_trail = _health.Current;

		UpdateImages();
	}

	private void OnDestroy() {
		if ( _health != null )
			_health.Changed -= OnHealthChanged;
	}

}