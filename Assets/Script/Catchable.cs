using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{
    public GameObject Home;
    public GameObject Cam;
    public GameObject Back;
    public GameObject Explode;
    public GameObject Sfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Catcher")
        {
            StartCoroutine("Caught", other.gameObject);
        }
    }

    IEnumerator Caught(GameObject Catchable)
    {
        Destroy(this.gameObject);
        Explode.SetActive(true);
        Home.SetActive(false);
        Cam.SetActive(false);
        Back.SetActive(false);
        Sfx.SetActive(true);

        return null;
    }
}