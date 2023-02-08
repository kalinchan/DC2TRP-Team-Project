using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    private EntityStats eS;
    private ThisCard thisCard;
    public Hand playerHand;

    //Get entity stats associated with this enemy
    //Find and get the Player's Hand - JD
    private void Start()
    {  
        eS = GetComponent<EntityStats>();
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
    }

    //using the players hand, find which card has been clicked (made active) and identify its tag.
    //if the active card has a damage tag, deal appropriate damage to this enemy. - JD
    public void GetCard()
    {   
        thisCard = playerHand.currentlySelectedCard;
        if (thisCard.tag.Contains("Attack"))
        {
            eS.TakeDamage(thisCard.damage);
        }
    }
}
