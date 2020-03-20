﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthStatus
{
    Healthy,
    Infected,
    Sick,
    Dead,
    Cured
}

public class PersonHealth : MonoBehaviour
{
    public HealthStatus Condition { get; set; } = HealthStatus.Healthy;
    public GameObject virusPrefab;
    public bool Aware { get; set; } = true;
    public int TimeInfected { get; set; } = 0;
    public int TimeSick { get; set; } = 0;
    public float Health { get; set; }

    private bool virusPresent = false;
    private VirusHealth virusHealth;
    private GameObject virus;


    public void ContractVirus()
    {
        virus = Instantiate(virusPrefab);
        virus.name = "virus";
        virusPresent = true;
        virusHealth = virus.GetComponent<VirusHealth>();
        virus.transform.parent = transform;
        Condition = HealthStatus.Infected;
    }

    public void RemoveVirus()
    {
        Destroy(virus);
        virusPresent = false;
        virusHealth = null;
        Condition = HealthStatus.Cured;
        
    }

    private void AttackVirus()
    {
        virusHealth.ReceiveAttack(0.1f);
    }

    public void ReceiveAttack(float damage)
    {
        Health -= damage;
        
        if (Health < 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(virusPresent)
        {
            AttackVirus();
        }
    }
}
