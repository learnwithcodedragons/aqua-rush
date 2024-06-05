using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] Obstacles;
    public Timer GameTimer;
    
    private float _spawnInterval;
    private float _difficultyInterval = 10;
    private float _currentDifficulty = 2;
    private bool _isSpawningObstacles = false;
    public TMP_Text _countdownText;
    private float _countdownTime = 5.0f;
    private int _maxDifficultyIncreases = 5;
    private int _currentNumberOfDiffultyIncreases;

    private void Start()
    {
        _spawnInterval = 2;
        StartCoroutine(CountdownCoroutine());
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
        }
        else if(_currentNumberOfDiffultyIncreases < _maxDifficultyIncreases)
        {
            _currentDifficulty -= 0.2f;
            _difficultyInterval = 30;
            _currentNumberOfDiffultyIncreases++;
        }
    }

    private void InstantiateObstacle()
    {

        if (!_isSpawningObstacles)
        {
            return;
        }

        var obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
        var position = new Vector3(Random.Range(-2.0f, 1.7f), 4.25f, 0);
        Instantiate(obstacle, position, Quaternion.identity);
        
    }

    public void StopSpawing()
    {
        _isSpawningObstacles = false;
    }

    IEnumerator CountdownCoroutine()
    {
        while (_countdownTime > 0)
        {
            _countdownText.text = _countdownTime.ToString("F0"); // Display as integer
            yield return new WaitForSeconds(1.0f);
            _countdownTime--;
        }

        _countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        _countdownText.text = "";

        _isSpawningObstacles = true;
        GameTimer.StartTimer();
    }
}
