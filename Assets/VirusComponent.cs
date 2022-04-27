using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusComponent : MonoBehaviour
{
    public Country countryReference;
    public Virus virus;

    [SerializeField] int susceptible;
    [SerializeField] int infected;
    [SerializeField] int killed;
    [SerializeField] int immune;

    int tick = 0;

    public Queue<int> infectedBuffer = new Queue<int>();
    SpriteRenderer renderer;

    private void Start()
    {
        susceptible = countryReference.population;
        InvokeRepeating("Tick", 1, 1);
        renderer = GetComponent<SpriteRenderer>();
    }

    void UpdateGraphics()
    {
        int total = infected + killed;
        float percent = total / (float)countryReference.population;
        renderer.color = new Color(percent,percent,percent,1) * virus.colour;
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
        if(rng < virus.fatalityRate)
        {
            Kill(numberToDefect);
        }
        //immune
        else
        {
            Immunize(numberToDefect);
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
