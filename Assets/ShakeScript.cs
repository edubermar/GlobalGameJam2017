using Extensions.UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    public float phase = 0.0f;

    private RectTransform rect;

    private void Awake()
    {
        this.rect = this.GetComponent<RectTransform>();
    }

	// Update is called once per frame
	private void Update () {
        var posX = (Mathf.PerlinNoise((Time.timeSinceLevelLoad * frequency) + this.phase, 0.0f) - 0.5f) * this.amplitude;
        var posY = (Mathf.PerlinNoise((Time.timeSinceLevelLoad * frequency) + this.phase, 100.0f) - 0.5f) * this.amplitude;

        this.rect.SetPosX(posX);
        this.rect.SetPosY(posY);
	}
}
