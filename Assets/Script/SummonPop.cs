using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPop : MonoBehaviour
{
    int i = 0;
    public GameObject Pop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i >= 40)
        {
            Pop.SetActive(true);
        }
    }
}
