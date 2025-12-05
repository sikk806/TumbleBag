using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDead += HealthDeath;
    }

    private void HealthDeath()
    {
        Debug.Log($"나 쥬금. {gameObject.name}");
    }

    void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDead -= HealthDeath;
        }
    }
}