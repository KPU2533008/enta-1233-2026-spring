using UnityEngine;

namespace Assets._1233_StudentWork.Scripts.PlayerCharacter.States {
	class Dead : MX02StateBase {

		public override void OnEnter(float deltaTime, in Character_MX02 character, in PlayerCharacterInput input) {
			base.OnEnter(deltaTime, character, input);
			character.velocity.Scale(new Vector3(0, 1, 0));
		}

	}
}
