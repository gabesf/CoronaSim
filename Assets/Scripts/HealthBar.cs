using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private SpriteRenderer barSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(0.4f, 1f, 1f);
        barSprite = bar.Find("BarSprite").GetComponent<SpriteRenderer>();
    }


    private void UpdateColor(float sizeNormalized)
    {
        barSprite.color = Color.red;
    }

    private void SetSize(float sizeNormalized)
    {
        //print($"Inside {transform.root}");
        bar.localScale = new Vector3(sizeNormalized, 1);
    }

    public void UpdateBar(float sizeNormalized)
    {
        //print( transform.root.name);
        //print("Updating " + sizeNormalized);
        SetSize(sizeNormalized);
        UpdateColor(sizeNormalized);
    }
}
