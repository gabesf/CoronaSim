using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HospitalManagement : MonoBehaviour
{
    public int Capacity { get; set; } = 100;
    public int Occupation { get; set; } = 0;

    public GameObject HealthBarPrefab;
    private HealthBar healthBarScript;
    public GameObject HospitalDisplayPrefab;

    private Text hospitalLabel;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject healthBar = Instantiate(HealthBarPrefab);
        GameObject hospitalDisplay = Instantiate(HospitalDisplayPrefab);
        
        transform.position = Constants.HospitalPosition;
        healthBar.transform.parent = transform;
        healthBar.transform.localPosition = Vector3.zero;
        healthBar.transform.localScale = new Vector3(1.5f, 2.0f, 1.0f);
        healthBar.AddComponent<BoxCollider2D>();
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateBar(0f);

        hospitalDisplay.transform.SetParent(transform, false);
        hospitalLabel = hospitalDisplay.GetComponentInChildren<Text>();
        hospitalLabel.transform.localPosition = healthBar.transform.position;
    }

    private void Start()
    {
        UpdateHospitalLabel();
        
    }

    public void AskAdmission(GameObject person)
    {
        if(Occupation < Capacity)
        {
            Occupation++;
            person.layer = 9;
            person.GetComponent<PersonBehaviour>().HospitalAccess = true;
            float OccupancyRatio = (float)Occupation / (float)Capacity;
            healthBarScript.UpdateBar(OccupancyRatio);
            UpdateHospitalLabel();
        }
    }

    private void UpdateHospitalLabel()
    {
        hospitalLabel.text = $"{Occupation} / {Capacity}";
    }

    public void RegisterExit()
    {
        Occupation--;
        float OccupancyRatio = (float)Occupation / (float)Capacity;
        healthBarScript.UpdateBar(OccupancyRatio);
        UpdateHospitalLabel();
    }
    




}
