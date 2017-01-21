using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GridController gridController;

    private void Start()
    {
        PlayerController.OnPlayerDied += this.OnPlayerDied;
	}

    private void OnDestroy()
    {
        PlayerController.OnPlayerDied -= this.OnPlayerDied;
    }

    private void OnPlayerDied(DeathType obj)
    {
        gridController.movementSpeed = 0.0f;

        // TODO
    }
	
}