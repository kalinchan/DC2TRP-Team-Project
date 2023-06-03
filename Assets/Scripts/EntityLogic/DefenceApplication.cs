using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceApplication : MonoBehaviour
{
    private EntityStats eS;
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
            applySpecialOne();
        }

        if (thisCard.tag.Contains("Special02"))
        {
            applySpecialTwo();
        }

        if (thisCard.tag.Contains("Special03"))
        {
            applySpecialThree();
        }

    }

    public void applySpecialOne()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.heal(10);
        eS.takeDamage(thisCard.damage);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialTwo()
    {
        player.GetComponent<PlayerLogic>().addEnergy(5);
        eS.takeDamage(thisCard.damage);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialThree()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        player.GetComponent<PlayerLogic>().setMultiplierToTrue();
        eS.takeDamage(thisCard.damage);
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }
}
