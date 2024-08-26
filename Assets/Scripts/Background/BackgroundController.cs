using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private ParallaxBackground[] bgObjects;

    private void Start()
    {
        SetValue();
    }

    private void SetValue()
    {
        bgObjects = FindObjectsOfType<ParallaxBackground>();
    }

    public void BG_Controll(bool isAttack)
    {
        foreach(var bgObject in bgObjects)
        { 
            if(bgObject.GetOption() == BG_OPTION.ON)
            {
                bgObject.PlayerAttackToSpeed(isAttack);
            }
        }
    }
}
