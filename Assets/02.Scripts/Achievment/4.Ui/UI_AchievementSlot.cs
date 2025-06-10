using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public TextMeshProUGUI RewardClaimDateTextUI;
    public TextMeshProUGUI ProgressSliderTextUI;
    public Slider ProgressSlider;
    public Button ReawardClaimButton;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;

        NameTextUI.text = achievementDTO.Name;
        DescriptionTextUI.text = achievementDTO.Description;
        RewardCountTextUI.text = achievementDTO.RewardAmount.ToString();
        ProgressSlider.value = (float)achievementDTO.CurrentValue / achievementDTO.GoalValue;
        ProgressSliderTextUI.text = $"{achievementDTO.CurrentValue} / {achievementDTO.GoalValue}";

        ReawardClaimButton.interactable = achievementDTO.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // ¼º°ø ÀÌÆÑÆ®
        }
        else
        {
            // ½ÇÆÐ ÆË¾÷
        }
    }    
}