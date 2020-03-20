using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManagement : MonoBehaviour
{
    public int Capacity { get; set; } = 100;
    public int Occupation { get; set; } = 0;

    public GameObject HealthBarPrefab;
    private HealthBar healthBarScript;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        GameObject healthBar = Instantiate(HealthBarPrefab);
        healthBar.transform.parent = transform;
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateBar(0f);
    }

    public void AskAdmission(GameObject person)
    {
        if(Occupation < Capacity)
        {
            Occupation++;
            person.GetComponent<PersonBehaviour>().HospitalAccess = true;
            person.layer = 9;
            float OccupancyRatio = (float)Occupation / (float)Capacity;
            print($"Occupancy: {OccupancyRatio}");
            healthBarScript.UpdateBar(OccupancyRatio);
            
        }
    }

    


}
