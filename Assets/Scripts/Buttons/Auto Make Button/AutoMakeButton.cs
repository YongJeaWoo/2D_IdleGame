using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoMakeButton : MonoBehaviour
{
    private Button myButton;
    private KnifeCollectionBar knifeBar;
    private TextMeshProUGUI toggleText;

    private CreateKnifeButton createButton;

    private Coroutine autoMakeCoroutine;

    private void Awake()
    {
        InitButton();
    }

    private void InitButton()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ToggleActiveButton);

        var obj = UIManager.Instance.gameObject;
        createButton = obj.GetComponentInChildren<CreateKnifeButton>();
        var division = obj.GetComponentInChildren<BottomDivisionComponent>();
        var function = division.GetFunctionBar();
        knifeBar = function.GetKnifeCollectBar();

        toggleText = GetComponentInChildren<TextMeshProUGUI>();
        toggleText.text = $"OFF";
    }

    public void ToggleActiveButton()
    {
        knifeBar.IsAutoPlay = !knifeBar.IsAutoPlay;
        toggleText.text = knifeBar.IsAutoPlay ? $"ON" : $"OFF";

        if (knifeBar.IsAutoPlay)
        {
            autoMakeCoroutine = StartCoroutine(AutoCreatedButtonCoroutine());
        }
        else
        {
            if (autoMakeCoroutine != null)
            {
                StopCoroutine(autoMakeCoroutine);
                autoMakeCoroutine = null;
            }
        }
    }

    private IEnumerator AutoCreatedButtonCoroutine()
    {
        while (knifeBar.IsAutoPlay)
        {
            yield return new WaitForSeconds(2f);

            createButton.CreateButton();
            yield return null;
        }
    }
}
