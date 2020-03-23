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
        Constants.NumberOfInfected = 0;
        Constants.NumberOfDead = 0;
        Constants.NumberOfCured = 0;
        GameObject.Find("ConstantManager").GetComponentInChildren<ConstantsManager>().UpdateStats();

        populationScript.RemoveAllPeople();
        GameObject.Find("HospitalSquare").GetComponent<HospitalManagement>().Reset();
        populationScript.Populate();

            //print("will repopulate");
    }
}
