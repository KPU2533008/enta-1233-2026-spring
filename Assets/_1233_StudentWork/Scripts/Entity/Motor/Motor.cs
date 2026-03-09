using UnityEngine;

[RequireComponent(typeof(IMover))]
public abstract class Motor : MonoBehaviour {

	protected IMover _mover;

	private void Start() {
		_mover = GetComponent<IMover>();
	}

}