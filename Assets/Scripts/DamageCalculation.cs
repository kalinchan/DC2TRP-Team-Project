using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    private EntityStats eS;
    private ThisCard thisCard;
    public Hand playerHand;

    private void Start()
    {
        eS = GetComponent<EntityStats>();
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
    }
    public void GetCard()
    {
        thisCard = playerHand.currentlySelectedCard;
        if (thisCard.tag.Contains("Attack"))
        {
            eS.TakeDamage(thisCard.damage);
        }
    }
}
