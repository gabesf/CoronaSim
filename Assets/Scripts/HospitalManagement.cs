using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManagement : MonoBehaviour
{
    public int Capacity { get; set; } = 100;
    public int Occupation { get; set; } = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AskAdmission(GameObject person)
    {
        if(Occupation < Capacity)
        {
            Occupation++;
            person.GetComponent<PersonBehaviour>().HospitalAccess = true;
            person.layer = 9;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
