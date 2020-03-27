using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Dropdown presetDropdown;

    public Text population;
    public Text numberOfDead;
    public Text numberOfCured;
    public Text numberOfInfected;
    public Text hospitalizations;

    public Button loadButton;
    public Button saveButton;
    public Button repopulateButton;
    public Button resetButton;

    private Pupulation populationScript;

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
        presetDropdown.onValueChanged.AddListener(delegate { SetPresetDropdownSelection(); });
        repopulateButton.onClick.AddListener(delegate { HandleRepopulateButtonClick(); });
        resetButton.onClick.AddListener(delegate { HandleResetButtonClick(); });
        //virusMortality.onValueChanged.AddListener(delegate { SetVirusMortality(); });
        loadButton.onClick.AddListener(delegate { HandleLoadButtonClick(); });
        saveButton.onClick.AddListener(delegate { HandleSaveButtonClick(); });

        initialInfectionProportionSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("initialInfectionProportionSlider.value"));
        selfHealingSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("selfHealingSlider.value"));
        virusAggressivenessSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("virusAggressivenessSlider.value"));
        personReactionToVirusSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("personReactionToVirusSlider.value"));
        walkingSpeed.SetValueWithoutNotify(PlayerPrefs.GetFloat("walkingSpeed.value"));
        showHealthBar.SetValueWithoutNotify(PlayerPrefs.GetFloat("showHealthBar.value"));
        populationSize.SetValueWithoutNotify(PlayerPrefs.GetFloat("populationSize.value"));
        levelToRequireHospitalSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("levelToRequireHospitalSlider.value"));

        SetSlidersValuesToConstants();
    }

    private void HandleRepopulateButtonClick()
    {
        GameObject population = GameObject.Find("Population");
        populationScript = population.GetComponent<Pupulation>();
        Constants.NumberOfInfected = 0;
        Constants.NumberOfDead = 0;
        Constants.NumberOfCured = 0;
        Constants.hospitalizations = 0;
        GameObject.Find("ConstantManager").GetComponentInChildren<ConstantsManager>().UpdateStats();

        populationScript.RemoveAllPeople();
        GameObject.Find("HospitalSquare").GetComponent<HospitalManagement>().Reset();
        populationScript.Populate();
    }

    private void HandleResetButtonClick()
    {
        print("Button pressed");
        SceneManager.LoadScene(0);
    }

    private void SetSlidersValuesToConstants()
    {
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

    private void HandleLoadButtonClick()
    {
        LoadPresetValues(Constants.DropDownCurrentSelection);
        SetSlidersValuesToConstants();
        HandleRepopulateButtonClick();
    }

    private void HandleSaveButtonClick()
    {
        SavePresetValues(Constants.DropDownCurrentSelection);
    }

    public void LoadPresetValues(int presetValue)
    {
        Debug.Log("Loading presets");
        string presetSelector = presetValue == 0 ? "" : presetValue.ToString();

        initialInfectionProportionSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("initialInfectionProportionSlider.value" + presetSelector));
        selfHealingSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("selfHealingSlider.value" + presetSelector));
        virusAggressivenessSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("virusAggressivenessSlider.value" + presetSelector));
        personReactionToVirusSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("personReactionToVirusSlider.value" + presetSelector));
        walkingSpeed.SetValueWithoutNotify(PlayerPrefs.GetFloat("walkingSpeed.value" + presetSelector));
        showHealthBar.SetValueWithoutNotify(PlayerPrefs.GetFloat("showHealthBar.value" + presetSelector));
        populationSize.SetValueWithoutNotify(PlayerPrefs.GetFloat("populationSize.value" + presetSelector));
        levelToRequireHospitalSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("levelToRequireHospitalSlider.value" + presetSelector));

    }

    public void SavePresetValues(int presetValue)
    {
        Debug.Log("Saving presets " + presetValue);
        if(presetValue!=0)
        {
            PlayerPrefs.SetFloat("levelToRequireHospitalSlider.value" + presetValue, levelToRequireHospitalSlider.value);
            PlayerPrefs.SetFloat("walkingSpeed.value" + presetValue, walkingSpeed.value);
            PlayerPrefs.SetFloat("populationSize.value" + presetValue, populationSize.value);
            PlayerPrefs.SetFloat("showHealthBar.value" + presetValue, showHealthBar.value);
            PlayerPrefs.SetFloat("initialInfectionProportionSlider.value" + presetValue, initialInfectionProportionSlider.value);
            PlayerPrefs.SetFloat("selfHealingSlider.value" + presetValue, selfHealingSlider.value);
            PlayerPrefs.SetFloat("personReactionToVirusSlider.value" + presetValue, personReactionToVirusSlider.value);
            PlayerPrefs.SetFloat("virusAggressivenessSlider.value" + presetValue, virusAggressivenessSlider.value);
        }
        

    }
    private void SetPresetDropdownSelection()
    {
        Debug.Log($"Dropdownselected =  {presetDropdown.value.ToString() }");
        Constants.DropDownCurrentSelection = presetDropdown.value;
        //LoadPresetValues(Constants.DropDownCurrentSelection);
    }

    private void SetLevelToRequireHospital()
    {
        Constants.LevelToRequireHospital = levelToRequireHospitalSlider.value;
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
    }

    private void SetPersonReactionToVirus()
    {
        Text PersonReactionToVirusLabel = GameObject.Find("PersonReactionToVirus").GetComponent<Text>();

        Constants.PersonAttackPower = personReactionToVirusSlider.value * Constants.PersonAttackPowerMax;
        PersonReactionToVirusLabel.text = $"Reaction to Virus - {personReactionToVirusSlider.value}";
    }

    private void SetVirusAttackPower()
    {
        Constants.VirusAttackPower = virusAggressivenessSlider.value * Constants.VirusAttackPowerMax;
        Text virusAggressivenessLabel = GameObject.Find("VirusAggressiveness").GetComponent<Text>();
        //virusAggressivenessLabel.text = $"Virus Aggressiviness - {Constants.VirusAttackPower.ToString("F3")}";
        virusAggressivenessLabel.text = $"Virus Aggressiviness - {virusAggressivenessSlider.value.ToString("F3")}";
        
    }

    private void SetSelfHealingPower()
    {
        string sliderValue = ShowSliderValue ? selfHealingSlider.value.ToString("F3") : "";
        Text PersonSelfHealRateLabel = GameObject.Find("PersonSelfHealing").GetComponent<Text>();
        Constants.PersonSelfHealRate = selfHealingSlider.value * Constants.PersonSelfHealMaxRate;
        PersonSelfHealRateLabel.text = $"Self Healing {selfHealingSlider.value} ";
        
    }

    private void SetInitialInfectionProportion()
    {
        Text InitialInfectionProportionLabel = GameObject.Find("InitialInfectionProportion").GetComponent<Text>();
        Constants.InitialInfectionProportion = initialInfectionProportionSlider.value * Constants.InitialInfectionProportionMax;
        InitialInfectionProportionLabel.text = $"% Initial Infection - {initialInfectionProportionSlider.value.ToString("F2")} - ({(int)(Constants.InitialInfectionProportion * Constants.InitialPopulation) })";
        //InitialInfectionProportionLabel.text = $"% Initial Infection - {Constants.InitialInfectionProportion.ToString("F3")}";
        
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
