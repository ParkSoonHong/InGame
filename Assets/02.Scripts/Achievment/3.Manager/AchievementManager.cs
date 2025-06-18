using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField]
    private List<AchievementSo> _metaDatas;

    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((a) => new AchievementDTO(a));

    public event Action OnDataChanged;

    public event Action<AchievementDTO> OnAchievementDone;

    private AchievmentRepository _repository;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _achievements = new List<Achievement>();
        _repository = new AchievmentRepository();

        List<AchievementSaveData> LoadedAchievments = _repository.Load();

        foreach(var metaData in _metaDatas)
        {
            Achievement duplicatedAchievment = FindById(metaData.Id);
            if (duplicatedAchievment != null)
            {
                throw new Exception($"업적 ID{metaData.Id}가 중복됩니다.");
            }

            AchievementSaveData saveData = LoadedAchievments?.Find(a => a.Id == metaData.Id) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }

    }

    private AchievementDTO FindByDTOId(string id)
    {
        return Achievements.Find(a => a.Id == id);
    }

    private Achievement FindById(string id)
    {
        return _achievements.Find(a => a.Id == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                if(achievement.TryDone(value))
                {
                    OnAchievementDone?.Invoke(FindByDTOId(achievement.Id));
                }
            }
        }

        OnDataChanged?.Invoke();
        _repository.Save(Achievements);
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = FindById(achievementDto.Id);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);

            OnDataChanged?.Invoke();
            _repository.Save(Achievements);
            return true;
        }

        return false;
    }
}
