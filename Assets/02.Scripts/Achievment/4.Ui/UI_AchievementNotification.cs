using DG.Tweening;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementNotification : MonoBehaviour
{
    [SerializeField] private float dropDistance = 100f;        // �Ʒ��� ������ �Ÿ�
    [SerializeField] private float moveDuration = 2f;          // �Ʒ��� �������� �ð�
    [SerializeField] private float disappearHeight = 150f;     // ����� �� �ö󰡴� ����
    [SerializeField] private float disappearDuration = 2f;     // ������� �� �ɸ��� �ð�

    [SerializeField] private RectTransform _rectTransform;     // �� UI�� RectTransform
    [SerializeField] private CanvasGroup _canvasGroup;         // ���̵� �ƿ��� ���� CanvasGroup

    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;

    private AchievementDTO _achievementDTO;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
       // _canvasGroup = GetComponent<CanvasGroup>();
       
    }

    private void Start()
    {
        AchievementManager.Instance.OnAchievementDone += Done;
        gameObject.SetActive(false);
    }
    // ������ ��ȯ 
    public void Done(AchievementDTO achievementDTO)
    {
        gameObject.SetActive(true);
        NameTextUI.text = achievementDTO.Name;
        DescriptionTextUI.text = achievementDTO.Description;
        MoveAndDisappear();
    }

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;
        NameTextUI.text = achievementDTO.Name;
        DescriptionTextUI.text = achievementDTO.Description;
    }

    void MoveAndDisappear()
    {
        Vector3 startPosition = _rectTransform.position;
        Vector3 dropPosition = startPosition + Vector3.down * dropDistance;

        // 1. ���� ��ġ���� �Ʒ��� dropDistance��ŭ 2�ʰ� �̵�
        _rectTransform.DOMove(dropPosition, moveDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                // 2. ���� disappearHeight��ŭ 2�ʰ� �̵��ϸ� ���̵� �ƿ�
                var disappearSequence = DOTween.Sequence();
                disappearSequence.Append(_rectTransform.DOMoveY(_rectTransform.position.y + disappearHeight, disappearDuration));
                //disappearSequence.Join(_canvasGroup.DOFade(0f, disappearDuration));
                disappearSequence.SetEase(Ease.InCubic);
                disappearSequence.OnComplete(() =>
                {
                    gameObject.SetActive(false); // �Ǵ� Destroy(gameObject);
                });
            });
    }
}
