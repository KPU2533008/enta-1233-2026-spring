using UnityEngine;
/// <summary>
/// In game HUD shown when not paused
/// </summary>
public class GameUI : MenuBase {

	[SerializeField] private int _boundPlayerIdx;
    [SerializeField] private HealthMeter _healthMeter;

    public override GameMenus MenuType() {
        return GameMenus.InGameUI;
    }

	private void OnEnable() {
		if ( PlayerService.Instance.GetPlayers()[0] != null ) {
			OnPlayerAdded(PlayerService.Instance.GetPlayers()[0], 0);
		}
		PlayerService.Instance.PlayerAdded += OnPlayerAdded;
	}

	private void OnDisable() {
		PlayerService.Instance.PlayerAdded -= OnPlayerAdded;
	}

	private void OnCharacterAdded(Character character) {
		Health health = character.GetComponent<Health>();
		if ( health != null ) {
			_healthMeter.BindToHealth(health);
		}
	}

	private void OnPlayerAdded(Player player, int index) {
		if ( index != _boundPlayerIdx )
			return;

		player.CharacterAdded += OnCharacterAdded;
		if ( player.Character != null )
			OnCharacterAdded(player.Character);
	}
}
