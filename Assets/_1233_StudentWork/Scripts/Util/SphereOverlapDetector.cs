using UnityEngine;
using UnityEngine.Events;

public class SphereOverlapDetector : MonoBehaviour {

    [SerializeField] private float _radius = 5f;
    [SerializeField] private bool _requiresLineOfSight;
    [SerializeField] private int _maxHits = 32;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnityEvent<Collider> _onDetected;

    private Collider[] _results;

    void Awake() {
        _results = new Collider[_maxHits];
    }

    public void Detect() {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _layerMask);

        for ( int i = 0; i < count; i++ ) {
            Collider collider = _results[i];

            if ( _requiresLineOfSight ) {
                Vector3 from = transform.position;
                Vector3 to = collider.gameObject.transform.position;
                bool hit = Physics.Raycast(from, to - from, Vector3.Distance(from, to), _layerMask);
                if ( hit )
                    continue;
            }

            _onDetected?.Invoke(collider);
        }
    }

	private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
	}
}
