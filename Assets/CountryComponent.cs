using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryComponent : MonoBehaviour
{
    [HideInInspector] public GameObject gameobject;
    public Country country;
    public Country[] landNeighbors;
    public Country[] seaNeighbors;

    private void Awake()
    {
        gameobject = this.gameObject;
    }

}
