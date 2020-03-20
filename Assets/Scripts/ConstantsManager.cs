using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstantsManager : MonoBehaviour
{
    public GameObject Canvas;

    public Slider walkingSpeed;
    public Slider infectingWithoutSigns;
    public Slider virusMortality;
    public Slider populationSize;

    public void Start()
    {
        walkingSpeed.onValueChanged.AddListener(delegate { SetWalkingSpeed(); } );
        infectingWithoutSigns.onValueChanged.AddListener(delegate { SetInfectingWithoutSignsTime(); });
        populationSize.onValueChanged.AddListener(delegate { SetPopulationSize(); });
        //virusMortality.onValueChanged.AddListener(delegate { SetVirusMortality(); });

        SetWalkingSpeed();
        SetInfectingWithoutSignsTime();
        SetVirusMortality();
    }

    private void SetPopulationSize()
    {
        Text populationSizeLabel = GameObject.Find("PopulationSize").GetComponent<Text>();

        float value = populationSize.value;

        int iPopulationSize = (int)(value * Constants.PopulationLimit);
        Constants.InitialPopulation = iPopulationSize;
        populationSizeLabel.text = $"Population Size - {iPopulationSize}";
        //int populationSize = (int)populationSize.value;
    }

    private void SetVirusMortality()
    {
        //Constants.VirusMortalityRate = Constants.MaxVirusMortalityRate * virusMortality.value;
    }

    private void SetInfectingWithoutSignsTime()
    {
        Constants.InfectingWithoutSignsTime = Constants.MaxInfectingWithoutSignsTime * infectingWithoutSigns.value;
    }

    private void SetWalkingSpeed()
    {
        float walkingSpeedCalculated = Constants.MaxWalkingSpeed * walkingSpeed.value;
        if (walkingSpeedCalculated <= 0) walkingSpeedCalculated = 0.01f;
        Constants.WalkingSpeed = walkingSpeedCalculated;


    }
}
