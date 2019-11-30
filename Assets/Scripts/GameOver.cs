using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI textGameOver;
    public TextMeshProUGUI textCompleted;

    void Start()
    {
        textGameOver.enabled = false;
    }

    private void Update()
    {
        if ((textGameOver.enabled) || (textCompleted.enabled))
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void ActivateGameOver()
    {
        textGameOver.enabled = true;
    }

    public void ActivateCompleted()
    {
        textCompleted.enabled = true;
    }
}
