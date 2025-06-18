using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
}


public class Achievement 
{
    //  최종 목표 : 자기 서술형

    // 데이터
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // 상태
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    // 생성자
    public Achievement(string id, string name, string description, EAchievementCondition condition, int goalValue, ECurrencyType rewardCurrencyType, int rewardAmount)
    {

        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }
        if (goalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }
        if (rewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }

        Id = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }

    // 생성자
    public Achievement(AchievementSo MetaData , AchievementSaveData loadedData)
    {
        if (string.IsNullOrEmpty(MetaData.Id))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(MetaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(MetaData.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }
        if (MetaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }
        if (MetaData.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }
        if(loadedData.CurrentValue < 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }
     
        Id = MetaData.Id;
        Name = MetaData.name;
        Description = MetaData.Description;
        Condition = MetaData.Condition;
        GoalValue = MetaData.GoalValue;
        RewardCurrencyType = MetaData.RewardCurrencyType;
        RewardAmount = MetaData.RewardAmount;

        _currentValue = loadedData.CurrentValue;
        _rewardClaimed = loadedData.RewardClaimed;
    }

    public void Increase (int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }

        _currentValue += value;
    }

    public bool TryDone(int value)
    {
        if(value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }

        _currentValue += value;
        if(_currentValue >= GoalValue)
        {
            return true;
        }

        return false;
    }



    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if(!CanClaimReward())
        {
            return false;
        }
        _rewardClaimed = true;
        return true;
    }
}
