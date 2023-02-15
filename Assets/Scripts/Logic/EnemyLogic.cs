using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int damage, charge, maxCharge, defence, moves, maxMoves, attackCost, defenceCost, specialCost;
    bool myTurn;
    private EntityStats self, player;
    
    // Start is called before the first frame update
    void Start()
    {
        //tweak numbers to tune difficulty per enemy
        damage = 4;
        charge = 0;
        maxCharge = 4;
        defence = 2;
        moves = 2;
        maxMoves = 2;
        attackCost = 1;
        defenceCost = 1;
        specialCost = 2;
        self =GetComponent<EntityStats>();
        player = GameObject.Find("Player").GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //apply the damage int to player
    public void attack()
    {
        player.takeDamage(damage);
        moves -= attackCost;
    }

    //use special attack
    public void special()
    {
        //probably needs to call a separate script? each enemy type has its own special
        //can that just be done here or is that too messy

        //the logic for charging this can be done with the turnCounter Value and calculating when there is no remainder when divided by a charge threshold? maybe.


    }

    //gain defence equal to defend value
    public void defend()
    {
        self.gainDefence(defence);
        moves -= defenceCost;
    }

    public void startTurn()
    {
        myTurn = true;
        turnTaker();
    }

    //tells the TurnManager that this turn is done
    public void endTurn()
    {
        myTurn = false;
        GameObject.Find("Turn Manager").GetComponent<TurnManager>().switchTurn();
    }

    public void turnTaker()
    {
        //if myTurn = true this method runs
        //resets moves to max, increases charge by one
        //using as many moves as the enemy has available carry out, random
        //moves until move pool is empty
        //then uses endTurn 
    }

}
