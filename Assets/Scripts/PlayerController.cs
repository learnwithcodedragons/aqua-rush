using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public GameObject PlayAgain;
    public GameObject GameContoller;

    private Rigidbody2D _rb2d;
    private Rigidbody2D _playerRb2d;
  

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _rb2d.MovePosition(_rb2d.position + movementVector * Time.deltaTime * MovementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayAgain.SetActive(true);
        }
    }
}
