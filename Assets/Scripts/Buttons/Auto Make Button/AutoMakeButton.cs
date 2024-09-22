using System.Collections;
using UnityEngine;

public class AutoMakeButton : AutoTextButton
{
    private CreateKnifeButton createButton;
    private Coroutine autoMakeCoroutine;

    protected override void Start()
    {
        base.Start();
        GetComponents();
    }

    private void GetComponents()
    {
        createButton = UIManager.Instance.gameObject.GetComponentInChildren<CreateKnifeButton>();
    }

    public override void ToggleActiveButton()
    {
        base.ToggleActiveButton();

        if (isAutoPlay)
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
        while (isAutoPlay)
        {
            yield return new WaitForSeconds(2f);

            createButton.CreateButton();
            yield return null;
        }
    }
}
