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

public enum MedicalNeeds
{
    Hospital,
    Respirator,
    None
}

public enum ReceivingCare
{
    None,
    Hospital,
    Respirator
}

public class PersonHealth : MonoBehaviour
{
    private ConstantsManager constantsManager;
    public HealthStatus Condition { get; set; } = HealthStatus.Healthy;
    public MedicalNeeds Needs { get; set; } = MedicalNeeds.None;
    public ReceivingCare Care { get; set; } = ReceivingCare.None;
    private PersonAppearance personAppearance;
    private PersonBehaviour personBehaviour;

    public GameObject virusPrefab;
    private GameObject graveYard;

    public bool Aware { get; set; } = true;
    public int TimeInfected { get; set; } = 0;
    public int TimeSick { get; set; } = 0;
    public float Health { get; set; }

    public bool VirusPresent { get; set; } = false;
    private VirusHealth virusHealth;
    private GameObject virus;

  

    private void Awake()
    {
        personAppearance = gameObject.GetComponent<PersonAppearance>();
        personBehaviour = gameObject.GetComponent<PersonBehaviour>();
        Health = Random.Range(0.8f, 1);
        if(Random.Range(0, 1f) < 0.20f)
        {
            Health = 0.5f;
        }
    }

    private void Start()
    {
        constantsManager = GameObject.Find("ConstantManager").GetComponentInChildren<ConstantsManager>();
        graveYard = GameObject.Find("GraveYard");

    }

    public void ContractVirus()
    {
        if (!constantsManager) Start();
        virus = Instantiate(virusPrefab);
        virus.name = "virus";
        VirusPresent = true;
        virusHealth = virus.GetComponent<VirusHealth>();
        virus.transform.parent = transform;
        virus.transform.localPosition = Vector3.zero;
        Condition = HealthStatus.Infected;
        Constants.NumberOfInfected++;
        constantsManager.UpdateStats();
        personAppearance.UpdateMaterial();
        if(Constants.HealthBarDisplay == ShowHealthBar.infected || Constants.HealthBarDisplay == ShowHealthBar.all)
        {
            personAppearance.SetBarActive(true);    
        }
        
    }

    public void RemoveVirus()
    {
        virus.GetComponent<VirusAppearance>().DestroyHealthBar();
        
        Destroy(virus);
        VirusPresent = false;
        virusHealth = null;
        Condition = HealthStatus.Cured;
        gameObject.layer = 8;
        
        personBehaviour.Walk();
        Constants.NumberOfCured++;
        Constants.NumberOfInfected--;
        constantsManager.UpdateStats();
        personAppearance.UpdateMaterial();
        
        if(personBehaviour.UnderTreatment)
        {
            personBehaviour.ExitHospital();
        }
        
    }

    private void AttackVirus(float value = 0.0f)
    {
        if(value == 0.0f)
        {
            virusHealth.ReceiveAttack(Constants.PersonAttackPower);
        } else
        {
            virusHealth.ReceiveAttack(value);
        }
    }

    private float CalculateAttackMultiplier()
    {
        switch (Needs)
        {
            case MedicalNeeds.None: return 1f;
            case MedicalNeeds.Hospital: return 4f;
            case MedicalNeeds.Respirator: return 10f;
            default: return 1f;
        }
    }

    public void ReceiveAttack(float damage)
    {

        float damageMultiplier = CalculateAttackMultiplier();
        
        Health -= (damage * damageMultiplier);
        //personAppearance.UpdateBar();

        if (Health > 0.8) Needs = MedicalNeeds.None;
        if (Health < 0.6) Needs = MedicalNeeds.Hospital;
        //if (Health < 0.1) Needs = MedicalNeeds.Respirator;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        Condition = HealthStatus.Dead;
        Constants.NumberOfDead++;
        Constants.NumberOfInfected--;
        constantsManager.UpdateStats();
        personAppearance.UpdateMaterial();
        graveYard.GetComponent<GraveyardManagement>().AskAdmission(gameObject);
        if (personBehaviour.UnderTreatment)
        {
            personBehaviour.ExitHospital();
        }
    }

    private float CalculateHealMultiplier()
    {
        switch (Care)
        {
            case ReceivingCare.None: return 1;
            case ReceivingCare.Hospital: return 10;
            case ReceivingCare.Respirator: return 50;
            default: return 1;
        }
    }

    private void SelfHeal()
    {
        if (Health<1 && Condition != HealthStatus.Dead)
        {
            float healMultiplier = CalculateHealMultiplier();
            Health += Constants.PersonSelfHealRate * healMultiplier;
        }

        if (Health > 1) Health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        SelfHeal();
        if(VirusPresent && Condition!=HealthStatus.Dead)
        {
            if(personBehaviour.UnderTreatment)
            {
                AttackVirus(1);
            } else
            {
                AttackVirus();

            }

        }

        personAppearance.UpdateBar();

    }
}
