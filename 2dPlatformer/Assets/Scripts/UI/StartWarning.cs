using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWarning : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPressed)
        {
            isPressed = true;
            StartCoroutine(WaitOneFrame());

        }
    }

    private IEnumerator WaitOneFrame()
    {
        yield return new WaitForEndOfFrame();
        gameManager.SetPlayerControls(true);
        gameObject.SetActive(false);
    }
    
}
