using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject GameContoller;
    public GameObject Menu;
    public GameObject Raft;

    private SpawnObstacles _spawnObstacles;

    private void Start()
    {
        _spawnObstacles = GameContoller.GetComponent<SpawnObstacles>();
    }

    public void StartGame()
    {
        var raftRigidBody = Raft.GetComponent<Rigidbody2D>();
        raftRigidBody.velocity = new Vector3(0f, 0f, 0f);
        raftRigidBody.angularVelocity = 0f;
        Raft.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        Raft.transform.position = new Vector3(0,0,0);

        Menu.SetActive(false);
        _spawnObstacles.IsSpawningObstacles = false;
        var coroutine = StartbstaclesWithDelay(3);
        StartCoroutine(coroutine);

    }

    private IEnumerator StartbstaclesWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spawnObstacles.IsSpawningObstacles = true;
    }
}
