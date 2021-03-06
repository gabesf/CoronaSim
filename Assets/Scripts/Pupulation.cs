﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupulation : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject healthBarPrefab;
    // Start is called before the first frame update

    

    void Start()
    {
        
        for(int i = 0; i < Constants.InitialPopulation; i++)
        {
           
            GameObject person = Instantiate(personPrefab);
            person.name = "person";
            float xPos = Random.Range(-Constants.WorldSize.x, Constants.WorldSize.x);
            float yPos = Random.Range(-Constants.WorldSize.y, Constants.WorldSize.y);



            person.transform.position = new Vector2(xPos, yPos);
            person.transform.parent = transform;
            person.layer = 8;
            if (i == 0)
            {
                person.GetComponent<PersonBehaviour>().Condition = HealthStatus.Infected;
                person.tag = "Patient0";
                person.transform.position = new Vector3(300, -60, 0);
                
                GameObject healthBar = Instantiate(healthBarPrefab);
                healthBar.transform.parent = person.transform;
                healthBar.name = "HealthBar";
                healthBar.transform.localPosition = new Vector3(0f, 2f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
