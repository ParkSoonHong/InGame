using UnityEngine;

// ��Ÿ�ӽ� ������ �ʴ°��� SO�� �����ϸ�:
// - ��ȸ�ڰ� �����Ϳ��� ���� ������ �����ϴ�.
// - ���������� Ȯ�强�� �����Ѵ�.
// - ������ ��ü(Achievement)�� ����(CurrentValue, isClaimend)�� �����Ѵ�.

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
