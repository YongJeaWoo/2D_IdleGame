using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingComponent : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private static string targetSceneName;

    public static void LoadScene(string sceneName)
    {
        targetSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadStageAsync(targetSceneName));
    }

    private IEnumerator LoadStageAsync(string sceneName)
    {
        yield return new WaitForEndOfFrame();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float fakeProgress = 0f;
        float loadingSpeed = 0.6f;

        while (!asyncLoad.isDone)
        {
            float targetProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            while (fakeProgress < targetProgress)
            {
                fakeProgress += Time.unscaledDeltaTime * loadingSpeed;
                fillImage.fillAmount = fakeProgress;
                yield return null;
            }

            if (asyncLoad.progress >= 0.9f)
            {
                fillImage.fillAmount = 1f;
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
