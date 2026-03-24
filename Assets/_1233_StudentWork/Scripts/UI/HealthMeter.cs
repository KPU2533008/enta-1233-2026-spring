using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour {

	[SerializeField] private Heart _heartPrefab;

	private HorizontalLayoutGroup layout;
	private List<Heart> _hearts = new();

	private void Awake() {
		layout = GetComponent<HorizontalLayoutGroup>();
	}

	public void BindToHealth(Health health) {
		DestroyHearts();

		int numHearts = health.Max / 4;

		for ( int i = 0; i < numHearts; i++ ) {
			Heart heart = Instantiate(_heartPrefab);
			heart.name = $"Heart{i}";
			heart.transform.parent = gameObject.transform;
			heart.SetStartHpIndex(i * 4);
			heart.BindToHealth(health);
			_hearts.Add(heart);
		}

		if ( layout != null ) {
			layout.childControlHeight = false;
			layout.childControlHeight = true;
			layout.childScaleWidth = false;
			layout.childScaleWidth = true;
		}
	}

	public void DestroyHearts() {
		foreach ( Heart heart in _hearts ) {
			Destroy(heart.gameObject);
		}
		_hearts.Clear();
	}

}
