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
    GameObject healthBar;
    private new Renderer renderer;
    private PersonHealth personHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.Find("HealthBar"))
        {
            healthBar = gameObject.transform.Find("HealthBar").gameObject;
            //Debug.Log(healthBar.transform.parent.name);
            //Debug.Log("Have a health bar");
            healthBarScript = healthBar.GetComponent<HealthBar>();
            healthBarScript.SetColor(Color.red);
            healthBarScript.UpdateBar(personHealth.Health);

        } 
        UpdateMaterial();
    }

    public void SetBarActive(bool active)
    {
        if(!healthBar)
        {
            Start();
        }
        healthBar.SetActive(active);


    }

    private void Awake()
    {
        //healthBar = transform.Find("HealthBar").gameObject;
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
                renderer.material.color = Color.blue;
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
        //print(personHealth.Health);
        if(healthBarScript)
        {
            healthBarScript.UpdateBar(personHealth.Health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
