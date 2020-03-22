using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGenerator : MonoBehaviour
{
    private Transform bar;
    private SpriteRenderer barSprite;
    public GameObject verticalBarPrefab;
    private float currentTime = 0f;
    private float timeIncrement = 0.01f;

    private float maxHeight = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        //bar = transform.Find("Bar");
        //bar.localScale = new Vector3(0.4f, 1f, 1f);
        //barSprite = bar.Find("BarSprite").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentTime += timeIncrement;
        GameObject verticalBar = Instantiate(verticalBarPrefab);

        verticalBar.transform.parent = transform;
        verticalBar.transform.localPosition = new Vector3(currentTime, 0, 0);


    }


}
