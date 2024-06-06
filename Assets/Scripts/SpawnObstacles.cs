using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] Obstacles;
    public Timer GameTimer;
    public GameObject[] BonusObstacles;
    
    private float _spawnInterval;
    private float _difficultyInterval = 15;
    private float _currentDifficulty = 2;
    private bool _isSpawningObstacles = false;
    public TMP_Text _countdownText;
    private float _countdownTime = 5.0f;
    private int _maxDifficultyIncreases = 5;
    private int _currentNumberOfDiffultyIncreases = 0;
    private bool _bonusObstaclesAdded;

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
            _difficultyInterval = 15;
            _currentNumberOfDiffultyIncreases++;
        }

        if(_currentNumberOfDiffultyIncreases == 4 && !_bonusObstaclesAdded)
        {
            Obstacles = Obstacles.Concat(BonusObstacles).ToArray();
            _bonusObstaclesAdded = true;
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
            _countdownText.text = _countdownTime.ToString("F0");
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
