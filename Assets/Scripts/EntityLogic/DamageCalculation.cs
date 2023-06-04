using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    private EntityStats eS;
    private ThisCard thisCard;
    public Hand playerHand;
    public GameObject player;
    private GameObject levelL;

    //Get entity stats associated with this enemy
    //Find and get the Player's Hand - JD
    private void Start()
    {  
        eS = GetComponent<EntityStats>();
        player = GameObject.Find("Player");
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
        levelL = GameObject.Find("Background");
    }

    //using the players hand, find which card has been clicked (made active) and identify its tag.
    //if the active card has a damage tag, deal appropriate damage to this enemy. Then set it as inactive (discard from Hand and remove as currentlySelectedCard - JD 16/02) - JD
    public void GetCard()
    {   
        thisCard = playerHand.currentlySelectedCard;
        if (thisCard.tag.Contains("Attack"))
        {
            player.GetComponent<PlayerLogic>().useEnergy(thisCard.energyCost);
            eS.takeDamage(thisCard.damage);
            thisCard.gameObject.SetActive(false);
            playerHand.clearCard();

       
            if(eS.gameObject.tag.Contains("Enemy"))
            {
                eS.gameObject.GetComponent<EnemyLogic>().updateUI();
                levelL.GetComponent<LevelLoad>().reduceHandSize();
            }

        }

      
    }
}
