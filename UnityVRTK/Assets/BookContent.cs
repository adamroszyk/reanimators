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

    public PageContent activatePage(int pageNum)
    {
        if (pageNum >= pages.Count)
        {
            pageNum = 0;
        }

        for (int i = 0; i < pages.Count; i++)
        {
            if (i == pageNum)
            {
                pages[i].gameObject.SetActive(true);
            } else
            {
                pages[i].gameObject.SetActive(false);
            }
        }

        return pages[pageNum];
    }
}
