using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject Obstacle;
    public bool IsSpawningObstacles;
    
    private float _spawnInterval;
    private float _difficultyInterval = 10;
    private float _currentDifficulty = 2;
   
    private void Start()
    {
        IsSpawningObstacles = false;
        _spawnInterval = 2;
    }

    private void FixedUpdate()
    {
        if (_spawnInterval > 0)
        {
            _spawnInterval -= Time.deltaTime;
        } else
        {
            InstantiateObstacle();
            _spawnInterval = _currentDifficulty;
        }

        if(_difficultyInterval > 0)
        {
            _difficultyInterval -= Time.deltaTime;
        } else
        {
            _currentDifficulty -= 0.2f;
            _difficultyInterval = 30;
        }
    }

    private void InstantiateObstacle()
    {
        if (IsSpawningObstacles)
        {
            var position = new Vector3(Random.Range(-2.0f, 1.7f), 4.25f, 0);
            Instantiate(Obstacle, position, Quaternion.identity);
        }
    }
}
