using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ((MovieTexture)(this.GetComponent<RawImage>().mainTexture)).Play();
        this.StartCoroutine(this.WaitEnd(7.0f));
	}

    private IEnumerator WaitEnd(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(1);
	}
}
