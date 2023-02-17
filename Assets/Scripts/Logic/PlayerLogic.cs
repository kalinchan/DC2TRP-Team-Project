using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerLogic : MonoBehaviour
{
    public int currentEnergy, energyMax;
    public bool myTurn;
    private EntityStats self;
    public TMP_Text HealthText, EnergyText, DefenceText;


    // Start is called before the first frame update
    void Start()
    {
        
        self = GameObject.Find("Player").GetComponent<EntityStats>();
        HealthText = GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();
        EnergyText = GameObject.Find("PlayerEnergyText").GetComponent<TextMeshProUGUI>();
        DefenceText = GameObject.Find("PlayerDefenceText").GetComponent<TextMeshProUGUI>();
        energyMax = 3;
        currentEnergy = energyMax;
        updateEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        //EnergyText.text = "Energy: " + currentEnergy + " / " + energyMax + "";
        DefenceText.text = "Defence: " + self.getCurrentDefence() + "";

    }

    public void useEnergy(int energy)
    {
        currentEnergy -= energy;
        updateEnergy();
    }

    public void resetEnergy()
    {
        currentEnergy = energyMax;
        updateEnergy();
    }

    public void updateEnergy()
    {
        EnergyText.text = "Energy: " + currentEnergy + " / " + energyMax + "";
    }
}
