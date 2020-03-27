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
    public Slider initialInfectionProportionSlider;
    public Slider hospitalCapacity;
    public Slider intensiveCareCapacity;
    public Slider levelToRequireHospitalSlider;
    public Text population;
    public Text numberOfDead;
    public Text numberOfCured;
    public Text numberOfInfected;
    public Text hospitalizations;

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
        initialInfectionProportionSlider.onValueChanged.AddListener(delegate { SetInitialInfectionProportion(); });
        levelToRequireHospitalSlider.onValueChanged.AddListener(delegate { SetLevelToRequireHospital(); });
        //virusMortality.onValueChanged.AddListener(delegate { SetVirusMortality(); });

        initialInfectionProportionSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("initialInfectionProportionSlider.value"));
        selfHealingSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("selfHealingSlider.value"));
        virusAggressivenessSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("virusAggressivenessSlider.value"));
        personReactionToVirusSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("personReactionToVirusSlider.value"));
        walkingSpeed.SetValueWithoutNotify(PlayerPrefs.GetFloat("walkingSpeed.value"));
        showHealthBar.SetValueWithoutNotify(PlayerPrefs.GetFloat("showHealthBar.value"));
        populationSize.SetValueWithoutNotify(PlayerPrefs.GetFloat("populationSize.value"));
        levelToRequireHospitalSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("levelToRequireHospitalSlider.value"));

        ChangeHealthBarDisplay();
        SetWalkingSpeed();
        SetInfectingWithoutSignsTime();
        SetVirusMortality();
        SetPersonReactionToVirus();
        SetSelfHealingPower();
        SetVirusAttackPower();
        SetPopulationSize();
        SetInitialInfectionProportion();
        SetLevelToRequireHospital();
    }

    private void SetLevelToRequireHospital()
    {
        Constants.LevelToRequireHospital = levelToRequireHospitalSlider.value;
        PlayerPrefs.SetFloat("levelToRequireHospitalSlider.value", levelToRequireHospitalSlider.value);
        Text levelToRequireHospitalLabel = GameObject.Find("LevelToRequireHospital").GetComponent<Text>();
        levelToRequireHospitalLabel.text = $"Level to require hospital: {Constants.LevelToRequireHospital.ToString("F3")}";
    }

    private void SetWalkingSpeed()
    {
        float walkingSpeedCalculated = Constants.MaxWalkingSpeed * walkingSpeed.value;
        if (walkingSpeedCalculated <= 0) walkingSpeedCalculated = 0.01f;
        Constants.WalkingSpeed = walkingSpeedCalculated;
        Text walkingSpeedLabel = GameObject.Find("WalkingSpeed").GetComponent<Text>();
        walkingSpeedLabel.text = $"Walking Speed - {walkingSpeed.value}";
        PlayerPrefs.SetFloat("walkingSpeed.value", walkingSpeed.value);
    }

    private void SetPersonReactionToVirus()
    {
        Text PersonReactionToVirusLabel = GameObject.Find("PersonReactionToVirus").GetComponent<Text>();

        Constants.PersonAttackPower = personReactionToVirusSlider.value * Constants.PersonAttackPowerMax;
        PersonReactionToVirusLabel.text = $"Reaction to Virus - {personReactionToVirusSlider.value}";
        PlayerPrefs.SetFloat("personReactionToVirusSlider.value", personReactionToVirusSlider.value);
    }

    private void SetVirusAttackPower()
    {
        Constants.VirusAttackPower = virusAggressivenessSlider.value * Constants.VirusAttackPowerMax;
        Text virusAggressivenessLabel = GameObject.Find("VirusAggressiveness").GetComponent<Text>();
        //virusAggressivenessLabel.text = $"Virus Aggressiviness - {Constants.VirusAttackPower.ToString("F3")}";
        virusAggressivenessLabel.text = $"Virus Aggressiviness - {virusAggressivenessSlider.value.ToString("F3")}";
        PlayerPrefs.SetFloat("virusAggressivenessSlider.value", virusAggressivenessSlider.value);
    }

    private void SetSelfHealingPower()
    {
        string sliderValue = ShowSliderValue ? selfHealingSlider.value.ToString("F3") : "";
        Text PersonSelfHealRateLabel = GameObject.Find("PersonSelfHealing").GetComponent<Text>();
        Constants.PersonSelfHealRate = selfHealingSlider.value * Constants.PersonSelfHealMaxRate;
        PersonSelfHealRateLabel.text = $"Self Healing {selfHealingSlider.value} ";
        PlayerPrefs.SetFloat("selfHealingSlider.value", selfHealingSlider.value);
    }

    private void SetInitialInfectionProportion()
    {
        Text InitialInfectionProportionLabel = GameObject.Find("InitialInfectionProportion").GetComponent<Text>();
        Constants.InitialInfectionProportion = initialInfectionProportionSlider.value * Constants.InitialInfectionProportionMax;
        InitialInfectionProportionLabel.text = $"% Initial Infection - {initialInfectionProportionSlider.value.ToString("F2")} - ({(int)(Constants.InitialInfectionProportion * Constants.InitialPopulation) })";
        //InitialInfectionProportionLabel.text = $"% Initial Infection - {Constants.InitialInfectionProportion.ToString("F3")}";
        PlayerPrefs.SetFloat("initialInfectionProportionSlider.value", initialInfectionProportionSlider.value);

    }

    

    private void ChangeHealthBarDisplay()
    {
        Text showHealthBarLabel = GameObject.Find("ShowHealthBar").GetComponent<Text>();
        int value = (int)showHealthBar.value;

        //GameObject.Find("Population").GetComponent<Pupulation>().ChangeStateHealthBars();


        switch (value)
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

        //print("Will call");
        GameObject.Find("Population").GetComponent<Pupulation>().ChangeStateHealthBars(Constants.HealthBarDisplay);
        PlayerPrefs.SetFloat("showHealthBar.value", showHealthBar.value);
        
        //print("Called");

    }

    private void SetPopulationSize()
    {
        Text populationSizeLabel = GameObject.Find("PopulationSize").GetComponent<Text>();

        float value = populationSize.value;

        int iPopulationSize = (int)(value * Constants.PopulationLimit);
        if (iPopulationSize < 1) iPopulationSize = 1;
        Constants.InitialPopulation = iPopulationSize;
        populationSizeLabel.text = $"Population Size - {iPopulationSize}";

        PlayerPrefs.SetFloat("populationSize.value", populationSize.value);
        //int populationSize = (int)populationSize.value;
    }

    private void SetVirusMortality()
    {
        //Constants.VirusMortalityRate = Constants.MaxVirusMortalityRate * virusMortality.value;
    }

    private void SetInfectingWithoutSignsTime()
    {
        Text InfectingWithoutSignsLabel = GameObject.Find("InfectingWithoutSigns").GetComponent<Text>();

        Constants.InfectingWithoutSignsTime = Constants.MaxInfectingWithoutSignsTime * infectingWithoutSigns.value;
        InfectingWithoutSignsLabel.text = $"Incubation Time: {infectingWithoutSigns.value}";
    }

    

    public void UpdateStats()
    {
        population.text = $"# Population: {Constants.InitialPopulation.ToString()}";
        numberOfInfected.text = $"# Infected: {Constants.NumberOfInfected.ToString()}";
        numberOfDead.text = $"# Dead: {Constants.NumberOfDead.ToString()}";
        numberOfCured.text = $"# Cured: {Constants.NumberOfCured.ToString()}";
        float hospitalizationProportion = ((float)Constants.hospitalizations / (float)Constants.InitialPopulation) * 100f;
        hospitalizations.text = $"# Hospitalizations: {Constants.hospitalizations.ToString()}({hospitalizationProportion.ToString("F2")}%)";
    }
}
