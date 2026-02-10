using System;
using UnityEngine;

namespace Assets._1233_StudentWork.Scripts.Player.Character {
	public class CharacterAudioController : MonoBehaviour {
		[SerializeField] private AudioSource _footstepSource;

		public void PlayFootstep() {
			_footstepSource?.Play();
		}
	}
}
