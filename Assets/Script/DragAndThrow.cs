using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DragAndThrow : MonoBehaviour {
    bool dragging = false;
    [SerializeField] private GameObject Front;
    float distance;
    public float ThrowSpeed;
    public float ArcSpeed;
    public float Speed;
    bool timerr = false;
    public int i;
    public string thisScene;
    public GameObject Touch;
    public GameObject Release;



    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Touch.SetActive(true);
    }

    public void OnMouseUp()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().velocity += Front.transform.forward * ThrowSpeed;
        this.GetComponent<Rigidbody>().velocity += this.transform.up * ArcSpeed;
        dragging = false;
        timerr = true;
        Release.SetActive(true);
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = Vector3.Lerp(this.transform.position, rayPoint, Speed * Time.deltaTime);
        }
        if (timerr == true)
        {
            i++;
            
        }
        if (i >= 200)
        {
            SceneManager.LoadScene(thisScene);
        }
    }
}
