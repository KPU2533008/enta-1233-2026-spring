using UnityEngine;

public class Explosion : MonoBehaviour {

	[SerializeField] private int _damage = 4;

	public void Hit(Collider collider) {
		GameObject obj = collider.gameObject;
		IDamageReceiver receiver = obj.GetComponent<IDamageReceiver>();

		if ( receiver != null ) {
			HealthModifyInfo info = new HealthModifyInfo {
				Source = gameObject,
				Amount = _damage,
				HitPos = collider.transform.position,
				HitNormal = Vector3.up,
			};
			receiver.ReceiveDamage(info);
		}
	}

}
