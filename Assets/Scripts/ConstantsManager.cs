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
    public Slider showHealthBar;
    public Slider selfHealingSlider;
    public Slider virusAggressivenessSlider;
    public Slider personReactionToVirusSlider;

    private bool ShowSliderValue = true;
    public void Start()
    {
        walkingSpeed.onValueChanged.AddListener(delegate { SetWalkingSpeed(); } );
        infectingWithoutSigns.onValueChanged.AddListener(delegate { SetInfectingWithoutSignsTime(); });
        populationSize.onValueChanged.AddListener(delegate { SetPopulationSize(); });
        showHealthBar.onValueChanged.AddListener(delegate { ChangeHealthBarDisplay(); });
        personReactionToVirusSlider.onValueChanged.AddListener(delegate { SetPersonReactionToVirus(); });
        virusAggressivenessSlider.onValueChanged.AddListener(delegate { SetVirusAttackPower(); });
        selfHealingSlider.onValueChanged.AddListener(delegate { SetSelfHealingPower(); });
        //virusMortality.onValueChanged.AddListener(delegate { SetVirusMortality(); });

        ChangeHealthBarDisplay();
        SetWalkingSpeed();
        SetInfectingWithoutSignsTime();
        SetVirusMortality();
        SetPersonReactionToVirus();
        SetSelfHealingPower();
        SetVirusAttackPower();
        SetPopulationSize();
    }

    private void SetSelfHealingPower()
    {
        string sliderValue = ShowSliderValue ? selfHealingSlider.value.ToString("F3") : "";
        Text PersonSelfHealRateLabel = GameObject.Find("PersonSelfHealing").GetComponent<Text>();
        Constants.PersonSelfHealRate = selfHealingSlider.value * Constants.PersonSelfHealMaxRate;
        PersonSelfHealRateLabel.text = $"Self Healing {Constants.PersonSelfHealRate.ToString("F3")} " + sliderValue;

    }

    private void SetVirusAttackPower()
    {
      
        Constants.VirusAttackPower = virusAggressivenessSlider.value * Constants.VirusAttackPowerMax;
        Text virusAggressivenessLabel = GameObject.Find("VirusAggressiveness").GetComponent<Text>();
        virusAggressivenessLabel.text = $"Virus Aggressiviness - {Constants.VirusAttackPower.ToString("F3")}";
    }

    private void SetPersonReactionToVirus()
    {
        Text PersonReactionToVirusLabel = GameObject.Find("PersonReactionToVirus").GetComponent<Text>();

        Constants.PersonAttackPower = personReactionToVirusSlider.value * Constants.PersonAttackPowerMax;
        PersonReactionToVirusLabel.text = $"Reaction to Virus - {Constants.PersonAttackPower.ToString("F3")}";
    }


    private void ChangeHealthBarDisplay()
    {
        Text showHealthBarLabel = GameObject.Find("ShowHealthBar").GetComponent<Text>();
        int value = (int)showHealthBar.value;

        

        switch(value)
        {
            case 0:
                Constants.HealthBarDisplay = ShowHealthBar.none;
                showHealthBarLabel.text = "Show Health Bar - None";

                break;
            case 1:
                Constants.HealthBarDisplay = ShowHealthBar.infected;
                showHealthBarLabel.text = "Show Health Bar - Infected";

                break;
            case 2:
                Constants.HealthBarDisplay = ShowHealthBar.all;
                showHealthBarLabel.text = "Show Health Bar - All";
                break;
            case 3:
                Constants.HealthBarDisplay = ShowHealthBar.OnClick;
                showHealthBarLabel.text = "Show Health Bar - On Click";
                break;


        }

        GameObject.Find("Population").GetComponent<Pupulation>().ChangeStateHealthBars(Constants.HealthBarDisplay);


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
        Text walkingSpeedLabel = GameObject.Find("WalkingSpeed").GetComponent<Text>();
        walkingSpeedLabel.text = $"Walking Speed - {Constants.WalkingSpeed.ToString("F3")}";


    }
}
