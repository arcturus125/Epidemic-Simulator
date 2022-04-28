using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryComponent : MonoBehaviour
{
    public Country country;
    public Country[] landNeighbors;
    public Country[] seaNeighbors;

    
    List<VirusComponent> viruses = new List<VirusComponent>();

    public bool DoesCountryContainVirus(Virus v)
    {
        foreach(VirusComponent temp in viruses)
        {
            if (temp.virus == v) return true;
        }
        return false;
    }
    public void InfectCountry(Virus v)
    {
        VirusComponent cmpnt = gameObject.AddComponent<VirusComponent>();

        cmpnt.virus = v;
        cmpnt.countryReference = country;
        cmpnt.beginInfection = true;
        cmpnt.countryComponent = this;

        viruses.Add(cmpnt);
    }

    public void ReinfectCountry(Virus v)
    {
        foreach (VirusComponent temp in viruses)
        {
            if (temp.virus == v)
            {
                temp.BeginInfection();
                return;
            }
        }
    }

    public Country GetRandomNeighbor()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            if (landNeighbors.Length > 0)
            {
                return GetRandomLandNeighbor();
            }
            else return GetRandomSeaNeighbor();
            
        }
        else
        {
            if (seaNeighbors.Length > 0)
            {
                return GetRandomSeaNeighbor();
            }
            else return GetRandomLandNeighbor();
        }
    }
    public Country GetRandomLandNeighbor()
    {
        int rand = Random.Range(0, landNeighbors.Length);
        return landNeighbors[rand];
    }
    public Country GetRandomSeaNeighbor()
    {
        int rand = Random.Range(0, seaNeighbors.Length);
        return seaNeighbors[rand];
    }

    private void Awake()
    {
        country.gameObject = this.gameObject;
    }
    private void Start()
    {
        if(country == null)
        {
            Debug.LogError($"Gameobject {gameObject.name} missing country");
        }
    }

}
