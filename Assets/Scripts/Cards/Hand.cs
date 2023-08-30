using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public ThisCard currentlySelectedCard;

    // Start is called before the first frame update
    void Start()
    {
        // nothing to do here...
    }

    // Update is called once per frame
    void Update()
    {
        // nothing to do here....
    }

    public void clearCard()
    {
        currentlySelectedCard = null;
    }

    public ThisCard GetCard()
    {
        Debug.Log("GetCard() called");
        return currentlySelectedCard;
    }

    public void OnCardClick(ThisCard card)
    {
        currentlySelectedCard = card;
        Debug.Log("Card clicked: " + card.name);
    }
}
