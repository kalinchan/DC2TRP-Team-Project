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

    public HealthBarScript healthbar;
    public DefenceBarScript defencebar;
    public EnergyBarScript energybar;

    // cursor alteration
    public Texture2D cursorArrow; //default pointer
    public Texture2D cursorHand; //interactable
    public Texture2D cursorShield; //defence
    public Texture2D cursorSpecial; //special
    public Texture2D cursorX; //not interactable
    public CursorMode cursorMode;
    public GameObject player;
    private Hand playerHand;
    // attack not needed for player




    // Start is called before the first frame update
    void Start()
    {
        
        self = GameObject.Find("Player").GetComponent<EntityStats>();
        HealthText = GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();
        EnergyText = GameObject.Find("PlayerEnergyText").GetComponent<TextMeshProUGUI>();
        DefenceText = GameObject.Find("PlayerDefenceText").GetComponent<TextMeshProUGUI>();
        energyMax = 10;





        if (energybar != null)
        {
            energybar.SetMaxEnergy(energyMax);

        }
        currentEnergy = energyMax;
        if (healthbar != null)
        {
            healthbar.SetMaxHealth(self.getCurrentHealth());

        }

        if (defencebar != null)
        {
            defencebar.SetDefence(self.getCurrentDefence());

        }
        updateEnergy();
        self.specialx2 = false;

        //set cursor
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
        player = GameObject.Find("Player");
        playerHand = player.GetComponent<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        //HealthText.text = "Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        HealthText.text = self.getCurrentHealth() + " / " + self.getMaxHealth() + "";

        //EnergyText.text = "Energy: " + currentEnergy + " / " + energyMax + "";

        //DefenceText.text = "Defence: " + self.getCurrentDefence() + "";
        DefenceText.text = self.getCurrentDefence() + "";
        
        if (healthbar != null)
        {
            healthbar.SetHealth(self.getCurrentHealth());
        }
        
        if(defencebar!= null)
        {
            defencebar.SetDefence(self.getCurrentDefence());

        }

        if(energybar != null)
        {
            energybar.SetEnergy(currentEnergy);

        }
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

    public void drainedEnergyReset()
    {
        currentEnergy = (energyMax - 2);
        updateEnergy();
        self.drained = false;
    }

    public void updateEnergy()
    {
        //EnergyText.text = "Energy: " + currentEnergy + " / " + energyMax + "";
        EnergyText.text = currentEnergy + " / " + energyMax + "";
    }

    public void addEnergy(int energy)
    {
        if (currentEnergy + energy > energyMax){
            resetEnergy();
        }
        else {
            currentEnergy += energy;
            updateEnergy();
        }
    }

    // change cursor on player hover to show it is interactible
    public void OnMouseEnter()
    {
        // if selected card is defence card, ability to interact with player only and not enemies --
        if (playerHand.currentlySelectedCard.group == "Defence")
        {
            Cursor.SetCursor(cursorShield, Vector2.zero, cursorMode);
        }
        else if (playerHand.currentlySelectedCard.group == "Special")
        {
            Cursor.SetCursor(cursorSpecial, Vector2.zero, cursorMode);
        }
        else if (playerHand.currentlySelectedCard.group == "Attack")
        {
            Cursor.SetCursor(cursorX, Vector2.zero, cursorMode);
        }
        else { OnMouseExit(); }
    }

    // cursor to arrow when exit interactable object
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
    }




}
