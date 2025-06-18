using System.Collections.Generic;
using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;

    // Todo: DTO 받아오기
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

                    // todo: 체력과 공격력 세팅
                    // var health = enemyGameObject.GetComponent<Health>();
                    // health.체력 셋(기본 체력 * _stageLevel.HealthFactor);
                }
            }


        }
    }

}