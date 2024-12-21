using System.Numerics;
using TMPro;
using UnityEngine;

public abstract class DropPossessItem : MonoBehaviour
{
    protected TextMeshProUGUI PossessText;
    
    protected PlayerPossessionsController possessionsController;

    [SerializeField] protected string dropItemName;
    [SerializeField] protected string dropAmountText;
    protected BigInteger dropAmount;
    
    protected void OnEnable()
    {
        InitPossessSet();
    }

    protected virtual void Start()
    {
        InitializePossessionsController();
    }

    private void InitializePossessionsController()
    {
        var playerManager = PlayerManager.Instance;
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager를 찾을 수 없음");
            return;
        }

        if (playerManager.GetPlayer() != null)
        {
            possessionsController = playerManager.GetPossessionsController();
            InitPossess();  
        }
        else
        {
            playerManager.OnPlayerReady += OnPlayerReadyCallback;
        }
    }

    private void OnPlayerReadyCallback()
    {
        PlayerManager.Instance.OnPlayerReady -= OnPlayerReadyCallback;
        possessionsController = PlayerManager.Instance.GetPossessionsController();
        InitPossess();
    }

    public virtual void DropItem()
    {
        if (possessionsController == null || string.IsNullOrEmpty(dropItemName))
        {
            Debug.LogError("PossessionsController가 초기화되지 않았거나 드롭 아이템 이름이 비어 있습니다.");
            return;
        }

        BigInteger currentAmount = possessionsController.LoadPossess(dropItemName);
        BigInteger updatedAmount = currentAmount + dropAmount;
        possessionsController.SavePossess(dropItemName, updatedAmount);
    }

    public abstract void InitPossessSet();
    protected abstract void InitPossess();
}
