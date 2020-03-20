using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupulation : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject healthBarPrefab;
    // Start is called before the first frame update

    public bool popQntOverride = false;
    public int populationQuantity = 1;
    

    void Start()
    {
        int numberOfPersons = popQntOverride ? populationQuantity : Constants.InitialPopulation;
        for(int i = 0; i < numberOfPersons; i++)
        {
           
            GameObject person = Instantiate(personPrefab);
            person.name = "person"+i;
            person.tag = "person";
            float xPos = Random.Range(-Constants.WorldSize.x, Constants.WorldSize.x);
            float yPos = Random.Range(-Constants.WorldSize.y, Constants.WorldSize.y);



            person.transform.position = new Vector2(xPos, yPos);
            person.transform.parent = transform;
            person.layer = 8;
            if (i == 0)
            {
                person.GetComponent<PersonHealth>().ContractVirus();
                //person.GetComponent<PersonHealth>().Condition = HealthStatus.Infected;
                
                person.tag = "Patient0";
                person.transform.position = new Vector3(300, -60, 0);
                
                GameObject healthBar = Instantiate(healthBarPrefab);
                healthBar.transform.parent = person.transform;
                healthBar.name = "HealthBar";
                healthBar.transform.localPosition = new Vector3(0f, 2.1f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
