using System.Collections.Generic;
using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;

    // Todo: DTO �޾ƿ���
    private StageLevel _stageLevel;


    private void Start()
    {
        Refresh();
        StageManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        _stageLevel = StageManager.Instance.Stage.CurrentLevel;
    }

    private void Update()
    {
        if (_stageLevel == null)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _stageLevel.SpawnInterval)
        {
            _currentTime = 0f;


            foreach (var spawnPoint in SpawnPoints)
            {
                if (Random.Range(0, 100) < _stageLevel.SpawnRate)
                {
                    GameObject enemyGameObject = Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);

                    // todo: ü�°� ���ݷ� ����
                    // var health = enemyGameObject.GetComponent<Health>();
                    // health.ü�� ��(�⺻ ü�� * _stageLevel.HealthFactor);
                }
            }


        }
    }

}