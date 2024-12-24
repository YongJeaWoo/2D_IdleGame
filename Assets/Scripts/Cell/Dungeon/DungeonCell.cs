using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonCell : MonoBehaviour
{
    [SerializeField] protected DungeonData dungeonData;

    protected virtual void Awake()
    {
        InitInfoSetting();
    }

    private void InitInfoSetting()
    {
        dungeonData.iconImage = transform.GetChild(0).GetComponent<Image>();
        dungeonData.iconImage.sprite = dungeonData.icon;
        dungeonData.dungeon_InfoText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        dungeonData.dungeon_InfoText.text = dungeonData.dungeonName;
        dungeonData.enterButton = transform.GetChild(1).GetComponent<Button>();
        dungeonData.enterButton.onClick.AddListener(EnterDungeon);
    }

    protected void EnterDungeon()
    {
        DungeonDataManager.Instance.SetDungeonData(dungeonData);
        LevelManager.Instance.SaveCurrentRound();
        LoadingComponent.LoadScene(dungeonData.dungeonEnterSceneName);
    }
}