using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthStatus
{
    Healthy,
    Infected,
    Sick,
    Dead
}

public class PersonBehaviour : MonoBehaviour
{
    public Material sick;
    public Material healthy;
    public Material infected;
    public Material dead;

    public HealthStatus Condition { get; set; } = HealthStatus.Healthy;
    public bool Aware { get; set; } = true;

    private new Renderer renderer;
    private Rigidbody2D rb;

    public int TimeInfected { get; set; } = 0;
    public int TimeSick { get; set; } = 0;

    public float Health { get; set; } = 1000;


    private void Walk()
    {
        if (!Aware)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomDirection = randomDirection.normalized;
            rb.velocity = randomDirection * Constants.MaxSpeed;
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }
           

    private void WalkTo(Vector3 address)
    {
        rb.velocity = (address - transform.position).normalized * Constants.MaxSpeed;
    }

    private void UpdateMaterial()
    {
        switch (Condition)
        {
            case HealthStatus.Healthy:
                renderer.material = healthy;
                break;

            case HealthStatus.Infected:
                renderer.material = infected;
                break;

            case HealthStatus.Sick:
                renderer.material = sick;
                break;


            case HealthStatus.Dead:
                renderer.material = dead;

                break;
        }
    }

   


    

    private void Start()
    {
        UpdateMaterial();

        GenerateRandomAwareness();
        Walk();


    }

    private void GenerateRandomAwareness()
    {
        float chance = Random.Range(0f, 1f);
        Aware = chance < Constants.BeAwareChance ? true : false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    private bool IsInsideHospital()
    {
        return false;
    }

    private void GoToHospital()
    {
        WalkTo(Constants.HospitalPosition);
    }

    private void BeAttended()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(Condition)
        {
            case HealthStatus.Healthy:

                break;

            case HealthStatus.Infected:
                TimeInfected ++;
                if(TimeInfected > Constants.infectionTimeLimit)
                {
                    Condition = HealthStatus.Sick;
                    UpdateMaterial();
                }
                break;

            case HealthStatus.Sick:
                TimeSick++; 
                if(IsInsideHospital())
                {

                    BeAttended();
                } else
                {
                    GoToHospital();
                    

                    
                }
                break;


            case HealthStatus.Dead:

                break;
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gam)
        Walk();


        if (collision.gameObject.name == "person")
        {
            PersonBehaviour otherPersonBehaviour = collision.gameObject.GetComponent<PersonBehaviour>();
            HealthStatus otherPersonCondition = otherPersonBehaviour.Condition;
            HandlePersonsTouched(otherPersonCondition);
        }
    }

    private void HandlePersonsTouched(HealthStatus otherPersonCondition)
    {
        if (Condition == HealthStatus.Healthy)
        {
            if (otherPersonCondition == HealthStatus.Infected)
            {
                Condition = HealthStatus.Infected;
                UpdateMaterial();
            }
        }
    }
}
