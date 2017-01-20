using System;
using System.Collections;
using UnityEngine;

[Prefab("Audio manager", true)]
public class AudioManager : Singleton<AudioManager>
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private AudioSource ambientSoundAudioSource;
    private AudioSource[] effectSoundAudioSourceList;
    private AudioSource musicAudioSource;

    private int effectSoundAudioSourcePointer = 0;

    private float ambientSoundVolume = 1.0f;
    private float effectSoundVolume = 1.0f;
    private float musicVolume = 1.0f;
    
    private bool effectSoundMuted = false;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    /// <summary>
    /// Obtiene el clip de sonido que está asignado actualmente en el canal de
    /// sonidos ambientales.
    /// </summary>
    /// <value>Clip de sonidos actual.</value>
    public AudioClip CurrentAmbientSound
    {
        get { return AudioManager.Instance.ambientSoundAudioSource.clip; }
    }
    
    /// <summary>
    /// Obtiene el clip de música que está asignado actualmente en el canal de
    /// música.
    /// </summary>
    /// <value>Clip de música actual.</value>
    public AudioClip CurrentMusic
    {
        get { return AudioManager.Instance.musicAudioSource.clip; }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de los sonidos ambientales, en el rango de
    /// valores entre 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de los sonidos ambientales.</value>
    public float AmbientSoundVolume
    {
        get { return AudioManager.Instance.ambientSoundAudioSource.volume; }
        set
        {
            AudioManager.Instance.ambientSoundVolume = Mathf.Clamp01(value);
            AudioManager.Instance.ambientSoundAudioSource.volume = Mathf.Clamp01(value);
        }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de los efectos de sonido, en el rango de
    /// valores entre 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de los efectos de sonido.</value>
    public float EffectSoundVolume
    {
        get { return AudioManager.Instance.effectSoundVolume; }
        set
        { 
            AudioManager.Instance.effectSoundVolume = Mathf.Clamp01(value);
            foreach (AudioSource effectSoundAudioSource in effectSoundAudioSourceList)
                effectSoundAudioSource.volume = Mathf.Clamp01(value);
        }
    }
    
    /// <summary>
    /// Obtiene o asigna el volumen de la música, en el rango de valores entre
    /// 0.0 y 1.0.
    /// </summary>
    /// <value>Volumen de la música.</value>
    public float MusicVolume
    {
        get { return AudioManager.Instance.musicAudioSource.volume; }
        set
        {
            AudioManager.Instance.musicVolume = Mathf.Clamp01(value);
            AudioManager.Instance.musicAudioSource.volume = Mathf.Clamp01(value);
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de música se
    /// está reproduciendo en bucle.
    /// </summary>
    public bool MusicLoop
    {
        get { return AudioManager.Instance.musicAudioSource.loop; }
        set { AudioManager.Instance.musicAudioSource.loop = value; }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de sonidos
    /// ambientales se está reproduciendo en bucle.
    /// </summary>
    public bool AmbientSoundLoop
    {
        get { return AudioManager.Instance.ambientSoundAudioSource.loop; }
        set { AudioManager.Instance.ambientSoundAudioSource.loop = value; }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de música está
    /// silenciado.
    /// </summary>
    public bool MusicMuted
    {
        get { return AudioManager.Instance.musicAudioSource.mute; }
        set
        {
            AudioManager.Instance.musicAudioSource.mute = value;
            if (!value)
                AudioManager.Instance.musicAudioSource.volume = AudioManager.Instance.musicVolume;
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de sonidos
    /// ambientales está silenciado.
    /// </summary>
    public bool AmbientSoundMuted
    {
        get { return AudioManager.Instance.ambientSoundAudioSource.mute; }
        set
        {
            AudioManager.Instance.ambientSoundAudioSource.mute = value;
            if (!value)
                AudioManager.Instance.ambientSoundAudioSource.volume = AudioManager.Instance.ambientSoundVolume;
        }
    }
    
    /// <summary>
    /// Obtiene o asigna un valor que indica si el canal de efectos de sonido
    /// está silenciado.
    /// </summary>
    public bool EffectSoundMuted
    {
        get { return AudioManager.Instance.effectSoundMuted; }
        set { AudioManager.Instance.effectSoundMuted = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de control de música
    /// <summary>
    /// Reproduce el clip de música asignado actualmente en el canal de
    /// música, o continua su reproducción si estaba pausado.
    /// </summary>
    public void PlayMusic()
    {
        AudioManager.Instance.musicAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de música, deteniendo una música anterior si se
    /// estaba reproduciendo. El cambio se produce instantáneamente.
    /// </summary>
    /// <param name="musicClip">Clip de música.</param>
    public void PlayMusic(AudioClip musicClip)
    {
        AudioManager.Instance.musicAudioSource.clip = musicClip;
        AudioManager.Instance.musicAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de música, deteniendo una música anterior si se
    /// estaba reproduciendo. Se produce un efecto de desvanecimiento que dura
    /// el tiempo especificado.
    /// </summary>
    /// <param name="musicClip">Clip de música.</param>
    /// <param name="crossFadeTime">Tiempo de desvanecimiento.</param>
    public void PlayMusic(AudioClip musicClip, float crossFadeTime)
    {
        AudioManager.Instance.StartCoroutine(AudioManager.Instance.CrossFadeMusic(musicClip, crossFadeTime));
    }
    
    /// <summary>
    /// Pausa la música.
    /// </summary>
    public void PauseMusic()
    {
        AudioManager.Instance.musicAudioSource.Pause();
    }
    
    /// <summary>
    /// Detiene la música.
    /// </summary>
    public void StopMusic()
    {
        AudioManager.Instance.musicAudioSource.Stop();
    }
    
    // Métodos de control de sonidos ambiente
    /// <summary>
    /// Reproduce el clip de sonido ambiental asignado actualmente en el canal
    /// de sonidos ambientales, o continua su reproducción si estaba pausado.
    /// </summary>
    public void PlayAmbientSound()
    {
        if (AudioManager.Instance.CurrentAmbientSound != null)
            AudioManager.Instance.ambientSoundAudioSource.Play();
    }
    
    /// <summary>
    /// Reproduce un clip de sonido ambiental, deteniendo un clip anterior si
    /// se estaba reproduciendo. El cambio se produce instantáneamente.
    /// </summary>
    /// <param name="ambientSoundClip">Clip de sonido ambiental.</param>
    public void PlayAmbientSound(AudioClip ambientSoundClip)
    {
        if (ambientSoundClip != null)
        {
            AudioManager.Instance.ambientSoundAudioSource.clip = ambientSoundClip;
            AudioManager.Instance.ambientSoundAudioSource.Play();
        }
    }
    
    /// <summary>
    /// Reproduce un clip de sonido ambiental, deteniendo un clip anterior si
    /// se estaba reproduciendo. Se produce un efecto de desvanecimiento que
    /// dura el tiempo especificado.
    /// </summary>
    /// <param name="ambientSoundClip">Clip de sonido ambiental.</param>
    /// <param name="crossFadeTime">Tiempo de desvanecimiento.</param>
    public void PlayAmbientSound(AudioClip ambientSoundClip, float crossFadeTime)
    {
        if (ambientSoundClip != null)
        {
            AudioManager.Instance.StartCoroutine(AudioManager.Instance.CrossFadeAmbientSound(ambientSoundClip, crossFadeTime));
        }
    }
    
    /// <summary>
    /// Pausa el clip de sonido ambiental.
    /// </summary>
    public void PauseAmbientSound()
    {
        AudioManager.Instance.ambientSoundAudioSource.Pause();
    }
    
    /// <summary>
    /// Detiene el clip de sonido ambiental.
    /// </summary>
    public void StopAmbientSound()
    {
        AudioManager.Instance.ambientSoundAudioSource.Stop();
    }
    
    // Métodos de control de efectos de sonido
    /// <summary>
    /// Reproduce un efecto de sonido sin tener en cuenta su posición en el
    /// espacio.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip)
    {
        if (!AudioManager.Instance.effectSoundMuted && (effectSoundClip != null))
        {
            AudioSource selectedAudioSource = AudioManager.Instance.effectSoundAudioSourceList[AudioManager.Instance.effectSoundAudioSourcePointer];

            selectedAudioSource.clip = effectSoundClip;
            selectedAudioSource.pitch = 1.0f;
            selectedAudioSource.Play();

            int totalChannels = AudioManager.Instance.effectSoundAudioSourceList.Length;
            AudioManager.Instance.effectSoundAudioSourcePointer = (AudioManager.Instance.effectSoundAudioSourcePointer + 1) % totalChannels;
        }
    }

    /// <summary>
    /// Reproduce un efecto de sonido sin tener en cuenta su posición en el
    /// espacio y con el tono especificado.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, float pitch)
    {
        if (!AudioManager.Instance.effectSoundMuted && (effectSoundClip != null))
        {
            AudioSource selectedAudioSource = AudioManager.Instance.effectSoundAudioSourceList[AudioManager.Instance.effectSoundAudioSourcePointer];

            selectedAudioSource.clip = effectSoundClip;
            selectedAudioSource.pitch = pitch;
            selectedAudioSource.Play();

            int totalChannels = AudioManager.Instance.effectSoundAudioSourceList.Length;
            AudioManager.Instance.effectSoundAudioSourcePointer = (AudioManager.Instance.effectSoundAudioSourcePointer + 1) % totalChannels;
        }
    }

    /// <summary>
    /// Reproduce un efecto de sonido 3D en la posición especificada por un
    /// objeto.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    /// <param name="position">Objeto del juego.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, GameObject gameObject)
    {
        if (!AudioManager.Instance.effectSoundMuted && (effectSoundClip != null))
            AudioSource.PlayClipAtPoint(effectSoundClip, gameObject.transform.position, AudioManager.Instance.effectSoundVolume);
    }
    
    /// <summary>
    /// Reproduce un efecto de sonido 3D en la posición especificada por un
    /// vector.
    /// </summary>
    /// <param name="soundEffectClip">Clip de sonido.</param>
    /// <param name="position">Vector de posición.</param>
    public void PlaySoundEffect(AudioClip effectSoundClip, Vector3 position)
    {
        if (!AudioManager.Instance.effectSoundMuted && (effectSoundClip != null))
            AudioSource.PlayClipAtPoint(effectSoundClip, position, AudioManager.Instance.effectSoundVolume);
    }
    
    // Corrutinas
    private IEnumerator CrossFadeMusic(AudioClip musicClip, float crossFadeTime)
    {
        float halfCrossFadeTimeInversed = 1.0f / (crossFadeTime * 0.5f);
        
        float timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            AudioManager.Instance.musicAudioSource.volume = (1.0f - timeCounter) * AudioManager.Instance.musicVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
        
        AudioManager.Instance.musicAudioSource.clip = musicClip;
        AudioManager.Instance.musicAudioSource.Play();
        
        timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            AudioManager.Instance.musicAudioSource.volume = timeCounter * AudioManager.Instance.musicVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
    }
    
    private IEnumerator CrossFadeAmbientSound(AudioClip ambientSoundClip, float crossFadeTime)
    {
        float halfCrossFadeTimeInversed = 1.0f / (crossFadeTime * 0.5f);
        
        float timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            AudioManager.Instance.musicAudioSource.volume = (1.0f - timeCounter) * AudioManager.Instance.ambientSoundVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
        
        AudioManager.Instance.musicAudioSource.clip = ambientSoundClip;
        AudioManager.Instance.musicAudioSource.Play();
        
        timeCounter = 0.0f;
        while (timeCounter < 1.0f)
        {
            AudioManager.Instance.musicAudioSource.volume = timeCounter * AudioManager.Instance.ambientSoundVolume;
            timeCounter += Time.unscaledDeltaTime * halfCrossFadeTimeInversed;
            yield return null;
        }
    }
    
    // Métodos de MonoBehaviour
    private void Awake()
    {
        // Crear componentes en tiempo de ejecución
        AudioManager.Instance.musicAudioSource = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
        AudioManager.Instance.ambientSoundAudioSource = AudioManager.Instance.gameObject.AddComponent<AudioSource>();

        AudioManager.Instance.effectSoundAudioSourceList = new AudioSource[4];
        for (int i = 0; i < effectSoundAudioSourceList.Length; i++)
            AudioManager.Instance.effectSoundAudioSourceList[i] = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
        
        // Configurar componentes
        AudioManager.Instance.musicAudioSource.loop = true;
        AudioManager.Instance.musicAudioSource.mute = false;
        AudioManager.Instance.musicAudioSource.priority = 0;
        AudioManager.Instance.musicAudioSource.spatialBlend = 0.0f;
        AudioManager.Instance.musicAudioSource.spatialize = false;
        
        AudioManager.Instance.ambientSoundAudioSource.loop = true;
        AudioManager.Instance.ambientSoundAudioSource.mute = false;
        AudioManager.Instance.ambientSoundAudioSource.priority = 8;
        AudioManager.Instance.ambientSoundAudioSource.spatialBlend = 0.0f;
        AudioManager.Instance.ambientSoundAudioSource.spatialize = false;

        foreach (var effectSoundAudioSource in effectSoundAudioSourceList)
        {
            effectSoundAudioSource.loop = false;
            effectSoundAudioSource.mute = false;
            effectSoundAudioSource.priority = 16;
            effectSoundAudioSource.spatialBlend = 0.0f;
            effectSoundAudioSource.spatialize = false;

            effectSoundAudioSource.volume = AudioManager.Instance.effectSoundVolume;
        }

        // Asignar volumen
        AudioManager.Instance.musicAudioSource.volume = AudioManager.Instance.musicVolume;
        AudioManager.Instance.ambientSoundAudioSource.volume = AudioManager.Instance.ambientSoundVolume;
    }
    
}