using Redcode.Pools;
using Unity.FPS.AI;
using UnityEngine;

public class EnemySpanwer_2 : MonoBehaviour
{
    public Transform Transform;
    public float SpawnTime;
    public EnemyManager EnemyManager;
    public GameObject EnemyPrefab;
    public float EnemyTypeKey;
    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= SpawnTime)
        {
            if (EnemyManager.NumberOfEnemiesTotal >= 10) return;
            GameObject gameObject =  Instantiate(EnemyPrefab,this.gameObject.transform);
            gameObject.SetActive(true);
            _timer = 0;
        }
    }
}
