using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : Singleton<PlayerService> {

	[SerializeField] private Player _playerPrefab;
	private List<Player> players = new();

	public event Action<Player, int> PlayerAdded;
	public event Action<Player, int> PlayerRemoved;

	public Player AddPlayer() {
		Player player = Instantiate(_playerPrefab);
		DontDestroyOnLoad(player);
		players.Add(player);
		PlayerAdded?.Invoke(player, players.IndexOf(player));
		return player;
	}

	public void RemovePlayer(Player player) {
		int idx = players.IndexOf(player);
		players.Remove(player);
		PlayerRemoved?.Invoke(player, idx);
	}

	public Player[] GetPlayers() {
		return players.ToArray();
	}

	public Player? GetPlayerFromCharacter(Character character) {
		foreach ( Player player in players ) {
			if ( player.Character.gameObject == character.gameObject )
				return player;
		}
		return null;
	}

	public override void Awake() {
		base.Awake();
		AddPlayer();
	}

}
