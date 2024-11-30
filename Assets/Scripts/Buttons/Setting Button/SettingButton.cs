using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    private readonly string SettingText = $"Setting Panel";
    private Button settingButton;

    private void Start()
    {
        settingButton = GetComponent<Button>();
        settingButton.onClick.AddListener(OnSetting);
    }

    public void OnSetting()
    {
        PopupManager.Instance.InstantPopup(SettingText);
    }
}
