using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DArts;

public class DABugtes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }
    //public GameObject Ball;
    //public GameObject Catchable;
    //public GameObject Camera;
    // Update is called once per frame
    void Update()
    {
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        DABug.Log("");
        GameObject[] CatchObjects = GameObject.FindGameObjectsWithTag("Catchable");
        foreach (GameObject CatchObject in CatchObjects)
        {
            DABug.Log(CatchObject.name + " = " + CatchObject.transform.position);
        }
        GameObject[] CatcherObjects = GameObject.FindGameObjectsWithTag("Catcher");
        foreach (GameObject CatcherObject in CatcherObjects)
        {
            DABug.Log(CatcherObject.name + " = " + CatcherObject.transform.position);
        }
        GameObject[] CameraObjects = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject CameraObject in CameraObjects)
        {
            DABug.Log(CameraObject.name + " = " + CameraObject.transform.position);
        }
    }    
}
