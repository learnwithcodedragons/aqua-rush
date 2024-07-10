using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public TMP_Text TutorialText;
    public Animator PaddleAnimator;
    public Animator LeftAnimator;
    public Animator RightAnimator;
    public SpawnObstacles SpawnObstaclesScript;

    private bool _isPaddlePressed;
    private bool _isRightPressed;
    private bool _hasPaddledRight;
    private bool _isLeftPressed;
    private bool _tutorialEnded;


    void Start()
    {
  

        if (PersistenceManager.Instance.HasSeenTutorial)
        {
            TutorialText.gameObject.SetActive(false);
            SpawnObstaclesScript.StartSpawnCountDown();
            this.enabled = false;

        } else
        {
            TutorialText.text = "Hold X button to move right";
            PaddleAnimator.SetBool("isFlashing", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonEast.wasPressedThisFrame && !_isPaddlePressed)
        {
            _isPaddlePressed = true;
            StartCoroutine(UpdateMessage("Press the right arrow button", 3));
            PaddleAnimator.SetBool("isFlashing", false);
            RightAnimator.SetBool("isFlashing", true);
        }

        if (Gamepad.current.dpad.right.wasPressedThisFrame
            && _isPaddlePressed 
            && !_isRightPressed)
        {
            StartCoroutine(UpdateMessage("Hold X button to move left", 1));
            _isRightPressed = true;
            RightAnimator.SetBool("isFlashing", false);
            PaddleAnimator.SetBool("isFlashing", true);
        }

        if (Gamepad.current.buttonEast.wasPressedThisFrame
            && _isPaddlePressed 
            && _isRightPressed
            && !_hasPaddledRight)
        {
            StartCoroutine(UpdateMessage("Press the left arrow button", 3));
            _hasPaddledRight = true;
            LeftAnimator.SetBool("isFlashing", true);
            PaddleAnimator.SetBool("isFlashing", false); 
        }


        if (Gamepad.current.dpad.left.wasPressedThisFrame 
            && _isPaddlePressed
            && _isRightPressed
            && _hasPaddledRight 
            && !_isLeftPressed)
        {
            StartCoroutine(UpdateMessage("Hold X button to move right", 1));
            _isLeftPressed = true;
            LeftAnimator.SetBool("isFlashing", false);
            PaddleAnimator.SetBool("isFlashing", true);
        }

        if (Gamepad.current.buttonEast.wasPressedThisFrame
            && _isPaddlePressed 
            && _isRightPressed
            && _hasPaddledRight
            && _isLeftPressed
            && TutorialText.gameObject.activeInHierarchy
            && !_tutorialEnded)
        {
            PersistenceManager.Instance.SetHasSeenTutorial(true);
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        StartCoroutine(UpdateMessage("Lets go!", 3));
        PaddleAnimator.SetBool("isFlashing", false);
        LeftAnimator.SetBool("isFlashing", false);
        RightAnimator.SetBool("isFlashing", false);
        StartCoroutine(DisableInstructions());
        _tutorialEnded = true;
    }

    private IEnumerator UpdateMessage(string message, int wait)
    {
        yield return new WaitForSeconds(wait);
        TutorialText.text = message;

    }

    private IEnumerator DisableInstructions()
    {
        yield return new WaitForSeconds(3);
        TutorialText.gameObject.SetActive(false);
        SpawnObstaclesScript.StartSpawnCountDown();
        this.enabled = false;

    }
}
