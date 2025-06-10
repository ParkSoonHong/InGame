using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class AchievmentRepository
{
    private const string SAVE_KEY = nameof(AchievmentRepository);

   public void save(List<AchievementDTO> achievements)
    {
        AchievementSaveDataList datas = new AchievementSaveDataList();
        datas.DataList = achievements.ConvertAll(achievement => new AchievementSaveData
        {
            Id = achievement.Id,
            CurrentValue = achievement.CurrentValue,
            RewardClaimed = achievement.RewardClaimed
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementSaveData> Load ()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDataList datas = JsonUtility.FromJson<AchievementSaveDataList>(json);

        return datas.DataList;
    }
}



[Serializable]
public struct AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;
}
