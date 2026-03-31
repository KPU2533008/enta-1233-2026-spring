using System.Collections;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour {

	[SerializeField] private ParticleSystem[] _particleSystems;

	//private void OnEnable() {
	//	StartCoroutine(PlayAndCleanup());
	//}

	public void Emit() {
		StartCoroutine(PlayAndCleanup());
	}

	private IEnumerator PlayAndCleanup() {
		for ( int i = 0; i < _particleSystems.Length; i++ ) {
			_particleSystems[i].Play();
		}
		for ( int i = 0; i < _particleSystems.Length; i++ ) {
			yield return new WaitUntil(() => !_particleSystems[i].IsAlive(true));
		}
		Destroy(gameObject);
	}

}