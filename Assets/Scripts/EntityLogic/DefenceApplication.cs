using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceApplication : MonoBehaviour
{
    private EntityStats eS, eES;
    private ThisCard thisCard;
    public Hand playerHand;
    public GameObject player;
    private GameObject levelL;
    public TMP_Text MoveText;
    private SelectCard selectCard;
    private int specialEnergy;
    private int specialHealth;
    private int specialDefence;

    // Start is called before the first frame update
    void Start()
    {
        eS = GetComponent<EntityStats>();
        player = GameObject.Find("Player");
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
        levelL = GameObject.Find("Background");
        eES = GameObject.Find("Enemy").GetComponent<EntityStats>();
        MoveText = GameObject.Find("MoveInfoText").GetComponent<TextMeshProUGUI>();
        specialEnergy = 3;
        specialHealth = 5;
        specialDefence = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpecialEnergy(int energy)
    {
        specialEnergy = energy;
    }

    public void setSpecialHealth(int health)
    {
        specialHealth = health;
    }

    public void setSpecialDefence(int def)
    {
        specialDefence = def;
    }

    public void applyCard()
    {
        thisCard = playerHand.currentlySelectedCard;

        if (thisCard.energyCost > player.GetComponent<PlayerLogic>().currentEnergy) // if not enough energy to play card
        {
            MoveText.text = "Insufficient Energy!";
            playerHand.clearCard();
            selectCard.clearCard();
            return;
        }

        if (thisCard.tag.Contains("Defence"))
        {
            player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
            eS.gainDefence(thisCard.defence);
            thisCard.gameObject.SetActive(false);
            playerHand.clearCard();
            levelL.GetComponent<LevelLoad>().reduceHandSize();
            //player.GetComponent<PlayerLogic>().EnergyText.text = "Energy: " + player.GetComponent<PlayerLogic>().currentEnergy + " / " + player.GetComponent<PlayerLogic>().energyMax + "";
        }

        if (thisCard.tag.Contains("Special01")) // Get enough sleep
        {
            applySpecialOne();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special02")) // Drink energy drink
        {
            applySpecialTwo();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special03")) // Workout
        {
            applySpecialThree();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special04")) // Extension
        {
            applySpecialFour();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }




    }


    public void testDefence() // for testing purposes only, skipping unneccessaries
    {
        thisCard = playerHand.currentlySelectedCard;
        eS = GetComponent<EntityStats>();
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.gainDefenceTest(thisCard.defence); // skipping animations and sounds
    }


    public void applySpecialOne() // get enough sleep, health + specialHealth
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.heal(specialHealth);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialOneTest() // for testing purposes only, skipping unneccessaries
    {
        specialHealth = 5;
        thisCard = playerHand.currentlySelectedCard;
        eS = GetComponent<EntityStats>();
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.heal(specialHealth);
    }

    public void applySpecialTwo() // drink energy drink, energy + specialEnergy
    {
        player.GetComponent<PlayerLogic>().addEnergy(specialEnergy);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialTwoTest() // for testing purposes only, skipping unneccessaries
    {
        specialEnergy = 3;
        thisCard = playerHand.currentlySelectedCard;
        player.GetComponent<PlayerLogic>().addEnergyTest(specialEnergy);
        Debug.Log("Adding " + specialEnergy + " energy");
    }

    public void applySpecialThree() // workout, attack x 2 for one card play
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eES.setMultiplierToTrue();
        eES.changeSpecialx2Text();
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialFour() // extension, defence + specialDefence
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.gainDefence(specialDefence);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }
}
