using UnityEngine;
using System.Collections;

public class DragAndThrow1 : MonoBehaviour
{
    bool dragging = false;
    [SerializeField] private GameObject Front;
    float distance;
    public float ThrowSpeed;
    public float ArcSpeed;
    public float Speed;

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Front.transform.position);
        dragging = true;

    }

    public void OnMouseUp()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().velocity += Front.transform.forward * ThrowSpeed;
        this.GetComponent<Rigidbody>().velocity += this.transform.up * ArcSpeed;
        dragging = false;

    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = Vector3.Lerp(this.transform.position, rayPoint, Speed * Time.deltaTime);
        }
    }
}
