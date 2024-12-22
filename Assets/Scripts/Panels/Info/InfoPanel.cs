using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI insideInfoText;

    public string SetInfoText(string value) => infoText.text = value;
    public string SetInsideInfoText(string value) => insideInfoText.text = value;
}
