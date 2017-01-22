using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private AudioClip sonarClip;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Animator fading;

    private void Start()
    {
        AudioManager.Instance.AmbientSoundVolume = 0.3333f;
        AudioManager.Instance.PlayAmbientSound(this.sonarClip);

        PlayerController.OnPlayerDied += this.OnPlayerDied;
	}

    private void OnDestroy()
    {
        PlayerController.OnPlayerDied -= this.OnPlayerDied;
    }

    private void OnPlayerDied(DeathType obj)
    {
        gridController.movementSpeed = 0.0f;
        GameManager.Instance.CurrentPoints = Time.timeSinceLevelLoad;
        GameManager.Instance.DeathType = obj;

        // TODO
        this.StartCoroutine(this.OpenGameOverSceneCorroutine(2f));
    }

    public IEnumerator OpenGameOverSceneCorroutine(float time)
    {
        yield return new WaitForSeconds(time * 0.5f);

        this.canvas.SetActive(true);
        this.fading.Play("trans");

        yield return new WaitForSeconds(time * 0.5f);

        SceneManager.LoadScene(3);
    }
	
}