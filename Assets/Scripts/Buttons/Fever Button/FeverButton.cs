using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeverButton : MonoBehaviour
{
    [Header("Fever ��Ÿ��")]
    [SerializeField] private float feverCool;

    [Header("fever ��Ÿ�� �̹���")]
    [SerializeField] private Image coolImage;

    [Header("fever ���ǵ� ���� ���")]
    [SerializeField] private float multipleSpeed;

    [Header("fever ���� �ð�")]
    [SerializeField] private float feverTime;

    private Button button;
    private PlayerSystem playerSystem;
    private float timer;

    private void Start()
    {
        playerSystem = FindObjectOfType<PlayerSystem>();
        button = GetComponent<Button>();
        ResetCooldown();
    }

    private void ResetCooldown()
    {
        timer = feverCool;
        coolImage.fillAmount = 0;
        button.interactable = true;
    }

    public void OnFeverButtonClick()
    {
        if (timer < feverCool) return;

        var player = playerSystem.GetPlayer();
        var speed = player.GetComponent<SpeedComponent>();
        speed.SpeedUp(multipleSpeed, feverTime);

        button.interactable = false;
        Invoke(nameof(StartCoolCoroutine), feverTime);
    }

    private void StartCoolCoroutine()
    {
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        timer = 0;
        button.interactable = false;

        while (timer < feverCool)
        {
            timer += Time.deltaTime;
            coolImage.fillAmount = 1 - (timer / feverCool);
            yield return null;
        }

        ResetCooldown();
    }
}
