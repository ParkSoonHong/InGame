
using System;


[Serializable]
public class AttendanceReward 
{
    public  int DayIndex;
    public  ECurrencyType RewardCurrencyType;
    public  int RewardAmount;
    public  bool RewardClaimed;
}
