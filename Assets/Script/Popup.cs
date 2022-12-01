using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public AudioSource Click;
    public void Pop(GameObject PopUp)
    {
        Click.Play();
        PopUp.SetActive(true);

    }

    public void Gone(GameObject PopDown)
    {
       
        PopDown.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
