using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour
{
    public Animator animRightPage;
    public Transform resetPosition;
    public Transform objectToReset;

    private int currentPage;

	// Use this for initialization
	void Start () {
        currentPage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (animRightPage == null)
        {
            return;   
        }

        if (other.gameObject.tag == "RightPage")
        {
            animRightPage.SetTrigger("MovePage");

            if (resetPosition != null && objectToReset != null)
            {
                objectToReset.position = resetPosition.position;
                objectToReset.rotation = resetPosition.rotation;
            }

            currentPage++;
        }
        

    }
}
