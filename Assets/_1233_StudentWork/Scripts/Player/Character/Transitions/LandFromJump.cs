using Assets._1233_StudentWork.Scripts.FSM;
using Assets._1233_StudentWork.Scripts.PlayerCharacter.States;
using System;

namespace Assets._1233_StudentWork.Scripts.PlayerCharacter.Transitions {
	class LandFromJump : FSM_TransitionMeta<Character_MX02, PlayerCharacterInput> {
		new public static Type[] From => new Type[] { typeof(Jumping) };
		new public static Type To => typeof(Landed);

		public override bool Test(float deltaTime, in Character_MX02 character, in PlayerCharacterInput input) {
			return character.Controller.isGrounded && character.velocity.y <= 0;
		}
	}
}
