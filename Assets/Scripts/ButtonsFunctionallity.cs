using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctionallity : MonoBehaviour
{
    private Pupulation populationScript;
    public void ReloadScene()
    {
        print("Button pressed");
        SceneManager.LoadScene(0);
    }

    public void Repopulate()
    {
        GameObject population = GameObject.Find("Population");
        populationScript = population.GetComponent<Pupulation>();
        populationScript.RemoveAllPeople();
        populationScript.Populate();
            //print("will repopulate");
    }
}
