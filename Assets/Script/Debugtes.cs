using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugtes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Ball;
    public GameObject Lemur;
    public GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Ball = "+Ball.transform.position);
        Debug.Log("Lemur = " + Lemur.transform.position);
        Debug.Log("Camera = " + Camera.transform.position);
    }
}
