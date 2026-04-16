using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBase : MonoBehaviour {

	[SerializeField] private bool _reversible;
	[SerializeField] private UnityEvent<bool> OnTriggerStateChanged;
	private bool isTriggered = false;

	protected void Trigger(bool active) {
		if ( active == isTriggered )
			return;

		if ( !active && !_reversible )
			return;

		isTriggered = active;
		OnTriggerStateChanged?.Invoke(active);
	}

}