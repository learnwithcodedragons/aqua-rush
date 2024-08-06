using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public Timer GameTimer;
    public GameObject PickUp;

    private bool hasPickUpSpwaned;

    void Update()
    {
        if(GameTimer.GetTimeElapsedInSeconds() == 30)
        {
            InstantiatePuckUp();
        }
    }

    private void InstantiatePuckUp()
    {
        if (!hasPickUpSpwaned)
        {
            var position = new Vector3(Random.Range(-2.0f, 1.7f), 4.25f, 0);
            Instantiate(PickUp, position, Quaternion.identity);
            hasPickUpSpwaned = true;
        }
    }
}
