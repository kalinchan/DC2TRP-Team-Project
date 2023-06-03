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

    public void applyDefence()
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
    }
}
