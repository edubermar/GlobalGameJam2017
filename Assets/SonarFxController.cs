using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarFxController : MonoBehaviour
{
    private SonarFx sonarFx;

	// Use this for initialization
	private void Start()
    {
        this.sonarFx = this.GetComponent<SonarFx>();
        PlayerController.OnPlayerMoved += this.OnPlayerMoved;
	}

    private void OnDestroy()
    {
        PlayerController.OnPlayerMoved -= this.OnPlayerMoved;
    }

    private void OnPlayerMoved(Vector3 obj)
    {
        this.sonarFx.origin = obj;
    }

}