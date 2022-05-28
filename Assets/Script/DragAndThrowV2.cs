using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragAndThrowV2 : MonoBehaviour
{
    bool dragging = false;
    float distance;
    [SerializeField] private GameObject Ball;
    bool x = false;
    public float ThrowSpeed;
    public float ArcSpeed;
    public float Speed;
    public void Throw()
    {
        x = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (x == false)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
        else
        {
            Ball.GetComponent<Rigidbody>().useGravity = true;
            Ball.GetComponent<Rigidbody>().velocity += Ball.transform.forward * ThrowSpeed;
            Ball.GetComponent<Rigidbody>().velocity += Ball.transform.up * ArcSpeed;
            dragging = false;
        }
        if (dragging)
        {
            transform.position = Vector3.Lerp(Ball.transform.position, Camera.main.transform.position, Speed * Time.time);
        }
    }
}
