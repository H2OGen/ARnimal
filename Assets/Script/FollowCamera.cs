using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private int z;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera);
        transform.Rotate(x, y, z);
    }
}
