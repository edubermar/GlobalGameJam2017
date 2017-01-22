using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    private Animator snekAnimator;

    private void Awake()
    {
        this.snekAnimator = this.GetComponent<Animator>();
        /*
        RaycastHit2D result;
        result = Physics2D.BoxCast(new Vector2(0.45f, 0.3f), Vector2.one * 0.1f, 0.0f, Vector2.right); 
        if (result.collider == null)
            GameObject.Destroy(this.gameObject);

        result = Physics2D.BoxCast(new Vector2(0.45f, 0.0f), Vector2.one * 0.1f, 0.0f, Vector2.right); 
        if (result.collider == null)
            GameObject.Destroy(this.gameObject);

        result = Physics2D.BoxCast(new Vector2(0.45f, -0.3f), Vector2.one * 0.1f, 0.0f, Vector2.right); 
        if (result.collider == null)
            GameObject.Destroy(this.gameObject);*/
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