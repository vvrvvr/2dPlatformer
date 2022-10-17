using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartWarning : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPressed)
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
