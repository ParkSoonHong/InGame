using UnityEngine;

// 런타임시 변하지 않는값을 SO로 관리하면:
// - 기회자가 에디터에서 직접 수정이 가능하다.
// - 유지보수와 확장성이 증가한다.
// - 도메인 객체(Achievement)는 상태(CurrentValue, isClaimend)만 관리한다.

[CreateAssetMenu(fileName = "AchievementSo", menuName = "Scriptable Objects/AchievementSo")]
public class AchievementSo : ScriptableObject
{
    [SerializeField]
    private string _id;
    public string Id => _id;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private string _description;
    public string Description => _description;

    [SerializeField]
    private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;

    [SerializeField]
    private int _goalValue;
    public int GoalValue => _goalValue;

    [SerializeField]
    private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;

    [SerializeField]
    private int _rewardAmount;
    public int RewardAmount => _rewardAmount;
}
