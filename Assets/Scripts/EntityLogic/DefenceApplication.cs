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

    // Start is called before the first frame update
    void Start()
    {
        eS = GetComponent<EntityStats>();
        player = GameObject.Find("Player");
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
        levelL = GameObject.Find("Background");
        eES = GameObject.Find("Enemy").GetComponent<EntityStats>();
        MoveText = GameObject.Find("MoveInfoText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (thisCard.tag.Contains("Special01"))
        {
            applySpecialOne();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special02"))
        {
            applySpecialTwo();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special03"))
        {
            applySpecialThree();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }

        if (thisCard.tag.Contains("Special04"))
        {
            applySpecialFour();
            eS.applyPlayerAnim("playerSpecial");
            AudioManager.instance.PlaySound("Special Move");
        }




    }


    public void applySpecialOne()
    {
        player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
        eS.heal(5);//hardcoded, should be changed for final submission
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

    public void applySpecialTwo()
    {
        player.GetComponent<PlayerLogic>().addEnergy(3);//hardcoded, should be changed for final submission
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
        eS.gainDefence(5);//hardcoded, should be changed for final submission
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }
}
