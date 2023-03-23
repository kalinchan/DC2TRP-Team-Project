
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyLogic : MonoBehaviour
{
    public int damage, charge, maxCharge, defence, movesRemaining, maxMoves, attackCost, defenceCost, specialCost;
    public float wait;
    public bool myTurn;
    private EntityStats self, player;
    public TMP_Text HealthText, SpecialText, DefenceText;

    //Declaring the healthBar
    public EnemyBarScript healthbar;
    public EnemyDefenceBarScript defencebar;
    

    // Start is called before the first frame update
    void Start()
    {
        //tweak numbers to tune difficulty per enemy
        damage = 4;
        charge = 0;
        maxCharge = 4;
        defence = 2;
        movesRemaining = 2;
        maxMoves = 2;
        attackCost = 1;
        defenceCost = 1;
        specialCost = 2;
        myTurn = false;
        wait = 1.5f;
        self = GameObject.Find("Enemy").GetComponent<EntityStats>();
        player = GameObject.Find("Player").GetComponent<EntityStats>();
        HealthText = GameObject.Find("EnemyHealthText").GetComponent<TextMeshProUGUI>();
        SpecialText = GameObject.Find("EnemySpecialText").GetComponent<TextMeshProUGUI>();
        DefenceText = GameObject.Find("EnemyDefenceText").GetComponent<TextMeshProUGUI>();
        HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        SpecialText.text = "Moves until Enemy Special: " + (maxCharge - charge) + "";
        DefenceText.text = "Enemy Defence: " + self.defence + "";

        //Health Bar set to max Health for the enemy
        healthbar.SetMaxHealth(self.getCurrentHealth());
        defencebar.SetDefence(self.getCurrentDefence());
    }

    // Update is called once per frame
    void Update()
    {
        //HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        //SpecialText.text = "Moves until Enemy Special: " + (maxCharge - charge) + "";
        //DefenceText.text = "Enemy Defence: " + self.defence + "";
    }

    public void updateUI()
    {
        HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        SpecialText.text = "Moves until Enemy Special: " + (maxCharge - charge) + "";
        DefenceText.text = "Enemy Defence: " + self.defence + "";

        //Enemy health Bar us updated by getting the current health
        healthbar.SetHealth(self.getCurrentHealth());
        defencebar.SetDefence(self.getCurrentDefence());
    }

    //apply the damage int to player
    public void attack()
    {
        Debug.Log("Attacking");
        player.takeDamage(damage);
        movesRemaining -= attackCost;
    }

    //use special attack
    public void special()
    {
        //probably needs to call a separate script? each enemy type has its own special
        //can that just be done here or is that too messy

        //the logic for charging this can be done with the turnCounter Value and calculating when there is no remainder when divided by a charge threshold? maybe.
        HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        movesRemaining -= specialCost;
    }

    //gain defence equal to defend value
    public void defend()
    {
        Debug.Log("Defending");
        self.gainDefence(defence);
        movesRemaining -= defenceCost;
        DefenceText.text = "Enemy Defence: " + self.defence + "";
    }

    public void startTurn()
    {
        Debug.Log("Starting Turn");
        myTurn = true;
        StartCoroutine(turnTaker());
    }

    //tells the TurnManager that this turn is done

    //THIS WILL CAUSE ISSUES WITH MULTIPLE ENEMIES IN ONE LEVEL!!! Not tested yet but almost certain that because this method CHANGES the turn directly, it will mean that once one enemy takes its turn, no more enemies will be
    //able too. as I say, not sure yet, but I can forsee it being an issue! Maybe we need to have something else that checks if ALL enemies myTurn == false and then call the method - probably safer. JD 14/02 <3
    public void endTurn()
    {
        myTurn = false;
        GameObject.Find("Turn Manager").GetComponent<TurnManager>().switchTurn();
    }

    
    IEnumerator turnTaker()
    {
        Debug.Log("TurnTaker Called");
        movesRemaining = maxMoves;
        charge++;
        while (movesRemaining > 0)
        {
            Debug.Log("entering while loop");
            yield return new WaitForSeconds(wait);
            int random = Random.Range(1, 3);
            switch (random)
            {
                case 1:
                    if (movesRemaining < attackCost)
                    {
                        defend();
                        break;
                    }
                    attack();
                    break;
                case 2:
                    defend();
                    break;
                case 3:
                    if (charge == maxCharge)
                    {
                        special();
                        break;
                    }
                    attack();
                    break;

            }
        }
        updateUI();
        endTurn();
    }


    //This might need an additonal check in the while loop for if there are moves in the pool but no moves costs that fit, i.e. if theres 1 move left in the pool but attack, defend and special all cost 2 or more
    //this loop would run indefinitely in that case - JD 16/02 
    //public void turnTaker()
    //{
        //movesRemaining = maxMoves;
        //charge++;
        //while (movesRemaining > 0)
        //{
        //    int random = Random.Range(1, 3);
        //    switch (random)
        //    {
        //        case 1:
        //            if (movesRemaining < attackCost)
        //            {
        //                defend();
        //                break;
        //            }
        //                attack();
        //                break;
        //        case 2:
        //            defend();
        //            break;
        //        case 3:
        //            if (charge == maxCharge)
        //            {
        //                special();
        //                break;
        //            }
        //            attack();
        //            break;

        //    }
        //}
        //endTurn();
        //if myTurn = true this method runs
        //resets moves to max, increases charge by one
        //using as many moves as the enemy has available carry out, random
        //moves until move pool is empty
        //then uses endTurn 
    //}

}
