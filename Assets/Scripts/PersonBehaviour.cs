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

    private new Renderer renderer;
    private Rigidbody2D rb;


    private void Walk()
    {
        //print(gameObject.name);
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randomDirection = randomDirection.normalized;
        //print(rb.velocity);
        rb.velocity = randomDirection * Constants.MaxSpeed;
    }

    private void HandlePersonsTouched(HealthStatus otherPersonCondition)
    {
       if( Condition == HealthStatus.Healthy)
        {
            if(otherPersonCondition == HealthStatus.Infected || otherPersonCondition == HealthStatus.Sick)
            {
                Condition = HealthStatus.Infected;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gam)
        Walk();

        if(collision.gameObject.name == "Person")
        {
            PersonBehaviour otherPersonBehaviour = collision.gameObject.GetComponent<PersonBehaviour>();
            HealthStatus otherPersonCondition = otherPersonBehaviour.Condition;
            HandlePersonsTouched(otherPersonCondition);
        }

    }
    // Start is called before the first frame update
    void Awake()
    {
        //print("Created");
        renderer = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        switch(Condition)
        {
            case HealthStatus.Healthy:
                renderer.material = healthy;

                //Walk();
                Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                randomDirection = randomDirection.normalized;
                rb.velocity = randomDirection * Constants.MaxSpeed;

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

    

    // Update is called once per frame
    void Update()
    {
        switch(Condition)
        {
            case HealthStatus.Healthy:

                break;

            case HealthStatus.Infected:

                break;

            case HealthStatus.Sick:

                break;


            case HealthStatus.Dead:

                break;
        }
        
        
    }
}
