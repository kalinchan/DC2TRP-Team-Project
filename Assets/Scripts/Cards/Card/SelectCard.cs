using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 11/01/2023 / Author CH

// script for selecting card to play from hand - CH
public class SelectCard : MonoBehaviour
{
    public GameObject Card, player;
    private EntityStats currentEnemy;
    private ThisCard thisCard;
    private Hand playerHand;
    public void Start()
    {
        thisCard = GetComponent<ThisCard>();
        player = GameObject.Find("Player");
        playerHand = player.GetComponent<Hand>();
    }
    // when card is selected by player - CH
    // Sets card to active, the active status can then be used for targeting & energy checking - JD
    // This needs to no work if the current player energy is not enough to cover the card cost - JD 15/02
    public void OnClick()
    {
        // TODO: Alter Stats (Health, Energy, Defence, Attack) - depending on card played
        //GetEnemy();
        //Card.SetActive(false); // remove card from hand
        if (thisCard.energyCost > player.GetComponent<PlayerLogic>().currentEnergy)
        {
            return;
        }
        playerHand.currentlySelectedCard = thisCard;
    }

    //The card knows what enemy it is looking at. - JD
    public void GetEnemy(EntityStats enemy)
    {
        currentEnemy = enemy;
    }

    //card tells the current enemy to take damage equal to its damage value - JD
    public void dealDamage()
    {
        currentEnemy.takeDamage(thisCard.damage);
    }

    public void gainDefence()
    {
        player.GetComponent<DefenceApplication>().applyDefence();
    }
}
