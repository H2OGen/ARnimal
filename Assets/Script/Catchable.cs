using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Catcher")
        {
            StartCoroutine("Caught", other.gameObject);
        }
    }

    IEnumerator Caught(GameObject Catchable)
    {

        Destroy(gameObject);
        return null;
    }
}