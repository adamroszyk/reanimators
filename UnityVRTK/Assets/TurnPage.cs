using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour
{
    public Animator animRightPage;
    public Transform resetPosition;
    public Transform objectToReset;
    public BookContent bookContent;
    public GameObject leftPage;
    public GameObject rightPage;
    public GameObject turnPage;
    public GameObject turnReversePage;

    private int currentPage;

	// Use this for initialization
	void Start () {
        currentPage = 0;
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

            PageContent currContent = bookContent.activatePage(currentPage);

            currContent.rightAnimator.SetTrigger("PageDown");
            currContent.leftAnimator.SetTrigger("PageDown");

            currentPage++;

            PageContent nextContent = bookContent.activatePage(currentPage);

            nextContent.gameObject.SetActive(true);

            if (resetPosition != null && objectToReset != null)
            {
                objectToReset.position = resetPosition.position;
                objectToReset.rotation = resetPosition.rotation;
            }

        }
        else if (other.gameObject.tag == "LeftPage")
        {
            Debug.Log("Left Page");
            animRightPage.SetTrigger("MovePage");

            if (resetPosition != null && objectToReset != null)
            {
                objectToReset.position = resetPosition.position;
                objectToReset.rotation = resetPosition.rotation;
            }

            currentPage--;
        }
        

    }
}
