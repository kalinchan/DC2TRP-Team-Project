using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public int currentEnergy, energyMax;
    public bool myTurn;
    
    // Start is called before the first frame update
    void Start()
    {
       
        energyMax= 1;
        currentEnergy= energyMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useEnergy(int energy)
    {
        currentEnergy -= energy;
    }

    public void resetEnergy()
    {
        currentEnergy = energyMax;
    }
}
