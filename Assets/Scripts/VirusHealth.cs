using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHealth : MonoBehaviour
{
    private VirusAppearance virusAppearance;
    private float Health { get; set; } = 1;

    public void ReceiveAttack(float damage)
    {
        Health -= damage;
        //Debug.Log(Health);
        if(Health < 0)
        {
            transform.parent.GetComponent<PersonHealth>().RemoveVirus();
        }

        virusAppearance.UpdateBar(Health);
    }

    // Start is called before the first frame update
    void Start()
    {
        virusAppearance = gameObject.GetComponent<VirusAppearance>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
