using System.Collections;
using UnityEngine;

public class GamInfo : MonoBehaviour
{
    public GameObject GameInfo;

    void Start()
    {
        StartCoroutine(DisableGameInstructions());
    }

    private IEnumerator DisableGameInstructions()
    {
        yield return new WaitForSeconds(5);
        GameInfo.SetActive(false);
        
    }
}
