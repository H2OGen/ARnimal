using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Catcher")
        {
            Debug.Log("ok3");
            StartCoroutine("Caught", other.gameObject);
        }
    }

    IEnumerator Caught(GameObject Catchable)
    {
        Debug.Log("ok4");
        Destroy(this.gameObject);
        return null;
    }
}