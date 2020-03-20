using System.Collections;
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
    public HealthStatus Condition { get; set; } = HealthStatus.Healthy;
    public MedicalNeeds Needs { get; set; } = MedicalNeeds.None;
    public ReceivingCare Care { get; set; } = ReceivingCare.None;
    private PersonAppearance personAppearance;
    private PersonBehaviour personBehaviour;
    private float attackPower = 0.002f;
    private float selfHealRate = 0.0005f;

    public GameObject virusPrefab;
    public bool Aware { get; set; } = true;
    public int TimeInfected { get; set; } = 0;
    public int TimeSick { get; set; } = 0;
    public float Health { get; set; }

    private bool virusPresent = false;
    private VirusHealth virusHealth;
    private GameObject virus;

    private void Awake()
    {
        personAppearance = gameObject.GetComponent<PersonAppearance>();
        personBehaviour = gameObject.GetComponent<PersonBehaviour>();
        Health = Random.Range(0.8f, 1);
    }

    public void ContractVirus()
    {
        virus = Instantiate(virusPrefab);
        virus.name = "virus";
        virusPresent = true;
        virusHealth = virus.GetComponent<VirusHealth>();
        virus.transform.parent = transform;
        virus.transform.localPosition = Vector3.zero;
        Condition = HealthStatus.Infected;
        personAppearance.UpdateMaterial();
        
    }

    public void RemoveVirus()
    {
        Destroy(virus);
        virusPresent = false;
        virusHealth = null;
        Condition = HealthStatus.Cured;
        gameObject.layer = 8;
        personBehaviour.Walk();
        personAppearance.UpdateMaterial(); 
    }

    private void AttackVirus()
    {
        virusHealth.ReceiveAttack(attackPower);
    }

    private float CalculateAttackMultiplier()
    {
        switch (Needs)
        {
            case MedicalNeeds.None: return 1f;
            case MedicalNeeds.Hospital: return 3f;
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
        if (Health < 0.5) Needs = MedicalNeeds.Hospital;
        if (Health < 0.1) Needs = MedicalNeeds.Respirator;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        Condition = HealthStatus.Dead;
        personAppearance.UpdateMaterial();
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
            Health += selfHealRate * healMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelfHeal();
        if(virusPresent && Condition!=HealthStatus.Dead)
        {
            AttackVirus();
        }

        personAppearance.UpdateBar();

    }
}
