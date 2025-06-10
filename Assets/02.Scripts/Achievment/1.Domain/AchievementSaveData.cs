using System;
using UnityEngine;

[Serializable]
public struct AchievementSaveData 
{
    public string Id;
    public int CurrentValue;
    public bool RewardClaimed;
}
