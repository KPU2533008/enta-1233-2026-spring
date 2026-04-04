using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MenuBase {
	[SerializeField] private Transform _content;
	[SerializeField] private LevelSelectEntry _entryPrefab;

	public override GameMenus MenuType() {
		return GameMenus.LevelSelectMenu;
	}

	private void Start() {
		BuildEntries();
	}

	public void ButtonBack() {
		UIMgr.Instance.HideMenu(GameMenus.LevelSelectMenu);
	}

	void BuildEntries() {
		string[] levels = LevelMgr.Instance.LevelSceneNames;
		if ( levels.Length == 0 )
			return;
		for ( int i = 0; i < levels.Length; i++ ) {
			LevelSelectEntry entry = Instantiate(_entryPrefab, _content);
			entry.Setup(levels[i], i);
		}
	}
}
