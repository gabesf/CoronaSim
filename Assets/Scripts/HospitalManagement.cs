using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class HospitalManagement : MonoBehaviour
{
    public int Capacity { get; set; } = 20;
    public int Occupation { get; set; } = 0;

    public GameObject HealthBarPrefab;
    private HealthBar healthBarScript;
    public GameObject HospitalDisplayPrefab;
    public GameObject hospitalBedPrefab;
    public GameObject bedsStartingPos;
    private Text hospitalLabel;
    private List<HospitalBed> beds;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject healthBar = Instantiate(HealthBarPrefab);
        GameObject hospitalDisplay = Instantiate(HospitalDisplayPrefab);
        
        transform.position = Constants.HospitalPosition;
        healthBar.transform.parent = transform;
        healthBar.transform.localPosition = new Vector3(0,-60,0);
        healthBar.transform.localScale = new Vector3(1.5f, 2.0f, 1.0f);
        healthBar.AddComponent<BoxCollider2D>();
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.UpdateBar(0f);

        hospitalDisplay.transform.SetParent(transform, false);
        hospitalLabel = hospitalDisplay.GetComponentInChildren<Text>();
        hospitalLabel.transform.localPosition = healthBar.transform.position;
    }

    public void Reset()
    {
        Occupation = 0;
        ArrangeHospitalBeds();
        UpdateHospitalLabel();

    }

    private void Start()
    {
        UpdateHospitalLabel();
        ArrangeHospitalBeds();
        
    }

    

    private void ArrangeHospitalBeds()
    {
        beds = new List<HospitalBed>();

        int rows = 10;
        int columns = 2;

        float distance = 19f;
        for (int i = 0; i < Capacity; i++)
        {
            HospitalBed bed = new HospitalBed();
            
            float xpos = i % rows;
            float ypos = i / columns;

            Vector3 bedPosition  = new Vector3(xpos * distance - 85f, -ypos * distance + 85f, -1);
            bed.position = bedPosition;
            bed.Number = i;
            beds.Add(bed);
        }
    }

    private HospitalBed GetFreeBed()
    {

        int startingPos = Random.Range(0, Capacity);
        int counter = 0;
        foreach (HospitalBed bed in beds)
        {
            //print(beds.Count);
            if (bed.free)
            {
                bed.free = false;
                return bed;
            }
            counter++;
        }
        print("error");
        return beds[0];
    }

    public void AskAdmission(GameObject person)
    {
        if(Occupation < Capacity)
        {
            Occupation++;
            Constants.hospitalizations++;
            person.layer = 9;
            HospitalBed hospitalBed = GetFreeBed();
            person.transform.localPosition = hospitalBed.position;
            person.GetComponent<PersonBehaviour>().UnderTreatment = true;
            person.GetComponent<PersonBehaviour>().Bed = hospitalBed;

            //person.GetComponent<PersonBehaviour>().HospitalAccess = true;
            float OccupancyRatio = (float)Occupation / (float)Capacity;
            healthBarScript.UpdateBar(OccupancyRatio);
            UpdateHospitalLabel();
        }
    }

    public void RegisterExit(GameObject person)
    {
        //person.GetComponent<PersonBehaviour>().Bed.free = true;
        print("Will make empty the bed #" + person.GetComponent<PersonBehaviour>().Bed.Number);
        beds[person.GetComponent<PersonBehaviour>().Bed.Number].free = true;
        Occupation--;
        UpdateHospitalLabel();
        //print("will register exit of patient");
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
