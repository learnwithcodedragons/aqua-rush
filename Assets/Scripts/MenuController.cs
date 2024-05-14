using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject GameContoller;
    public GameObject Menu;
    public GameObject Raft;
    public Timer GameTimer;

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

        Raft.GetComponent<PlayerController>().CanMove(true);

        Menu.SetActive(false);
        _spawnObstacles.IsSpawningObstacles = false;
        GameTimer.ResetTimer();
        GameTimer.StartTimer();
        var coroutine = StartObstaclesWithDelay(2);
        StartCoroutine(coroutine);
   
    }

    private IEnumerator StartObstaclesWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spawnObstacles.IsSpawningObstacles = true;
    }
}
