using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupulation : MonoBehaviour
{
    public GameObject personPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < Constants.InitialPopulation; i++)
        {
           
            GameObject person = Instantiate(personPrefab);
            person.name = "person";
            float xPos = Random.Range(-Constants.WorldSize.x, Constants.WorldSize.x);
            float yPos = Random.Range(-Constants.WorldSize.y, Constants.WorldSize.y);

            person.transform.position = new Vector2(xPos, yPos);
            person.transform.parent = transform;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
