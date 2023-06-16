using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardCard : MonoBehaviour
{
    private ThisCard thisCard;
    public Hand playerHand;
    public GameObject player;
    private GameObject levelL;

    //Get entity stats associated with this enemy
    //Find and get the Player's Hand - JD
    private void Start()
    {
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
        levelL = GameObject.Find("Background");
    }

    //using the players hand, find which card has been clicked (made active) and identify its tag.
    //if the active card has a damage tag, deal appropriate damage to this enemy. Then set it as inactive (discard from Hand and remove as currentlySelectedCard - JD 16/02) - JD
    public void Discard()
    {
        thisCard = playerHand.currentlySelectedCard;
        thisCard.gameObject.SetActive(false);
        playerHand.clearCard();
        levelL.GetComponent<LevelLoad>().reduceHandSize();
    }

}
