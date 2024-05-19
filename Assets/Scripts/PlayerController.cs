using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public GameObject GameContoller;
    public Timer GameTimer;
    public GameObject GameOverPanel;
    public GameObject LeaderBoardEntry;
    public SpawnObstacles Obstacles;
  
    private Rigidbody2D _rb2d;
    private bool _canMove = true;
    private Animator _anim;
    private Direction _direction;
    private bool _moveLeft;
    private bool _moveRight;
    private LeaderBoardManager _leaderBoardManager;

    enum Direction
    {
        Left,
        Right,
        Centre
    }

    public void CanMove(bool canMove)
    {
        _canMove = canMove;
    }

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _direction = Direction.Centre;
        _leaderBoardManager = GameContoller.GetComponent<LeaderBoardManager>();
    }

    void Update()
    {
        if (_canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _direction != Direction.Centre)
            {
                _anim.SetBool("IsRowing", true);

                if(_direction == Direction.Left)
                {
                    _moveLeft = true;
                    _moveRight = false;
                }

                if(_direction == Direction.Right)
                {
                    _moveLeft = false;
                    _moveRight = true;
                }
                
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _anim.SetBool("IsRowing", false);
                _moveRight = false;
                _moveLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anim.SetBool("IsRowing", false);
                _anim.SetBool("IsLeft", true);
                _anim.SetBool("IsRight", false);
                _direction = Direction.Right;
                _moveRight = false;
                _moveLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anim.SetBool("IsRowing", false);
                _anim.SetBool("IsRight", true);
                _anim.SetBool("IsLeft", false);
                _direction = Direction.Left;
                _moveRight = false;
                _moveLeft = false;
            }

            if (_moveRight)
            {
                _rb2d.MovePosition(_rb2d.position + new Vector2(1, 0) * Time.deltaTime * MovementSpeed);
            }

            if (_moveLeft)
            {
                _rb2d.MovePosition(_rb2d.position + new Vector2(-1, 0) * Time.deltaTime * MovementSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOverPanel.SetActive(true);
            _canMove = false;
            GameTimer.StopTimer();
            Obstacles.StopSpawing();

            var topTenScore = _leaderBoardManager.GetTopTenScore();
            var playerBestScore = _leaderBoardManager.GetPlayersBestScore();
            var timeElapsed = GameTimer.GetTimeElapsedInSeconds();

            if (timeElapsed > topTenScore  && timeElapsed > playerBestScore )
            {
                LeaderBoardEntry.SetActive(true);
            }
        }
    }
}
