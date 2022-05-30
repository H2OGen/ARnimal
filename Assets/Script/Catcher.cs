using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Catchable")
        {
            Debug.Log("ok1");
            StartCoroutine("Catching", other.gameObject);
        }
    }

    IEnumerator Catching(GameObject Catchable)
    {
        Destroy(this.gameObject);
        Debug.Log("ok2");
        return null;
    }
}
