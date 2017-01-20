#if (UNITY_5 || UNITY_4)
using System;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public class WaitForRealSeconds : CustomYieldInstruction
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private float waitTime;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public override bool keepWaiting
        {
            get { return Time.realtimeSinceStartup < this.waitTime; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public WaitForRealSeconds(float time)
        {
            this.waitTime = Time.realtimeSinceStartup + time;
        }
    }
    
}
#endif