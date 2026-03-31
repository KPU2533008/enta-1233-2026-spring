using UnityEngine;

public class PressurePlate : MonoBehaviour {
	public void PressDown(bool pressed) {
		gameObject.transform.position += new Vector3(0, 0.05f * (pressed ? -1 : 1), 0);
	}
}
