using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookContent : MonoBehaviour
{
	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightPage")
        {

        }
    }
}
