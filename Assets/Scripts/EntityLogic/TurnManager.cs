using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public int turnCounter;
    public turnStatus currentTurn;
    //public GameObject[] enemies;
    public GameObject enemy;
    public GameObject player, eTButton;
    private Hand playerHand;



    // Start is called before the first frame update
    // Starts the level on the players turn
    void Start()
    {
        currentTurn = turnStatus.playerTurn;
        player = GameObject.Find("Player");
        enablePlayer();
        eTButton = GameObject.Find("End Turn Button");
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy = GameObject.Find("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
    }

    public enum turnStatus
    {
        playerTurn,
        enemyTurn,
        levelEnd
    }

    //end the current turn and switch to the opposition
    //do we separate this into 2 methods? - JD 15/02
    public void switchTurn()
    {
        if (currentTurn == turnStatus.playerTurn)
        {
            eTButton.SetActive(false);
            player.GetComponent<PlayerLogic>().myTurn = false;
            currentTurn = turnStatus.enemyTurn;
            enableEnemies();
            turnCounter++;
        }
        else if (currentTurn == turnStatus.enemyTurn)
        {
            eTButton.SetActive(true);
            currentTurn = turnStatus.playerTurn;
            enablePlayer();
            turnCounter++;
            //This is suuuuuuuper janky but works to update cards at the end of the turn
            if (!player.GetComponent<EntityStats>().skipDraw)
            {
                gameObject.GetComponent<DealCards>().OnClick();
            }
            player.GetComponent<EntityStats>().skipDraw = false;
         }

        }

    //find all enemies, set myTurn to true and call the turnTaker method
    //needs to be fixed to go through a list of all enemies with the enemy tag - JD 15/02
    public void enableEnemies()
    {

        enemy.GetComponent<EnemyLogic>().startTurn();
       

        //foreach (GameObject enemy in enemies) 
        //{
        //    enemy.GetComponent<EnemyLogic>().startTurn();  
        //}

    }

    public void enablePlayer()
    {
        player.GetComponent<PlayerLogic>().myTurn = true;
        if (!player.GetComponent<EntityStats>().drained)
        {
            player.GetComponent<PlayerLogic>().resetEnergy();
        }
        else
        {
            player.GetComponent<PlayerLogic>().drainedEnergyReset();
        }


    }


}
