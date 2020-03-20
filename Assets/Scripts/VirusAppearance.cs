using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAppearance : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    private HealthBar healthBarScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject person = transform.parent.gameObject;
        if (person.transform.Find("HealthBar"))
        {
            healthBar = Instantiate(healthBarPrefab);
            healthBar.transform.parent = transform.parent;
            healthBar.name = "Virus HealthBar";
            healthBar.transform.localPosition = new Vector3(0, 1.25f, 0f);
            healthBarScript = healthBar.GetComponent<HealthBar>();
            healthBarScript.SetColor(Color.green);
        }
    }

    public void UpdateBar(float healthNormalized)
    {
        //print(personHealth.Health);
        if (healthBarScript)
        {
            healthBarScript.UpdateBar(healthNormalized);
        }
    }
}
