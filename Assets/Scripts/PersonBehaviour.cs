﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    private PersonHealth personHealth;
    private PersonAppearance personAppearance;
    private GameObject hospital;
    
    public bool ShouldGoToHospital { get; set; } = false;
    public HospitalBed Bed { get; set; }
    public bool HospitalAccess { get; set; } = false;
    public bool UnderTreatment = false;
    private Rigidbody2D rb;
    
    public void ExitHospital()
    {
        hospital.GetComponent<HospitalManagement>().RegisterExit(gameObject);
    }

    private void Start()
    {
        GenerateRandomAwareness();
        Walk();
        hospital = GameObject.Find("HospitalSquare");
    }

    private void GenerateRandomAwareness()
    {
        float chance = Random.Range(0f, 1f);
        personHealth.Aware = chance < Constants.BeAwareChance ? true : false;
    }

    private void GenerateRandomNecessityToGoToHospital()
    {
        float chance = Random.Range(0f, 1f);
        ShouldGoToHospital = chance < Constants.goToHospitalWhenSickChance ? true : false;
    }

    void Awake()
    {
        personHealth = gameObject.GetComponent<PersonHealth>();
        personAppearance = gameObject.GetComponent<PersonAppearance>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private bool IsInsideHospital()
    {
        return false;
    }

    private void GoToHospital()
    {
        WalkTo(Constants.HospitalPosition + new Vector3(Random.Range(-60, 60), Random.Range(-60,60), 0f));
    }


    void Update()
    {
        switch (personHealth.Condition)
        {
            case HealthStatus.Healthy :
            case HealthStatus.Cured:
                rb.velocity = rb.velocity.normalized * Constants.WalkingSpeed;
                break;

            case HealthStatus.Infected:
                

                personHealth.TimeInfected++;
                if(personHealth.Health < Constants.LevelToRequireHospital)
                {
                    personHealth.Condition = HealthStatus.Sick;
                    personAppearance.UpdateMaterial();
                    GenerateRandomNecessityToGoToHospital();

                }
                
                break;

            case HealthStatus.Sick:
                //health.Health -= virus.Damage;

                personHealth.TimeSick++; 

                if(ShouldGoToHospital && !UnderTreatment)
                {
                    //if already going to hospital, don't change layer
                    if(gameObject.layer !=9)
                    {
                        gameObject.layer = 12;
                    }
                    GoToHospital();
                    
                } else
                {
                    personHealth.Aware = true;
                }

                
                break;


            case HealthStatus.Dead:
                rb.velocity = Vector3.zero;
                break;
        }

        if (personHealth.Aware) rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gam)
        Walk();


        if (collision.gameObject.tag == "person" || collision.gameObject.tag == "Patient0")
        {
            PersonBehaviour otherPersonBehaviour = collision.gameObject.GetComponent<PersonBehaviour>();
            HealthStatus otherPersonCondition = otherPersonBehaviour.personHealth.Condition;
            HandlePersonsTouched(otherPersonCondition);
        }

        if (collision.gameObject.name == "HospitalBody" && personHealth.Condition == HealthStatus.Sick && gameObject.layer == 12 && !UnderTreatment)
        {
            collision.transform.root.gameObject.GetComponent<HospitalManagement>().AskAdmission(gameObject);            
        }
    }

    private void HandlePersonsTouched(HealthStatus otherPersonCondition)
    {
        if (personHealth.Condition == HealthStatus.Healthy)
        {
            if (otherPersonCondition == HealthStatus.Infected)
            {
                personHealth.ContractVirus();
                
                
            }
        }
    }

    public void Walk()
    {
        if (!personHealth.Aware)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomDirection = randomDirection.normalized;
            rb.velocity = randomDirection * Constants.WalkingSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }


    private void WalkTo(Vector3 address)
    {
        rb.velocity = (address - transform.position).normalized * Constants.WalkingSpeed;
    }

    
}
