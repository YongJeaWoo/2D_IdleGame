using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Game/Round Data")]
public class RoundData : ScriptableObject
{
    private int savedRound = 1;

    public void SaveRound(int round)
    {
        savedRound = round;
    }

    public int LoadRound()
    {
        return savedRound;
    }
}
