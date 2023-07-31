
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
    public TMP_Text HealthText, SpecialText, DefenceText, MoveText;
    public List<string> attacks = new List<string>();
    public List<string> defences = new List<string>();
    public List<string> specials = new List<string>();

    //Declaring the healthBar
    public EnemyBarScript healthbar;
    public EnemyDefenceBarScript defencebar;
    public EnemySpecialBarScript specialbar;

    // cursor alteration
    public Texture2D cursorArrow; //default pointer
    public Texture2D cursorHand; //interactable
    public Texture2D cursorSword; //attack
    public Texture2D cursorX; //not interactable
    public CursorMode cursorMode;
    private Hand playerHand;
    //defence & special not needed for enemy

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
        wait = 2f;
        self = GameObject.Find("Enemy").GetComponent<EntityStats>();
        player = GameObject.Find("Player").GetComponent<EntityStats>();
        HealthText = GameObject.Find("EnemyHealthText").GetComponent<TextMeshProUGUI>();
        SpecialText = GameObject.Find("EnemySpecialText").GetComponent<TextMeshProUGUI>();
        DefenceText = GameObject.Find("EnemyDefenceText").GetComponent<TextMeshProUGUI>();
        MoveText = GameObject.Find("MoveInfoText").GetComponent<TextMeshProUGUI>();

        


        //HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        HealthText.text = self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        
        SpecialText.text = "Moves until Enemy Special: " + (maxCharge - charge) + "";
        
        //DefenceText.text = "Enemy Defence: " + self.defence + "";
        DefenceText.text =  self.defence + "";


        specialbar.SetMaxSpecial(maxCharge);
        specialbar.SetSpecial(charge);

        //Health Bar set to max Health for the enemy
        if (healthbar != null)
        {
            healthbar.SetMaxHealth(self.getCurrentHealth());

        }
        if (defencebar != null)
        {
            defencebar.SetDefence(self.getCurrentDefence());

        }

        //set cursor
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
        playerHand = player.GetComponent<Hand>();

        attacks.AddRange(new List<string>{
            "Pop Quiz! \n" + damage + " damage taken", "Assign Exam!\n" + damage + " damage taken", "Harsh Feedback!\n" + damage + " damage taken"
        });

        defences.AddRange(new List<string>{
            "Union Strike!\nDefence +" + defence, "Take a Holiday!\nDefence +" + defence
        });

        specials.AddRange(new List<string>{
            "Surprise Presentation! \nYou cant draw new cards this turn.", "A Complete Tangent! \nEnemy gained +2 Damage", "My Classroom, My Rules! \nEnemy Defence WAY up! ", "BRAIN DRAIN!!\nYou're Overworked, and have less energy this turn."
        });

        attacks.AddRange(new List<string>{
            "Pop Quiz! \n" + damage + " damage taken", "Assign Exam!\n" + damage + " damage taken", "Harsh Feedback!\n" + damage + " damage taken"
        });

        defences.AddRange(new List<string>{
            "Union Strike!\nDefence +" + defence, "Take a Holiday!\nDefence +" + defence
        });

        specials.AddRange(new List<string>{
            "Surprise Presentation! \nYou cant draw new cards this turn.", "A Complete Tangent! \nEnemy gained +2 Damage", "My Classroom, My Rules! \nEnemy Defence WAY up! ", "BRAIN DRAIN!!\nYou're Overworked, and have less energy this turn."
        });
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
        //HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        HealthText.text = self.getCurrentHealth() + " / " + self.getMaxHealth() + "";

        SpecialText.text = "Moves until Enemy Special: " + (maxCharge - charge) + "";
        
        //DefenceText.text = "Enemy Defence: " + self.defence + "";
        DefenceText.text =  self.defence + "";

        specialbar.SetSpecial(charge);

        //Enemy health Bar us updated by getting the current health
        if(healthbar != null)
        {
            healthbar.SetHealth(self.getCurrentHealth());

        }
        if (defencebar != null)
        {
            defencebar.SetDefence(self.getCurrentDefence());
        }
    }

    //apply the damage int to player
    public void attack()
    {
        Debug.Log("Attacking");
        int random = Random.Range(0, attacks.Count);
        player.takeDamage(damage);
        MoveText.text = attacks[random];
        movesRemaining -= attackCost;
    }

    //use special attack
    public void special()
    {

        // special animation to play
        self.applyEnemyAnim("enemySpecial");
        AudioManager.instance.PlaySound("Special Move");

        //probably needs to call a separate script? each enemy type has its own special
        //can that just be done here or is that too messy

        //the logic for charging this can be done with the turnCounter Value and calculating when there is no remainder when divided by a charge threshold? maybe.
        int random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                player.skipDraw = true;
                charge = 0;
                MoveText.text = specials[0];
                break;

            case 2:
                damage++;
                damage++;
                charge = 0;
                attacks.Clear();
                attacks.AddRange(new List<string>{
                "Pop Quiz! \n" + damage + " damage taken", "Assign Exam!\n" + damage + " damage taken", "Harsh Feedback!\n" + damage + " damage taken"
                });
                MoveText.text = specials[1];
                break;

            case 3:
                self.gainDefence(defence * 3);
                charge = 0;
                MoveText.text = specials[2];
                break;

            case 4:
                player.drained = true;
                charge = 0;
                MoveText.text = specials[3];
                break;


        }



        //HealthText.text = "Enemy Health: " + self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        HealthText.text = self.getCurrentHealth() + " / " + self.getMaxHealth() + "";
        movesRemaining -= specialCost;
    }

    //gain defence equal to defend value
    public void defend()
    {
        int random = Random.Range(0, defences.Count);
        Debug.Log("Defending");
        self.gainDefence(defence);
        MoveText.text = defences[random];
        movesRemaining -= defenceCost;
        //DefenceText.text = "Enemy Defence: " + self.defence + "";
        DefenceText.text = self.defence + "";
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
        if(charge < maxCharge)
        {
            charge++;
        }

        while (movesRemaining > 0)
        {
            Debug.Log("entering while loop");
            yield return new WaitForSeconds(wait);
            int random = Random.Range(1, 3);
            if(charge == maxCharge)
            {
                random = 3;
            }
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
        yield return new WaitForSeconds(wait);
        updateUI();
        endTurn();
    }

    // change cursor on enemy hover to show it is interactible
    public void OnMouseEnter()
    {
        // if selected card is attack card - ability to interact with enemies only and not player --
        if (playerHand.currentlySelectedCard.group == "Attack")
        {
            Cursor.SetCursor(cursorSword, Vector2.zero, cursorMode);
        }
        else if (playerHand.currentlySelectedCard.group == "Defence")
        {
            Cursor.SetCursor(cursorX, Vector2.zero, cursorMode);
        }
        else if (playerHand.currentlySelectedCard.group == "Special")
        {
            Cursor.SetCursor(cursorX, Vector2.zero, cursorMode);
        }
        else { OnMouseExit(); }
    }

    // return cursor to arrow when exit interactable object
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
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
