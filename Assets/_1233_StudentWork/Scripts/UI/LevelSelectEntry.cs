using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectEntry : MonoBehaviour {
	[SerializeField] private Button _button;
	[SerializeField] private TMP_Text _levelNameText;

	private int _levelIndex;

	private void Awake() {
		if ( _button == null )
			_button = GetComponent<Button>();
	}

	public void Setup(string level, int levelIdx) {
		_levelIndex = levelIdx;
		if ( _levelNameText != null )
			_levelNameText.text = level;
	}

	public void Pressed() {
		LevelMgr.Instance.SetCurrentLevel(_levelIndex);
		SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
	}
}
