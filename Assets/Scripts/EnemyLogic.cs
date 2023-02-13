using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int damage, charge, maxCharge, defence, moves, maxMoves;
    bool myTurn;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //apply the damage int to player
    public void attack()
    {
        //call the takeDamage method of player for damage value
        //once max moves has been reached, end turn
    }

    //use special attack if charge is available
    public void special()
    {
        //each turn increase charge value
        //if charge value >= threshold, allow use of special
        //after use, reset charge to 0
        //once max moves has been reached, end turn
    }

    //gain defence equal to defend value
    public void defend()
    {
        //gain defence == defence integer
        //once max moves has been reached, end turn
    }

    public void endTurn()
    {
        myTurn = false;
        //set player myTurn to true
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
