using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Catchable")
        {
            StartCoroutine("Catching", other.gameObject);
        }
    }

    IEnumerator Catching(GameObject Catchable)
    {
        Debug.Log("Testing OK~");
        return null;
    }
}
