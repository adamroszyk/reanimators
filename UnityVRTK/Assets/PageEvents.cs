using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageEvents : MonoBehaviour
{
    public GameObject bookContent;
    private AudioSource audioEvent;
    public Animator anim;
    
    void Start () {
        audioEvent = GetComponent<AudioSource>();
        
        if (bookContent != null)
        {
            bookContent.SetActive(false);
        }

        anim = GetComponent<Animator>();
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

    public void resetAnim()
    {
        if (anim != null)
        {
            anim.SetTrigger("Reset");
        }
    }
}
