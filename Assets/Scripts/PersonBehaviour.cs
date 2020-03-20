using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class PersonBehaviour : MonoBehaviour
{
    private PersonHealth personHealth;
    private PersonAppearance personAppearance;
    //create a health quantity (relatively random) that is depleted by the virus
    //if the quantity is bellow some arbitrary level (go to hospital, go to intensive care) the person needs 
    //to go to the specified area or it's health depletes faster. If it goes to zero, the person goes to the morge.

   

    //private GameObject healthBar;
    

    
    public bool ShouldGoToHospital { get; set; } = false;

    public bool HospitalAccess { get; set; } = false;

    
    private Rigidbody2D rb;

    

    

    

    private void Start()
    {
        GenerateRandomAwareness();
        Walk();
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

    // Start is called before the first frame update
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
        WalkTo(Constants.HospitalPosition);
    }

    
    void Update()
    {
        switch(personHealth.Condition)
        {
            case HealthStatus.Healthy:
                rb.velocity = rb.velocity.normalized * Constants.WalkingSpeed;
                break;

            case HealthStatus.Infected:
                

                personHealth.TimeInfected++;
                if(personHealth.TimeInfected > Constants.InfectingWithoutSignsTime)
                {
                    personHealth.Condition = HealthStatus.Sick;
                    GenerateRandomNecessityToGoToHospital();
                    personAppearance.UpdateMaterial();
                }
                break;

            case HealthStatus.Sick:
                //health.Health -= virus.Damage;

                personHealth.TimeSick++; 

                if(ShouldGoToHospital)
                {
                    //if already going to hospital, don't change layer
                    if(gameObject.layer !=9)
                    {
                        gameObject.layer = 12;

                    }
                    if (IsInsideHospital())
                    {

                        
                    }
                    else
                    {
                        GoToHospital();



                    }
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


        if (collision.gameObject.name == "person")
        {
            PersonBehaviour otherPersonBehaviour = collision.gameObject.GetComponent<PersonBehaviour>();
            HealthStatus otherPersonCondition = otherPersonBehaviour.personHealth.Condition;
            HandlePersonsTouched(otherPersonCondition);
        }

        if (collision.gameObject.name == "HospitalBody" && personHealth.Condition == HealthStatus.Sick && gameObject.layer == 12 )
        {
            print("Asking Admission");
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

    private void Walk()
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
