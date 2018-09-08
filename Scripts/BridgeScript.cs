using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour {

    public Animator bridgeAnimation;

	// Use this for initialization
	void Start () {
        bridgeAnimation = GetComponent<Animator>();
	}

    void Update()
    {
        if (!GameObject.Find("Platform").GetComponent<ButtonBehavior>().started)
        {
            bridgeAnimation.enabled = true;
        }
    }

    void PauseAnimation ()
    {
        bridgeAnimation.enabled = false;
    }
	
}
