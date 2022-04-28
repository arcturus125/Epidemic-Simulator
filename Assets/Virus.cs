using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VirusName", menuName = "Virus", order = 1)]
public class Virus : ScriptableObject
{
    // data
    public string virusName;
    public float R;
    [Range(0.0f,1.0f)]
    public float fatalityRate;
    [Range(0.0f, 1.0f)]
    public float chanceOfRecurrance;
    public int incubationPeriod;

    // visuals
    public Color colour;
}
