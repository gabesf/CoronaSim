using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void ReloadScene()
    {
        print("Button pressed");
        SceneManager.LoadScene(0);
    }

    public void Repopulate()
    {
        GameObject population = GameObject.Find("Population");
        print(population.name);
        
            //print("will repopulate");
    }
}
