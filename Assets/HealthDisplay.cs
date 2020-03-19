using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject HealthQuantity;
    // Start is called before the first frame update
    
        //Scale = 5     -> Position = 2
        //Scale = 1     -> Position = 0
        //Scale = 2.50  -> Position = 0.75
        //Scale = 0.25  -> Position = -0.25

    void UpdateHealthQuantity(float percentual)
    {
        float position =1 ;
        //float scale = 1;
        float scale = percentual * 2.5f;
        //HealthQuantity.transform.localPosition = new Vector3(position, 0, 0);
        HealthQuantity.transform.localScale = new Vector3(scale, 1f, 1f);
    }

    private void Update()
    {

        
    }
}
