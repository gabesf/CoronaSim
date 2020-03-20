using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalCreation : MonoBehaviour
{
    //public GameObject hospitalBoundaryPrefab;
    //private GameObject hospitalBoundary;
    float currentScale = 0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = Constants.HospitalPosition;
        //hospitalBoundary = Instantiate(hospitalBoundaryPrefab);
        //hospitalBoundary.transform.parent = transform;
        //hospitalBoundary.transform.localPosition = Vector3.zero;
        
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
