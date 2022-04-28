using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusComponent : MonoBehaviour
{
    static Color defaultColour = new Color(209,219,221);
    const int StartInfectionNumber = 10;
    const float timestep = 0.2f;

    public Country countryReference;
    public CountryComponent countryComponent;
    public Virus virus;

    [SerializeField] int susceptible;
    [SerializeField] int infected;
    [SerializeField] int killed;
    [SerializeField] int immune;

    int tick = 0;

    public Queue<int> infectedBuffer = new Queue<int>();
    SpriteRenderer renderer;

    public bool beginInfection = false;
    public void BeginInfection()
    {
        Infect(StartInfectionNumber);
    }

    private void Start()
    {
        susceptible = countryReference.population;
        InvokeRepeating("Tick", timestep, timestep);
        renderer = GetComponent<SpriteRenderer>();
        if (beginInfection)
            BeginInfection();
    }

    void UpdateGraphics()
    {
        int total = infected + killed;
        float percent = total / (float)countryReference.population;
        renderer.color = Color.white * (1-percent) + (virus.colour * percent);

        if(susceptible == 0 && infected == 0)
        {
            renderer.color = Color.green;
        }
    }

    public void Tick()
    {
        int numberToInfect = Mathf.RoundToInt(infected * virus.R);
        Infect(numberToInfect);
        infectedBuffer.Enqueue(numberToInfect);

        if (tick > virus.incubationPeriod)
        {
            int numberToDefect = infectedBuffer.Dequeue();
            Defect(numberToDefect);
        }

        tick++;

        Country c =  countryComponent.GetRandomNeighbor();
        CountryComponent otherCountry = c.gameObject.GetComponent<CountryComponent>();
        if (!otherCountry.DoesCountryContainVirus(virus))
            otherCountry.InfectCountry(virus);
        else
            otherCountry.ReinfectCountry(virus);

        UpdateGraphics();
    }

    void Infect(int numberToInfect)
    {
        if (susceptible > numberToInfect)
        {
            susceptible -= numberToInfect;
            infected += numberToInfect;
        }
        else
        {
            infected += susceptible;
            susceptible = 0;
        }
    }
    void Defect(int numberToDefect)
    {
        float rng = Random.Range(0.0f, 1.0f);
        //death
        if (rng < virus.fatalityRate)
        {
            Kill(numberToDefect);
        }
        //no death
        else
        {
            rng = Random.Range(0.0f, 1.0f);
            if (rng < virus.chanceOfRecurrance)
            {
                // person may become infected again
                if (infected > numberToDefect)
                {
                    infected -= numberToDefect;
                    susceptible += numberToDefect;
                }
                else
                {
                    susceptible += infected;
                    infected = 0;
                }
            }
            else
            {
                // person become completely immune to reinfection of this virus
                Immunize(numberToDefect);
            }
        }
    }
    void Kill(int numberToKill)
    {
        if (infected > numberToKill)
        {
            infected -= numberToKill;
            killed += numberToKill;
        }
        else
        {
            killed += infected;
            infected = 0;
        }
    }
    void Immunize(int numberToImmunize)
    {
        if (infected > numberToImmunize)
        {
            infected -= numberToImmunize;
            immune += numberToImmunize;
        }
        else
        {
            immune += infected;
            infected = 0;
        }
    }
}
