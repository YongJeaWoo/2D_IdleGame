using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum E_DungeonType
{
    Coin,
    Ore,
    Capsule
}

[CreateAssetMenu(fileName = "Dungeon Scriptable", menuName = "Dungeon")]
public class DungeonData : ScriptableObject
{
    public Image iconImage;
    public Sprite icon;
    public Sprite dungeonObjSprite;
    public TextMeshProUGUI dungeon_InfoText;
    public Button enterButton;
    public string dungeonName;

    public float dungeonTimer;

    public string dungeonEnterSceneName;

    public E_DungeonType dungeonType;
    public GameObject dungeonObj;
}

