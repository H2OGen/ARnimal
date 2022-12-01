using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPopAR : MonoBehaviour
{
    public GameObject Pop;
    public GameObject Sound;
    public float i;
    public float j;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    void Update()
    {
        i -= Time.deltaTime;
        if (i < 0)
        {
            Pop.SetActive(true);
            j -= Time.deltaTime;
            if (j < 0)
            {
                Sound.SetActive(false);
            }
        }

    }
}
