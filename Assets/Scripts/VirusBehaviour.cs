using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private PersonHealth personHealth;

    private void Awake()
    {
        personHealth = transform.parent.GetComponent<PersonHealth>();
    }

    private void AttackPerson()
    {
        personHealth.ReceiveAttack(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
