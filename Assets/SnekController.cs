using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    private Animator snekAnimator;

    private void Awake()
    {
        this.snekAnimator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
        {
            this.snekAnimator.SetBool("Near bat", true);
        }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.snekAnimator.SetBool("Near bat", false);
        }
    }


}