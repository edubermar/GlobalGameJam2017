using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    
    private void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //this.transform.Translate(input * Time.deltaTime);
        
        this.playerRigidbody.AddForce(input, ForceMode2D.Impulse);
	}

    private void OnCollisionEnter2D()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnCollisionExit2D()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1.0f);
    }

}