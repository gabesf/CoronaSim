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
        transform.position = Constants.HospitalPosition;
        healthBar.transform.parent = transform;
        healthBar.transform.localPosition = Vector3.zero;
        healthBar.transform.localScale = new Vector3(1.5f, 2.0f, 1.0f);
        healthBar.AddComponent<BoxCollider2D>();
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
