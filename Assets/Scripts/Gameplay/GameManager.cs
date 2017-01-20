using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Prefab("Game manager", true)]
public sealed class GameManager : Singleton<GameManager>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Lenguaje
    private string gameLanguage;

    // Datos guardados del juego
    private GamePersistentData gamePersistentData;

    // Variables volátiles (se conservan entre cambios de escena, pero no son persistentes)
    private int currentPoints = 0;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Lenguaje
    public string GameLanguage
    {
        get { return GameManager.Instance.gameLanguage; }
        set
        {
            GameManager.Instance.gameLanguage = value;
            if (GameManager.Instance.OnGameLanguageChanged != null)
                GameManager.Instance.OnGameLanguageChanged(value);
        }
    }

    // Datos guardados del juego
    public GamePersistentData GamePersistentData
    {
        get
        {
            if (GameManager.Instance.gamePersistentData == null)
                GameManager.Instance.gamePersistentData = new GamePersistentData();
            return GameManager.Instance.gamePersistentData;
        }
    }

    // Variables volátiles
    public int CurrentPoints
    {
        get { return GameManager.Instance.currentPoints; }
        internal set
        {
            int setValue = value > 0 ? value : 0;
            if (GameManager.Instance.currentPoints != setValue)
            {
                GameManager.Instance.currentPoints = setValue;
                GameManager.Instance.OnCurrentPointsChanged(setValue);
            }
        }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<int> OnCurrentPointsChanged = delegate { };
    public event Action<string> OnGameLanguageChanged = delegate { };
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de Monobehaviour
    private void Awake()
    {
        GameManager.Instance.LoadData();
    }

    protected override void OnDestroy()
    {
        GameManager.Instance.SaveData();
        base.OnDestroy();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.D))
            GameManager.Instance.DeleteData();
        #endif
    }

    // Métodos de juego

    // Métodos de gestión de la salida
    public void ExitApplication()
    {
        #if UNITY_EDITOR
        Debug.Log("Salida de la aplicación recibida.");
        #endif
        
        Application.Quit();
    }

    // Métodos de guardado y carga de datos
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        this.LoadData();
    }

    public void LoadData()
    {
        // Cargar datos persistentes del juego
        if (PlayerPrefs.HasKey("GamePersistentData"))
        {
            string persistentData = PlayerPrefs.GetString("GamePersistentData");
            GameManager.Instance.gamePersistentData = JsonUtility.FromJson<GamePersistentData>(persistentData);
        }
        else
        {
            GameManager.Instance.gamePersistentData = new GamePersistentData();
        }

        // Cargar idioma actual del juego
        if (PlayerPrefs.HasKey("Language"))
            GameManager.Instance.gameLanguage = PlayerPrefs.GetString("Language");
        else
            GameManager.Instance.gameLanguage = "en";
    }

    public void SaveData()
    {
        // Guardar datos persistentes del juego
        string persistentDataString = JsonUtility.ToJson(GameManager.Instance.gamePersistentData);
        PlayerPrefs.SetString("GamePersistentData", persistentDataString);

        // Guardar idioma actual del juego
        PlayerPrefs.SetString("Language", GameManager.Instance.gameLanguage);
    }

}