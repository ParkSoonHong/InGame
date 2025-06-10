public class AchievementDTO
{
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;
    public readonly int CurrentValue;
    public readonly bool RewardClaimed;

    public AchievementDTO(string id, int currentValue, bool rewardClaimed)
    {
        Id = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    public AchievementDTO(string id, string name, string description,EAchievementCondition condition,int goalValue,
                          ECurrencyType rewardCurrencyType,int rewardAmount,int currentValue,bool rewardClaimed)
    {
        Id = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    public AchievementDTO(Achievement achievement)
    {
        Id = achievement.Id;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return CurrentValue >= GoalValue;
    }

}
