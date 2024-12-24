using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        InitGameStart();
    }

    private void InitGameStart()
    {
        PlayerManager.Instance.FindPlayer();
        var canvasObj = UIManager.Instance.GetGUICanvas();
        var canvas = canvasObj.GetComponent<Canvas>();
        var uiCamObj = GameObject.FindWithTag("UI Camera");
        var uiCam = uiCamObj.GetComponent<Camera>();
        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = uiCam;
        }
    }
}
