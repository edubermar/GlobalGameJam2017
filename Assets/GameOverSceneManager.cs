using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour
{
    public Text scoreText;
    public Image deathImage;

    public Sprite[] deathSprites;

    public AudioClip dolor;

    public void Awake()
    {
        AudioManager.Instance.StopAmbientSound();
        AudioManager.Instance.PlaySoundEffect(this.dolor);

        this.scoreText.text = string.Format("{0:0.00}", GameManager.Instance.CurrentPoints);

        if (GameManager.Instance.GamePersistentData.DistanceRecord < GameManager.Instance.CurrentPoints)
            GameManager.Instance.GamePersistentData.DistanceRecord = GameManager.Instance.CurrentPoints;

        int currentDeath = (int)GameManager.Instance.DeathType;
        this.deathImage.sprite = this.deathSprites[currentDeath];
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

}