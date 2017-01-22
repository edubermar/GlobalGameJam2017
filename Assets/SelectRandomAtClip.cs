using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandomAtClip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float audioLength = this.GetComponent<AudioSource>().clip.length;
        this.GetComponent<AudioSource>().time = Random.Range(0.0f, audioLength);
        this.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
