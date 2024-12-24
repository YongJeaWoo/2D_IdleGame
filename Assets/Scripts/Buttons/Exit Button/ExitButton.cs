using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private readonly string ExitText = $"Exit Panel";
    private Button settingButton;

    private void Start()
    {
        settingButton = GetComponent<Button>();
        settingButton.onClick.AddListener(OnSetting);
    }

    public void OnSetting()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        PopupManager.Instance.InstantPopup(ExitText);
    }
}
