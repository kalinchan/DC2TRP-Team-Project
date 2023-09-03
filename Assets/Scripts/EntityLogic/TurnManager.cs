using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TurnManager : MonoBehaviour
{
    public int turnCounter;
    public turnStatus currentTurn;
    public GameObject enemy;
    public TMP_Text MoveText;
    public GameObject player, eTButton, bin;
    private Hand playerHand;
    public GameObject border;
    public GradeManager gradeManager;
    public LevelLoad levelL;


    // Start is called before the first frame update
    // Starts the level on the players turn
    void Start()
    {
        currentTurn = turnStatus.playerTurn;
        player = GameObject.Find("Player");
        enablePlayer();
        eTButton = GameObject.Find("End Turn Button");
        bin = GameObject.Find("Bin");
        bin.SetActive(true);
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy = GameObject.Find("Enemy");
        MoveText = GameObject.Find("MoveInfoText").GetComponent<TextMeshProUGUI>();
        playerHand = player.GetComponent<Hand>();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurn == turnStatus.enemyTurn)
        {
            playerHand.currentlySelectedCard = null;
        }
    }

    public enum turnStatus
    {
        playerTurn,
        enemyTurn,
        levelEnd
    }

    //end the current turn and switch to the opposition
    //do we separate this into 2 methods? - JD 15/02 - nah its good as one CH
    public void switchTurn()
    {
        if (currentTurn == turnStatus.playerTurn)
        {
            if (playerHand != null && playerHand.currentlySelectedCard != null)
            {
                playerHand.currentlySelectedCard = null;
            }

            MoveText.text = "Enemy Turn";
            eTButton.SetActive(false);
            player.GetComponent<PlayerLogic>().myTurn = false;
            currentTurn = turnStatus.enemyTurn;
            enableEnemies();
            turnCounter++;
            bin.SetActive(false);
        }
        else if (currentTurn == turnStatus.enemyTurn)
        {
            
            eTButton.SetActive(true);
            currentTurn = turnStatus.playerTurn;
            enablePlayer();
            player.GetComponent<PlayerLogic>().myTurn = true;
            MoveText.text = "Your Turn!";
            turnCounter++;
            bin.SetActive(true);

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
        levelL = GameObject.Find("Background").GetComponent<LevelLoad>();
        levelL.dimCard(-1); // -1 energy number which will dim all cards as some cost 0 energy

        enemy.GetComponent<EnemyLogic>().startTurn();

        foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (gameObject.name.Equals("Border"))
            {
                gameObject.SetActive(false);
            }
        }


    }

    public void enablePlayer()
    {
        levelL = GameObject.Find("Background").GetComponent<LevelLoad>();
        levelL.undimCards();
        player.GetComponent<PlayerLogic>().myTurn = true;
        gradeManager = GameObject.Find("Progress").GetComponent<GradeManager>();
        gradeManager.IncrementMoves();
        Debug.Log("Turns Taken:" + gradeManager.moves);

        if (!player.GetComponent<EntityStats>().drained)
        {
            player.GetComponent<PlayerLogic>().resetEnergy();
            levelL.dimCard(player.GetComponent<PlayerLogic>().currentEnergy);
        }
        else
        {
            player.GetComponent<PlayerLogic>().drainedEnergyReset();
        }
        
        


    }


    public turnStatus getCurrentTurn()
    {
        return currentTurn;
    }



}
