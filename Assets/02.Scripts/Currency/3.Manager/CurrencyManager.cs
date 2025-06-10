using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ��Ű���� : ���� �� ��ä(���踶�� ö���� �ִ�.)
// ������ ���� : ���踦 �����ϴ� �������� ���̴� ����
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    // �����ο� ��ȭ�� ���� �� ȣ��Ǵ� �׼�
    public event Action OnDataChanged;

    private CurrencyPlayerPrefsRepository _repository;

    // ��ƾ ������: �̸��ϴ� ���� ����ȭ�� 90%�� �ǹ̰� ����.
    //public event Action OnGoldChanged;
    //public event Action OnDiamondChanged;

    // ������ : �߰� ���� ��ȸ ����
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
        // ����
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // �������丮(�����)
        _repository = new CurrencyPlayerPrefsRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        for (int i = 0; i < (int)ECurrencyType.Count; ++i)
        {
            ECurrencyType type = (ECurrencyType)i;
            CurrencyDTO loadedCurrency = loadedCurrencies?.Find(data => data.Type == type);

            // ����� �����Ͱ� �ִٸ� �� ��.. ���ٸ� 0
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
        // ���� �˻� : "���� ���� ���Ѷ�!"
        // ���� ����
        // 1. �������� ��Ģ�� UI��?
        // 2. ��Ģ�� �ٲ�� UI���� �ͼ� �����ؾ��Ѵ�.
        // 3. '���' ��� ������ 'Ŀ���� ������'�� �߿��� ��Ȱ
        // 4. '���' ��� ������ ����/���ñ�/���ñ� �� �پ��� ������ �����ٵ�..�׶����� ������ ?
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());
        OnDataChanged?.Invoke();
        return true;
    }

}
