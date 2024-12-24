using UnityEngine;

public class DungeonController : MonoBehaviour
{
    private void Start()
    {
        InitDungeonStart();
    }

    private void InitDungeonStart()
    {
        PlayerManager.Instance.FindPlayer();
        var canvasObj = UIManager.Instance.GetGUICanvas();
        var canvas = canvasObj.GetComponent<Canvas>();
        var uiCamObj = GameObject.FindWithTag("UI Camera");
        var uiCam = uiCamObj.GetComponent<Camera>();
        canvas.worldCamera = uiCam;
        DungeonDataManager.Instance.StartDungeonTime();
    }
}
