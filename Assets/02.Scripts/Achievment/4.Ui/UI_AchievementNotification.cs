using DG.Tweening;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementNotification : MonoBehaviour
{
    [SerializeField] private float dropDistance = 100f;        // 아래로 내려갈 거리
    [SerializeField] private float moveDuration = 2f;          // 아래로 내려오는 시간
    [SerializeField] private float disappearHeight = 150f;     // 사라질 때 올라가는 높이
    [SerializeField] private float disappearDuration = 2f;     // 사라지는 데 걸리는 시간

    [SerializeField] private RectTransform _rectTransform;     // 이 UI의 RectTransform
    [SerializeField] private CanvasGroup _canvasGroup;         // 페이드 아웃을 위한 CanvasGroup

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
    // 생각에 전환 
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

        // 1. 생성 위치에서 아래로 dropDistance만큼 2초간 이동
        _rectTransform.DOMove(dropPosition, moveDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                // 2. 위로 disappearHeight만큼 2초간 이동하며 페이드 아웃
                var disappearSequence = DOTween.Sequence();
                disappearSequence.Append(_rectTransform.DOMoveY(_rectTransform.position.y + disappearHeight, disappearDuration));
                //disappearSequence.Join(_canvasGroup.DOFade(0f, disappearDuration));
                disappearSequence.SetEase(Ease.InCubic);
                disappearSequence.OnComplete(() =>
                {
                    gameObject.SetActive(false); // 또는 Destroy(gameObject);
                });
            });
    }
}
