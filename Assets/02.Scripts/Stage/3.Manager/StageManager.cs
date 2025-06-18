using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    // �����ο� ��ȭ�� ���� �� ȣ��Ǵ� �׼�
    public event Action OnDataChanged;

    [SerializeField]
    private List<StageLevelSO> _levelSOList;
    private Stage _stage;
    public StageDTO Stage => _stage.ToDTO();

    private StageRepository _repository;
    
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
        _repository = new StageRepository();
        
        StageSaveData stageSaveData = _repository.Load();
        _stage = new Stage(stageSaveData.LevelNumber, stageSaveData.SubLevelNumber, stageSaveData.ProgressTime, stageSaveData.Levels);
        OnDataChanged?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChanged);
    }
}
