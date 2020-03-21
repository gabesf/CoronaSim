using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupulation : MonoBehaviour
{
    public GameObject personPrefab;
    public GameObject healthBarPrefab;
    // Start is called before the first frame update

    public bool popQntOverride = false;
    public int populationQuantity = 1;
    
    public void ChangeStateHealthBars(ShowHealthBar showHealthBar)
    {
        List<Transform> persons = GetAllPersons();

        switch (showHealthBar)
        {
            case ShowHealthBar.none:
                foreach (Transform person in persons)
                {
                    person.gameObject.GetComponent<PersonAppearance>().SetBarActive(false);
                    if (person.gameObject.GetComponent<PersonHealth>().VirusPresent)
                    {
                        Debug.Log("will deactivate");
                        person.gameObject.GetComponentInChildren<VirusAppearance>().SetBarActive(false);
                        //person.gameObject.GetComponent<VirusAppearance>().SetBarActive(false);
                    }
                }
                break;

            case ShowHealthBar.all:
                foreach (Transform person in persons)
                {
                    person.gameObject.GetComponent<PersonAppearance>().SetBarActive(true);
                    if (person.gameObject.GetComponent<PersonHealth>().VirusPresent)
                    {
                        person.gameObject.GetComponentInChildren<VirusAppearance>().SetBarActive(true);
                    }
                }
                break;

            case ShowHealthBar.infected:
                foreach (Transform person in persons)
                {
                    if (person.gameObject.GetComponent<PersonHealth>().VirusPresent)
                    {
                        person.gameObject.GetComponent<PersonAppearance>().SetBarActive(true);
                        person.gameObject.GetComponentInChildren<VirusAppearance>().SetBarActive(true);
                    }
                }
                break;
        }

        //bool showAll = false;
        //if(showHealthBar == ShowHealthBar.all)
        //{
        //    showAll = true;
        //}
        //foreach (Transform t in transform)
        //{
        //    Transform healthBarTransform = t.Find("HealthBar");
        //    healthBarTransform.gameObject.SetActive(showAll);
        //}
    }

    public void ChangePersonSize()
    {
        List<Transform> persons = GetAllPersons();
        foreach (Transform t in persons)
        {
            transform.localScale = Constants.PersonSize;
        }
    }

    public void Populate()
    {
        int numberOfPersons = popQntOverride ? populationQuantity : Constants.InitialPopulation;
        for (int i = 0; i < numberOfPersons; i++)
        {

            GameObject person = Instantiate(personPrefab);
            person.name = "person" + i;
            person.tag = "person";
            float xPos = Random.Range(-Constants.WorldSize.x, Constants.WorldSize.x);
            float yPos = Random.Range(-Constants.WorldSize.y, Constants.WorldSize.y);



            person.transform.position = new Vector2(xPos, yPos);
            person.transform.parent = transform;
            person.layer = 8;


            GameObject healthBar = Instantiate(healthBarPrefab);
            healthBar.transform.parent = person.transform;
            healthBar.name = "HealthBar";
            healthBar.transform.localPosition = new Vector3(0f, 2.1f, 0f);
            healthBar.SetActive(false);
            
            if(Constants.HealthBarDisplay == ShowHealthBar.all)
            {
                healthBar.SetActive(true);
            }

            
            //if (i == 0)
            //{
            //    person.GetComponent<PersonHealth>().ContractVirus();
            //    //person.GetComponent<PersonHealth>().Condition = HealthStatus.Infected;
            //
            //    person.tag = "Patient0";
            //    person.transform.position = new Vector3(300, -60, 0);
            //
            //    
            //}
        }
        InfectPopulation();
    }

    private List<Transform> GetAllPersons()
    {
        List<Transform> persons = new List<Transform>();
        foreach (Transform t in transform)
        {
            if (t.tag == "person")
            {
                persons.Add(t);
            }
        }
        return persons;
    }

    private void InfectPopulation()
    {
        List<Transform> persons = GetAllPersons();
        

        int numberOfInitialInfected = (int)(persons.Count * Constants.InitialInfectionProportion);
        if (numberOfInitialInfected < 1) numberOfInitialInfected = 1;

        print($"There are {persons.Count} and {numberOfInitialInfected} should be infected");
        List<int> personsIdToBeInfected = new List<int>();

        while(personsIdToBeInfected.Count < numberOfInitialInfected)
        {
            int personId = Random.Range(1, persons.Count);
            if (!personsIdToBeInfected.Contains(personId)) personsIdToBeInfected.Add(personId);
        }

        print($"There are {personsIdToBeInfected.Count} in personsToBeInfected");

        foreach (int personId in personsIdToBeInfected)
        {
            
            print("Will infect " + personId);
            persons[personId].gameObject.GetComponent<PersonHealth>().ContractVirus();

        }


    }

    public void RemoveAllPeople()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
            //print(t.name);
        }
    }

    void Start()
    {
        Populate();
    }

    // Update is called once per frame

}
