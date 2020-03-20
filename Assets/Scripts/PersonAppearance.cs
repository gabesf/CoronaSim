using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAppearance : MonoBehaviour
{
    public Material sick;
    public Material healthy;
    public Material infected;
    public Material dead;
    public Material cured;


    private HealthBar healthBarScript;
    private new Renderer renderer;

    private PersonHealth personHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.Find("HealthBar"))
        {
            GameObject healthBar = transform.Find("HealthBar").gameObject;
            Debug.Log(healthBar.transform.parent.name);
            Debug.Log("Have a health bar");
            healthBarScript = healthBar.GetComponent<HealthBar>();
            healthBarScript.UpdateBar(personHealth.Health);
        } 
        UpdateMaterial();
    }

    private void Awake()
    {
        personHealth = gameObject.GetComponent<PersonHealth>();
        renderer = gameObject.GetComponent<Renderer>();
    }

    public void UpdateMaterial()
    {
        switch (personHealth.Condition)
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

            case HealthStatus.Cured:
                renderer.material = cured;
                break;

            case HealthStatus.Dead:
                renderer.material = dead;

                break;
        }
    }

    public void UpdateBar()
    {
        print(personHealth.Health);
        healthBarScript.UpdateBar(personHealth.Health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
