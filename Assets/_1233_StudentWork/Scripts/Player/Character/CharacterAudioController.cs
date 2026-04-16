using System;
using UnityEngine;

namespace Assets._1233_StudentWork.Scripts.Player.Character {
	public class CharacterAudioController : MonoBehaviour {
		[SerializeField] private AudioSource[] _footstepSounds;
		[SerializeField] private AudioSource _swordSwingSource;
		[SerializeField] private AudioSource[] _hurtSounds;

		public void PlayFootstepAudio() {
			System.Random rng = new();
			int i = rng.Next(0, _footstepSounds.Length);
			_footstepSounds[i]?.Play();
		}

		public void PlayJumpAudio() { }

		public void PlayLandAudio() { }

		public void PlayHurtAudio() {
			System.Random rng = new();
			int i = rng.Next(0, _hurtSounds.Length);
			_hurtSounds[i]?.Play();
		}

		public void PlaySwordAudio() {
			_swordSwingSource?.Play();
		}
	}
}
