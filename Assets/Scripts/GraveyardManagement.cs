using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardManagement : MonoBehaviour
{
    int horizontalInterval = 10;
    int verticalInterval = 10;
    int rows = 39;
    int columns = 5;

    public int Capacity { get; set; } 
    public int Occupation { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        Capacity = CalculateCapacity();
        print($"Capacity = {Capacity}");
    }

    private int CalculateCapacity()
    {
        return rows * columns;
    }

    public void AskAdmission(GameObject person)
    {
        print("Asking admission");

        if (Occupation < Capacity)
        {
            

            Vector3 gravePosition = GetGravePosition();
            Occupation++;
            person.transform.parent = transform;
            person.transform.position = transform.position + gravePosition;
        }
        else
        {
            print("Graveyard is full.");
        }
    }

    private Vector3 GetGravePosition()
    {
        int row = Occupation % columns;
        int column = Occupation / columns;
        print($"The position is row = {row} and column = {column}");

        return new Vector3(row * verticalInterval, -column * horizontalInterval);
    
    }
}
