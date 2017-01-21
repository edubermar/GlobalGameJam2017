using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// Clase que se encarga de almacenar todos los datos que deben persistir entre diferentes ejecuciones del juego.
/// Sus atributos deben ser serializables, para que el conversor JSON pueda convertirlos entre texto y datos.
/// </summary>
public class GamePersistentData
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private bool musicActive;
    [SerializeField]
    private bool soundActive;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public bool MusicActive
    {
        get { return this.musicActive; }
        internal set { this.musicActive = value; }
    }

    public bool SoundActive
    {
        get { return this.soundActive; }
        internal set { this.soundActive = value; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GamePersistentData"/>. Se deben asignar los valores por defecto que se desean para cada atributo para la primera ejecución del juego, o tras un borrado completo de datos.
    /// </summary>
    public GamePersistentData()
    {
        this.musicActive = true;
        this.soundActive = true;
    }

}