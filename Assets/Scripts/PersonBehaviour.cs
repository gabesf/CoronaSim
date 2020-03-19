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

public class Virus
{
    public float Health { get; set; } = 1000;

    public float Damage { get; set; } = Constants.VirusMortalityRate;


}



public class PersonBehaviour : MonoBehaviour
{

    //create a health quantity (relatively random) that is depleted by the virus
    //if the quantity is bellow some arbitrary level (go to hospital, go to intensive care) the person needs 
    //to go to the specified area or it's health depletes faster. If it goes to zero, the person goes to the morge.

    public Material sick;
    public Material healthy;
    public Material infected;
    public Material dead;

    //private GameObject healthBar;
    private HealthBar healthBarScript;

    public HealthStatus Condition { get; set; } = HealthStatus.Healthy;
    public bool Aware { get; set; } = true;
    public bool ShouldGoToHospital { get; set; } = false;

    public bool HospitalAccess { get; set; } = false;

    private new Renderer renderer;
    private Rigidbody2D rb;

    public int TimeInfected { get; set; } = 0;
    public int TimeSick { get; set; } = 0;

    public float Health { get; set; }

    private Virus virus;

    

    private void Start()
    {
        virus = new Virus();

        GameObject healthBar = GameObject.Find("HealthBar");
        if (healthBar) healthBarScript = healthBar.GetComponent<HealthBar>();
        Health = Random.Range(500, 1000);
        healthBarScript.UpdateBar(Health);
        UpdateMaterial();

        GenerateRandomAwareness();
        Walk();


    }

    private void GenerateRandomAwareness()
    {
        float chance = Random.Range(0f, 1f);
        Aware = chance < Constants.BeAwareChance ? true : false;
    }

    private void GenerateRandomNecessityToGoToHospital()
    {
        float chance = Random.Range(0f, 1f);
        ShouldGoToHospital = chance < Constants.goToHospitalWhenSickChance ? true : false;
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

    
    void Update()
    {
        

        switch(Condition)
        {
            case HealthStatus.Healthy:
                rb.velocity = rb.velocity.normalized * Constants.WalkingSpeed;
                break;

            case HealthStatus.Infected:
                Health -= virus.Damage;

                TimeInfected ++;
                if(TimeInfected > Constants.InfectingWithoutSignsTime)
                {
                    Condition = HealthStatus.Sick;
                    GenerateRandomNecessityToGoToHospital();
                    UpdateMaterial();
                }
                break;

            case HealthStatus.Sick:
                Health -= virus.Damage;

                TimeSick++; 

                if(ShouldGoToHospital)
                {
                    //gameObject.layer = 12;
                    if (IsInsideHospital())
                    {

                        
                    }
                    else
                    {
                        GoToHospital();



                    }
                } else
                {
                    Aware = true;
                }

                
                break;


            case HealthStatus.Dead:

                break;
        }

        if (Aware) rb.velocity = Vector2.zero;
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

        if (collision.gameObject.name == "Hospital" && Condition == HealthStatus.Sick)
        {
            collision.gameObject.GetComponent<HospitalManagement>().AskAdmission(gameObject);
            
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

    private void Walk()
    {
        if (!Aware)
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
}
