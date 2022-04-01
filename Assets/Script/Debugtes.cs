using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugtes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject bola;
    public GameObject lemur;
    public GameObject Kamera;
    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("bola = "+bola.transform.position);
        Debug.Log("lemur = " + lemur.transform.position);
        Debug.Log("kamera = " + Kamera.transform.position);
    }
}
