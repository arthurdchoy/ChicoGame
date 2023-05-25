using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePageSoundEffects : MonoBehaviour
{
    AudioSource buttonClick;
    AudioSource buttonTrigger;
    // Start is called before the first frame update
    void Start()
    {
        buttonClick = transform.Find("buttonClick").gameObject.GetComponent<AudioSource>();
        buttonTrigger = transform.Find("buttonTrigger").gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }public void playMusic(string audioName)
    {
        if (audioName == "buttonClick")
            buttonClick.Play();
        else if (audioName == "buttonTrigger")
            buttonTrigger.Play();
    }
}
