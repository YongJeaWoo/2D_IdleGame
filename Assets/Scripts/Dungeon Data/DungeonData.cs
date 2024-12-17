using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dungeon Scriptable", menuName = "Dungeon")]
public class DungeonData : ScriptableObject
{
    public Image iconImage;
    public Sprite icon;
    public TextMeshProUGUI dungeon_InfoText;
    public Button enterButton;
    public string dungeonName;

    public string dungeonEnterSceneName;
}

