using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHealth : MonoBehaviour
{
    private float Health { get; set; } = 100;

    public void ReceiveAttack(float damage)
    {
        Health -= damage;
        Debug.Log(Health);
        if(Health < 0)
        {
            transform.parent.GetComponent<PersonHealth>().RemoveVirus();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
