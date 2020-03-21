using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalBodyManagement : MonoBehaviour
{
    //public GameObject hospitalBoundaryPrefab;
    //private GameObject hospitalBoundary;
    float currentScale = 0.0001f;
    HospitalManagement hospitalManagementScript;
    // Start is called before the first frame update
    void Start()
    {

        hospitalManagementScript = transform.root.gameObject.GetComponent<HospitalManagement>();
        //transform.position = Constants.HospitalPosition;
        //hospitalBoundary = Instantiate(hospitalBoundaryPrefab);
        //hospitalBoundary.transform.parent = transform;
        //hospitalBoundary.transform.localPosition = Vector3.zero;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject person = collision.gameObject;

        if(person.layer == 8)
        {
            PersonBehaviour personBehaviour = person.GetComponent<PersonBehaviour>();

            if(personBehaviour.HospitalAccess)
            {
                hospitalManagementScript.RegisterExit();
                personBehaviour.HospitalAccess = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentScale = 199;
        if(currentScale < 200)
        {
            currentScale *= 1.03f;
            Vector3 increasedHospitalBoundary = new Vector3(currentScale, currentScale, currentScale);
            transform.localScale = increasedHospitalBoundary;

        } else
        {
            currentScale = 200;
        }
    }
}
