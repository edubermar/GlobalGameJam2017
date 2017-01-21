using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    public CanvasGroup[] canvasGroupList;
    [SerializeField]
    public float crossFadeTime = 1.0f;

    [SerializeField]
    public AudioClip mainMenuMusic;

    private int currentCanvas = 0;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        // Reproducir música
        AudioManager.Instance.MusicVolume = 0.5f;
        AudioManager.Instance.PlayMusic(this.mainMenuMusic, 1.0f);
    }

    // Métodos de control
    public void ExitGame()
    {
        GameManager.Instance.ExitApplication();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToCanvasGroup(int index)
    {
        if ((index >= 0) && (index < this.canvasGroupList.Length))
            this.StartCoroutine(this.CrossFadeCanvasGroup(this.currentCanvas, index));
    }

    // Corrutinas
    private IEnumerator CrossFadeCanvasGroup(int startIndex, int endIndex)
    {
        // Ninguno de los dos lienzos, ni el de origen ni el de destino, deben ser interactivos
        this.canvasGroupList[startIndex].interactable = false;
        this.canvasGroupList[startIndex].blocksRaycasts = false;

        this.canvasGroupList[endIndex].interactable = false;
        this.canvasGroupList[endIndex].blocksRaycasts = false;

        // Interpolar transparencia de cada lienzo
        float time = 0.0f, inverseTotalTime = 1.0f / this.crossFadeTime;
        while (time < 1.0f)
        {
            this.canvasGroupList[startIndex].alpha = 1.0f - time;
            this.canvasGroupList[endIndex].alpha = time;

            time += Time.unscaledDeltaTime * inverseTotalTime;
            yield return null;
        }

        // Desactivar por completo el lienzo de origen
        this.canvasGroupList[startIndex].alpha = 0.0f;
        this.canvasGroupList[startIndex].interactable = false;
        this.canvasGroupList[startIndex].blocksRaycasts = false;

        // Activar por completo el lienzo de destino
        this.canvasGroupList[endIndex].alpha = 1.0f;
        this.canvasGroupList[endIndex].interactable = true;
        this.canvasGroupList[endIndex].blocksRaycasts = true;

        // Asignar índice interno
        this.currentCanvas = endIndex;
    }

}