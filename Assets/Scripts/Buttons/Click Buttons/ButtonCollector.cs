using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCollector : MonoBehaviour
{
    private ClickButtonComponent currentSelectedButton;

    [SerializeField] private FunctionBarComponent functionBar;

    public void OnButtonSelected(ClickButtonComponent button)
    {
        if (currentSelectedButton != null && currentSelectedButton != button)
        {
            currentSelectedButton.CloseTargetPanel();
            currentSelectedButton.DeselectButton();
        }

        currentSelectedButton = button;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.CloseTargetPanel();
            currentSelectedButton.DeselectButton();
            functionBar.ActiveObjectKnifeUIObject();
            currentSelectedButton = null;
        }
    }
}
