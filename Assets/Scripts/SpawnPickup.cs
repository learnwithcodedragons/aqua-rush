using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    public Timer GameTimer;
    public GameObject Bubble;
    public GameObject Bonus;

    void Start()
    {
        if(GameTimer.GetTimeElapsedInSeconds() % 30 == 0)
        {
            InvokeRepeating(nameof(InstantiateBubble), 30.0f, 30.0f);
            InvokeRepeating(nameof(InstantiateBonus), 20.0f, 30.0f);
        }
    }

    private void InstantiateBubble()
    {
        if (GameTimer.IsTiming())
        {
            var position = new Vector3(Random.Range(-2.0f, 1.7f), 4.25f, 0);
            Instantiate(Bubble, position, Quaternion.identity);
        }
    
    }

    private void InstantiateBonus()
    {
        if (GameTimer.IsTiming())
        {
            var position = new Vector3(Random.Range(-2.0f, 1.7f), 4.25f, 0);
            Instantiate(Bonus, position, Quaternion.identity);
        }
    }
}
