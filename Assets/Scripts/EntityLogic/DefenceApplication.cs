using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceApplication : MonoBehaviour
{
    private EntityStats eS, eES;
    private ThisCard thisCard;
    public Hand playerHand;
    public GameObject player;
    private GameObject levelL;

    // Start is called before the first frame update
    void Start()
    {
        eS = GetComponent<EntityStats>();
        player = GameObject.Find("Player");
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
        levelL = GameObject.Find("Background");
        eES = GameObject.Find("Enemy").GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void applyCard()
    {
        thisCard = playerHand.currentlySelectedCard;

        if (thisCard.tag.Contains("Defence"))
        {
            player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
            eS.gainDefence(thisCard.defence);
            thisCard.gameObject.SetActive(false);
            playerHand.clearCard();
            levelL.GetComponent<LevelLoad>().reduceHandSize();
            //player.GetComponent<PlayerLogic>().EnergyText.text = "Energy: " + player.GetComponent<PlayerLogic>().currentEnergy + " / " + player.GetComponent<PlayerLogic>().energyMax + "";
        }

        if (thisCard.tag.Contains("Special01"))
        {
            applySpecialThree();
            eS.applyPlayerAnim("playerSpecial");
        }

        if (thisCard.tag.Contains("Special02"))
        {
            applySpecialTwo();
            eS.applyPlayerAnim("playerSpecial");
        }

        if (thisCard.tag.Contains("Special03"))
        {
            applySpecialThree();
            eS.applyPlayerAnim("playerSpecial");
        }

        if (thisCard.tag.Contains("Special04"))
        {
            applySpecialFour();
            eS.applyPlayerAnim("playerSpecial");
        }

    }

    public void applySpecialOne()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.heal(10);//hardcoded, should be changed at some point
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialTwo()
    {
        player.GetComponent<PlayerLogic>().addEnergy(5);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialThree()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eES.setMultiplierToTrue();
        eES.changeSpecialx2Text();
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialFour()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.gainDefence(10);//hardcoded, should be changed at some point
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }
}
