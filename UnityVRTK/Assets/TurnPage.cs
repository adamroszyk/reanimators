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

            PageContent currContent = bookContent.getPage(currentPage++);
            PageContent nextContent = bookContent.getPage(currentPage);

            currContent.rightPageContainer.transform.SetParent(turnPage.transform);
            currContent.leftPageContainer.transform.SetParent(leftPage.transform);
            currContent.rightAnimator.SetTrigger("PageDown");
            currContent.leftAnimator.SetTrigger("PageDown");

            nextContent.rightPageContainer.SetActive(true);
            nextContent.rightPageContainer.transform.SetParent(rightPage.transform);
            //nextContent.leftPageContainer.transform.SetParent()


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
