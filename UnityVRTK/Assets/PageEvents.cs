using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageEvents : MonoBehaviour
{
    public GameObject bookContent;
    private AudioSource audioEvent;

    void Start () {
        audioEvent = GetComponent<AudioSource>();

        if (bookContent != null)
        {
            bookContent.SetActive(false);
        }
	}

    public void playSound()
    {
        if (audioEvent != null)
        {
            audioEvent.Play();
        }
    }

    public void activateContent()
    {
        if (bookContent != null)
        {
            bookContent.SetActive(true);
        }
    }
}
