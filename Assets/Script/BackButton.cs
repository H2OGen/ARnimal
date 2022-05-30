using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public int type;
    public string sceneName;

void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if(type == 1)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else if(type == 0)
                {
                    Application.Quit();
                }
                return;
            }
        }
    }
}
