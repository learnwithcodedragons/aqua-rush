using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D _rb2d;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = new Vector2(0, -1);
        _rb2d.MovePosition(_rb2d.position + movementVector * Time.deltaTime * MovementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Raft"))
        {
            Destroy(this.gameObject);
        }
    }
}
