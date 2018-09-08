using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour {

    public AudioSource[] sound;
    public bool started = false;

    private void Start()
    {
        sound = GetComponents<AudioSource>();
    }

    void OnTriggerEnter()
    {
        GameObject.Find("Bridge").GetComponent<BridgeScript>().bridgeAnimation.SetTrigger("lower");
        started = true;
        sound[0].Play();
    }

    void Update()
    {
        if (started && !sound[0].isPlaying)
        {
            sound[1].Play();
            started = false;
        }
    }
}
