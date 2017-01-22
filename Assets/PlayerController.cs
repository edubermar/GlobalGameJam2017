﻿using Extensions.System;
using Extensions.UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Campos
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    private float life = 100.0f;
    private bool dead = false;

    private float forceAux = 0.003f;

    // Propiedades
    public float Life
    {
        get { return this.life; }
        private set { this.life = value.ClampTo(0.0f, 100.0f); }
    }

    public bool Dead
    {
        get { return this.dead; }
    }

    // Eventos
    public static event Action<Vector3> OnPlayerMoved = delegate { };
    public static event Action<DeathType> OnPlayerDied = delegate { };

    // Métodos
    private void Start()
    {
        this.playerRigidbody = this.GetComponent<Rigidbody2D>();
        this.playerAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //this.transform.Translate(input * Time.deltaTime);
        
        this.playerRigidbody.AddForce(input, ForceMode2D.Impulse);
        this.playerAnimator.SetFloat("Horizontal speed", input.x);
        this.transform.SetRotationEulerZ(-input.x * 15.0f);

        PlayerController.OnPlayerMoved(this.transform.position);

        if (!this.dead)
            this.Life += 5f * Time.deltaTime;

        if (this.transform.position.x < Camera.main.transform.position.x - Camera.main.aspect )
        {
            this.dead = true;

            PlayerController.OnPlayerDied(DeathType.leftCamera);
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.life < 20.0f) // Se ha muerto
        {
            this.life = 0.0f;
            this.dead = true;

            DeathType deathType = DeathType.WallHit;
            if (other.collider.tag == "Wall")
                deathType = DeathType.WallHit;
            if (other.collider.tag == "Snake")
                deathType = DeathType.Snake;
            if (other.collider.tag == "Spider")
                deathType = DeathType.Spider;
            
            PlayerController.OnPlayerDied(deathType);
        }
        else // Choque no letal
        {
            this.Life -= 20.0f;

            this.GetComponent<SpriteRenderer>().color = Color.red;
            Vector3 repulsionVector = this.transform.position - (Vector3)other.contacts[0].point;

            float force = (200.0f + (100.0f - this.life) * 10.0f) * forceAux;
            this.playerRigidbody.AddForce(repulsionVector.normalized * force, ForceMode2D.Impulse);
            this.StartCoroutine(this.DragAdjustmentCorroutine(0.5f));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator DragAdjustmentCorroutine(float totalTime)
    {
        float time = 0.0f, inverseTotalTime = 1.0f / totalTime;
        while (time < 1.0f)
        {
            this.playerRigidbody.drag = Mathf.Lerp(15.0f, 25.0f, time);
            time += Time.deltaTime * inverseTotalTime;
            yield return null;
        }
        this.playerRigidbody.drag = 25.0f;
    }

}