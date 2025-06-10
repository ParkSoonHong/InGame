using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 아키텍쳐 : 설계 그 잡채(설계마다 철학이 있다.)
// 다지인 패턴 : 설계를 구현하는 과정에서 쓰이는 패턴
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;

    private CurrencyPlayerPrefsRepository _repository;

    // 마틴 아저씨: 미리하는 성능 최적화의 90%는 의미가 없다.
    //public event Action OnGoldChanged;
    //public event Action OnDiamondChanged;

    // 관리자 : 추가 생성 조회 삭제
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
        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // 레포지토리(깃허브)
        _repository = new CurrencyPlayerPrefsRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        for (int i = 0; i < (int)ECurrencyType.Count; ++i)
        {
            ECurrencyType type = (ECurrencyType)i;
            CurrencyDTO loadedCurrency = loadedCurrencies?.Find(data => data.Type == type);

            // 저장된 데이터가 있다면 그 값.. 없다면 0
            Currency currency = new Currency(type, loadedCurrency != null ? loadedCurrency.Value : 0);

            _currencies.Add(type, currency);
        }
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(cureency => new CurrencyDTO(cureency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);
        AchievementManager.Instance.Increase(EAchievementCondition.GoldCollect, value);
        _repository.Save(ToDtoList());
        OnDataChanged?.Invoke();
    }

    //public void Subtract(ECurrencyType type, int value)
    //{
       
    //}

    public bool TryBuy(ECurrencyType type, int value)
    {
        // 구글 검색 : "묻지 말고 시켜라!"
        // 현재 문제
        // 1. 도메인의 규칙이 UI에?
        // 2. 규칙이 바뀌면 UI까지 와서 수정해야한다.
        // 3. '사다' 라는 행위는 '커렌시 도메인'의 중요한 역활
        // 4. '사다' 라는 행위는 상점/뭐시기/저시기 등 다양한 곳에서 쓰일텐데..그때마다 로직을 ?
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());
        OnDataChanged?.Invoke();
        return true;
    }

}
