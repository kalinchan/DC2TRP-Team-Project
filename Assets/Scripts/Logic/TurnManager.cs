using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnCounter;
    public turnStatus currentTurn;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = turnStatus.playerTurn;
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
    //do we separate this into 2 methods?
    public void switchTurn()
    {
        if(currentTurn == turnStatus.playerTurn)
        {
            currentTurn = turnStatus.enemyTurn;
            enableEnemies();
            turnCounter++;
        }
        else if (currentTurn == turnStatus.enemyTurn)
        {
            currentTurn = turnStatus.playerTurn;
            enablePlayer();
            turnCounter++;
        }
                
    }
       
    //find all enemies, set myTurn to true and call the turnTaker method
    public void enableEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) 
        {
            enemy.GetComponent<EnemyLogic>().startTurn();  
        }
    }

    public void enablePlayer()
    {
        //allow the player to interact again, set player myTurn to true or however we're coding it
    }
}
