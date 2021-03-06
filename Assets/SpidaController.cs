using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidaController : MonoBehaviour
{
    private Animator spidaAnimator;

    private void Awake()
    {
        this.spidaAnimator = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
        {
            this.spidaAnimator.SetBool("Near bat", true);
        }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.spidaAnimator.SetBool("Near bat", false);
        }
    }


}