using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStyler : MonoBehaviour
{
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Material gateMaterial;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;

    public void SetScheme(LevelScheme levelScheme)
    {
        RenderSettings.fogColor = levelScheme.skyColor;

        skyMaterial.color = levelScheme.skyColor;
        groundMaterial.color = levelScheme.groundColor;
        gateMaterial.color = levelScheme.gateColor;
        playerMaterial.color = levelScheme.playerColor;
        enemyMaterial.color = levelScheme.enemyColor;
    }
}
