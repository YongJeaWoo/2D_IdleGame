using TMPro;
using UnityEngine;

public class KnifePossess : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;

    private KnifeCollectionBar knifeBar;

    private void OnEnable()
    {
        KnifeUIActivator.OnMerge += ChangeText;
        KnifeCollectionBar.OnUpdateKnife += ChangeText;
        CreateKnifeButton.OnCreateButton += ChangeText;
    }

    private void OnDisable()
    {
        KnifeUIActivator.OnMerge -= ChangeText;
        KnifeCollectionBar.OnUpdateKnife -= ChangeText;
        CreateKnifeButton.OnCreateButton -= ChangeText;
    }

    private void Awake()
    {
        FindFunctionBar();
        ChangeText();
    }

    private void FindFunctionBar()
    {
        BottomDivisionComponent division = UIManager.Instance.GetComponentInChildren<BottomDivisionComponent>();
        var functionBar = division.GetFunctionBar();
        knifeBar = functionBar.GetKnifeCollectBar();
    }

    private void ChangeText()
    {
        myText.text = $"{knifeBar.GetCreatedCurrentCount()} / {knifeBar.GetCreatedMaxCount()}";
    }
}
