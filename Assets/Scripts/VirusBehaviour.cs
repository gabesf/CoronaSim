﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private PersonHealth personHealth;
    //private float attackPower = Constants.VirusAttackPower;

    private void Awake()
    {
        
    }

    private void Start()
    {
        personHealth = transform.parent.GetComponent<PersonHealth>();
        //
        //print($"Inside {transform.parent.name}" );


    }

    private void AttackPerson()
    {
        
            personHealth.ReceiveAttack(Constants.VirusAttackPower);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (personHealth.Condition != HealthStatus.Dead)
        {
            AttackPerson();
        }
        
    }
}