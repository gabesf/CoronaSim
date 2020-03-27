using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum ShowHealthBar
{
    all,
    infected,
    none,
    OnClick
}

public static class Constants
    {

    static public int NumberOfDead { get; set; } = 0;
    static public int NumberOfCured { get; set; } = 0;
    static public int NumberOfInfected { get; set; } = 0;
    static public int hospitalizations { get; set; } = 0;


    static public ShowHealthBar HealthBarDisplay = ShowHealthBar.infected;
    static public int InitialPopulation { get; set; } = 50;
    static public Vector2 WorldSize { get; set; } = new Vector2(355, 200);

    static public float PersonAttackPower { get; set; } = 0.002f;
    static public float PersonAttackPowerMax = 0.005f;

    static public float PersonSelfHealRate { get; set; } = 0.001f;
    static public float PersonSelfHealMaxRate { get; } = 0.005f;

    static public float VirusAttackPower = 0.002f;
    static public float VirusAttackPowerMax { get; } = 0.005f;


    static public float MaxWalkingSpeed = 100;
    static public float MaxInfectingWithoutSignsTime = 1000;
    static public float MaxVirusMortalityRate = 100;

    static public int PopulationLimit { get; } = 1000;

    static public float WalkingSpeed { get; set; } = 30;

    static public Vector3 HospitalPosition { get; set; } = new Vector3(0, 0, 0);

    static public float InitialInfectionProportion = 0.01f;
    static public float InitialInfectionProportionMax { get; } = 0.05f;

    static public float InfectingWithoutSignsTime { get; set; } = 50;

    static public float VirusMortalityRate { get; set; }

    static public Vector3 PersonSize { get; set; }
    static public float PersonSizeMax { get; } = 10;


    //static public float infectionRate = 0.1f;
    //static public float infectionTimeLimit = 50;

    static public float goToHospitalWhenSickChance = 1f;
    static public float LevelToRequireHospital = 1f;

    public static float BeAwareChance = 0.0f;
    }

