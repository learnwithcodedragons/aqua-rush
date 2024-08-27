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
    public GameObject Ability;
  
    private Rigidbody2D _rb2d;
    private bool _canMove = true;
    private Animator _anim;
    private Direction _direction = Direction.Right;
    private bool _moveLeft;
    private bool _moveRight;
    private LeaderBoardManager _leaderBoardManager;
    private bool _isBubbleActive;
    private float _numberOfSecondsAbilityActive = 10f;
    private float _expiryWarningSeconds = 3f;
    private float _abilityTimer;
    private Animator _bubbleAnim;

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
        _anim.SetBool("IsRowing", false);
        _anim.SetBool("IsLeft", true);
        _leaderBoardManager = GameContoller.GetComponent<LeaderBoardManager>();
        _abilityTimer = _numberOfSecondsAbilityActive;
        _bubbleAnim = Ability.GetComponent<Animator>();
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

        if (_isBubbleActive)
        {
            if (_abilityTimer > 0)
            {
                _abilityTimer -= Time.deltaTime;
                Debug.Log($"timer: {_abilityTimer}");
                if (_abilityTimer < _expiryWarningSeconds)
                {
                    Debug.Log("IsExpiring true");
                    _bubbleAnim.SetBool("IsExpiring", true);
                }
            }
            else
            {
                Ability.SetActive(false);
                _abilityTimer = _numberOfSecondsAbilityActive;
                _isBubbleActive = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (_isBubbleActive)
            {
                Destroy(collision.gameObject);
                return;
            }


            GameOverPanel.SetActive(true);
            OnScreenControls.SetActive(false);
            _canMove = false;
            GameTimer.StopTimer();
            Obstacles.StopSpawing();

            var topTenScore = _leaderBoardManager.GetTopTenScore();
            var playerBestScore = _leaderBoardManager.GetPlayersBestScore();
            var timeElapsed = GameTimer.GetTimeElapsedInSeconds();

            if (timeElapsed > topTenScore  
                && timeElapsed > playerBestScore && topTenScore != null)
            {
                LeaderBoardEntry.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            _isBubbleActive = true;
            Ability.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PointsBonus"))
        {

            GameTimer.AddTime(10);
            Destroy(collision.gameObject);

        }
    }
}
