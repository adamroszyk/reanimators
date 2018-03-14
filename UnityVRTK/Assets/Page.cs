using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cover")
        {
            Debug.Log("Collision Detected");
            rb.isKinematic = false;
        }
    }
    

    private IEnumerator PageEffect()
    {
        yield return new WaitForSeconds(1);

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
