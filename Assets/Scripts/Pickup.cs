using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movementVector = new Vector2(0, -1);
        _rb2d.MovePosition(_rb2d.position + movementVector * Time.deltaTime * MovementSpeed);
    }
}


