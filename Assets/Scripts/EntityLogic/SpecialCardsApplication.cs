using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCardsApplication : MonoBehaviour
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

}