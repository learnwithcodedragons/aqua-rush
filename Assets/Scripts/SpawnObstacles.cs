using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject Obstacle;
    public float SpawnRate;
    public bool IsSpawningObstacles;


    private void Start()
    {
        IsSpawningObstacles = false;
        InvokeRepeating("InstantiateObstacles", 0.0f, SpawnRate);
    }

    private void InstantiateObstacles()
    {
        if (IsSpawningObstacles)
        {
            var position = new Vector3(Random.Range(-2.0f, 1.7f), 2.75f, 0);
            Instantiate(Obstacle, position, Quaternion.identity);
        }

    }
}
