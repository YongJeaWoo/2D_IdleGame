using UnityEngine;

public class RoundController : MonoBehaviour
{
    private void OnEnable()
    {
        LevelManager.Instance.OnRoundChange += RoundChanging;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnRoundChange -= RoundChanging;
    }

    private void Start()
    {
        RoundChanging();
    }

    private void RoundChanging()
    {
        var roundText = UIManager.Instance.GetRoundText();
        var currentRound = LevelManager.Instance.GetCurrentRound();

        roundText.text = currentRound.ToString();
    }
}
