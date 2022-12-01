using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public GameObject Non;
    public GameObject AR;
    public GameObject Sound;
    public float i;
    bool j = false;
    public AudioSource Click;
    
    // Start is called before the first frame update
    public void NonToAR()
    {
        Click.Play();
        Non.SetActive(false);
        AR.SetActive(true);
    }
    public void ARToNon()
    {
        Click.Play();
        Non.SetActive(true);
        AR.SetActive(false);
    }
    public void PoptoPop()
    {
        Click.Play();
        Destroy(Non);
        AR.SetActive(true);
        j = true;
    }
    private void Update()
    {
        if (j == true)
        {
            i -= Time.deltaTime;
            if (i < 0)
            {
                Sound.SetActive(false);
            }
        }
 

    }

}
