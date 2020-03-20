﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupulation : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject healthBarPrefab;
    // Start is called before the first frame update

    public bool popQntOverride = false;
    public int populationQuantity = 1;
    
    public void ChangeStateHealthBars(ShowHealthBar showHealthBar)
    {
        bool showAll = false;
        if(showHealthBar == ShowHealthBar.all)
        {
            showAll = true;
        }
        foreach (Transform t in transform)
        {
            Transform healthBarTransform = t.Find("HealthBar");
            healthBarTransform.gameObject.SetActive(showAll);
        }
    }

    public void Populate()
    {
        int numberOfPersons = popQntOverride ? populationQuantity : Constants.InitialPopulation;
        for (int i = 0; i < numberOfPersons; i++)
        {

            GameObject person = Instantiate(personPrefab);
            person.name = "person" + i;
            person.tag = "person";
            float xPos = Random.Range(-Constants.WorldSize.x, Constants.WorldSize.x);
            float yPos = Random.Range(-Constants.WorldSize.y, Constants.WorldSize.y);



            person.transform.position = new Vector2(xPos, yPos);
            person.transform.parent = transform;
            person.layer = 8;


            GameObject healthBar = Instantiate(healthBarPrefab);
            healthBar.transform.parent = person.transform;
            healthBar.name = "HealthBar";
            healthBar.transform.localPosition = new Vector3(0f, 2.1f, 0f);
            healthBar.SetActive(false);
            
            if(Constants.HealthBarDisplay == ShowHealthBar.all)
            {
                healthBar.SetActive(true);
            }
            if (i == 0)
            {
                person.GetComponent<PersonHealth>().ContractVirus();
                //person.GetComponent<PersonHealth>().Condition = HealthStatus.Infected;

                person.tag = "Patient0";
                person.transform.position = new Vector3(300, -60, 0);

                
            }
        }
    }

    public void RemoveAllPeople()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
            //print(t.name);
        }
    }

    void Start()
    {
        Populate();
    }

    // Update is called once per frame

}
