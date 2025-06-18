using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceRewoardSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private TextMeshProUGUI _rewardDayText;
    [SerializeField] private Button _rewardButton;
    //  [SerializeField] private Image _rewardImage;

    public void Refresh(AttendanceReward attendanceReward)
    {
        _rewardAmountText.text = attendanceReward.RewardAmount.ToString();
        _rewardDayText.text = attendanceReward.DayIndex.ToString();
   
    }
    public void GetReward()
    {
    }
}
