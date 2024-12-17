using SingletonBase.DontDestroySingleton;
using UnityEngine;

public class DungeonDataManager : SingletonBase<DungeonDataManager>
{
    [SerializeField] private DungeonData currentDungeonData;

    public void SetDungeonData(DungeonData data)
    {
        currentDungeonData = data;
        UpdateDungeonInfo();
    }

    private void UpdateDungeonInfo()
    {
        if (currentDungeonData == null) return;

        var roundText = UIManager.Instance.GetRoundText();

        if (roundText != null)
        {
            roundText.text = currentDungeonData.dungeonName;
        }
    }
}