using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public GameObject GameContoller;
    public Timer GameTimer;
    public GameObject GameOverPanel;
    public GameObject LeaderBoardEntry;
    public SpawnObstacles Obstacles;
    public GameObject OnScreenControls;
  
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
        _direction = Direction.Right;
        _anim.SetBool("IsRowing", false);
        _anim.SetBool("IsLeft", true);
        _leaderBoardManager = GameContoller.GetComponent<LeaderBoardManager>(); 
    }

    void Update()
    {
        if (_canMove)
        {
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
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

            if (Gamepad.current.buttonEast.wasReleasedThisFrame)
            {
                _anim.SetBool("IsRowing", false);
                _moveRight = false;
                _moveLeft = false;
            }

            if (Gamepad.current.dpad.left.wasPressedThisFrame)
            {
                _anim.SetBool("IsRowing", false);
                _anim.SetBool("IsLeft", true);
                _anim.SetBool("IsRight", false);
                _direction = Direction.Right;
                _moveRight = false;
                _moveLeft = false;
            }

            if (Gamepad.current.dpad.right.wasPressedThisFrame)
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
            OnScreenControls.SetActive(false);
            _canMove = false;
            GameTimer.StopTimer();
            Obstacles.StopSpawing();

            var topTenScore = _leaderBoardManager.GetTopTenScore();
            var playerBestScore = _leaderBoardManager.GetPlayersBestScore();
            var timeElapsed = GameTimer.GetTimeElapsedInSeconds();

            Debug.Log($"timeElapsed {timeElapsed} topTen {topTenScore} playerBestScore {playerBestScore} ");
            if (timeElapsed > topTenScore  
                && timeElapsed > playerBestScore && topTenScore != null)
            {
                LeaderBoardEntry.SetActive(true);
            }
        }
    }
}
