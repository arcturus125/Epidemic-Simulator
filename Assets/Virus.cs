using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VirusName", menuName = "Virus", order = 1)]
public class Virus : ScriptableObject
{
    // data
    public string virusName;
    public int R;
    public float fatalityRate;
    public int incubationPeriod;

    // visuals
    public Color colour;
}
