using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookContent : MonoBehaviour
{
    public List<PageContent> pages;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightPage")
        {

        }
    }

    public PageContent getPage(int i)
    {
        Debug.Log(i);
        Debug.Log(pages[i]);
        return pages[i];
    }
}
