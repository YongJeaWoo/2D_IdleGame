using UnityEngine;
using UnityEngine.UI;

public class StartButtonComponent : MonoBehaviour
{
    private readonly string startButtonSceneName = $"GameScene";

    private Button myButton;

    private void Start()
    {
        StartButtonClick();
    }

    private void StartButtonClick()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(GoToGameScene);
    }

    private void GoToGameScene()
    {
        LoadingComponent.LoadScene(startButtonSceneName);
    }
}
