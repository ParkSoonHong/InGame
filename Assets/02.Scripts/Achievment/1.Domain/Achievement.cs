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
    //  ���� ��ǥ : �ڱ� ������

    // ������
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // ����
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    // ������
    public Achievement(string id, string name, string description, EAchievementCondition condition, int goalValue, ECurrencyType rewardCurrencyType, int rewardAmount)
    {

        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("���� ID�� ������� �� �����ϴ�.");
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("���� �̸��� ������� �� �����ϴ�.");
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new Exception("���� ������ ������� �� �����ϴ�.");
        }
        if (goalValue <= 0)
        {
            throw new Exception("���� ��ǥ ���� 0���� Ŀ���մϴ�.");
        }
        if (rewardAmount <= 0)
        {
            throw new Exception("���� ���� ���� 0���� Ŀ���մϴ�.");
        }

        Id = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }

    // ������
    public Achievement(AchievementSo MetaData , AchievementSaveData loadedData)
    {
        if (string.IsNullOrEmpty(MetaData.Id))
        {
            throw new Exception("���� ID�� ������� �� �����ϴ�.");
        }
        if (string.IsNullOrEmpty(MetaData.Name))
        {
            throw new Exception("���� �̸��� ������� �� �����ϴ�.");
        }
        if (string.IsNullOrEmpty(MetaData.Description))
        {
            throw new Exception("���� ������ ������� �� �����ϴ�.");
        }
        if (MetaData.GoalValue <= 0)
        {
            throw new Exception("���� ��ǥ ���� 0���� Ŀ���մϴ�.");
        }
        if (MetaData.RewardAmount <= 0)
        {
            throw new Exception("���� ���� ���� 0���� Ŀ���մϴ�.");
        }
        if(loadedData.CurrentValue < 0)
        {
            throw new Exception("���� ���� ���� 0���� Ŀ���մϴ�.");
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
            throw new Exception("���� ���� 0���� Ŀ���մϴ�.");
        }

        _currentValue += value;
    }

    public bool TryDone(int value)
    {
        if(value <= 0)
        {
            throw new Exception("���� ���� 0���� Ŀ���մϴ�.");
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
