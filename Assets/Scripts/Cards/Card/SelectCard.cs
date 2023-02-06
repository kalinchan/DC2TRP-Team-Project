using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 11/01/2023 / Author CH

// script for selecting card to play from hand - CH
public class SelectCard : MonoBehaviour
{
    public GameObject Card;

    // when card is selected by player - CH
    public void OnClick()
    {
        // TODO: Alter Stats (Health, Energy, Defence, Attack) - depending on card played
        Card.SetActive(false); // remove card from hand
    }
}
