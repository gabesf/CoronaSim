using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Constants
    {
    static public int InitialPopulation { get; set; } = 500;
    static public Vector2 WorldSize { get; set; } = new Vector2(355, 200);


    static public float MaxWalkingSpeed = 100;
    static public float MaxInfectingWithoutSignsTime = 1000;
    static public float MaxVirusMortalityRate = 100;

    static public int PopulationLimit = 1000;

    static public float WalkingSpeed { get; set; } = 30;

    static public Vector3 HospitalPosition { get; set; } = new Vector3(-140, 82, 0);


    static public float InfectingWithoutSignsTime { get; set; } = 50;

    static public float VirusMortalityRate { get; set; }

    //static public float infectionRate = 0.1f;
    //static public float infectionTimeLimit = 50;

    static public float goToHospitalWhenSickChance = 1f;

    public static float BeAwareChance = 0.0f;
    }

