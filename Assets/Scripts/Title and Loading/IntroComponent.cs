using UnityEngine;

public class IntroComponent : MonoBehaviour
{
    [SerializeField] private string startButtonSceneName;
    public void StartButtonClick()
    {
        LoadingComponent.LoadScene(startButtonSceneName);
    }
}
