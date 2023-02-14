using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int health = 10, defence = 0, maxHealth = 100;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when the enemy takes damage.
    //Performs a check if the Health drops to 0 or below
    //Sanity check by setting Health to 0 if negative (hopefully avoids janky funk)
    //if Health is 0 then isDead becomes true! So we can do something with that! - JD 08/02

    //Adjusted Method to consider defence value first. If DEF > 0 then the damage will go to DEF first and then the
    //remainder to the player health. This could be refactored to have the HP adjustment as a separate method that is
    //called to tidy it up, but right now this will do. - JD 09/02

    //NEEDS TESTING - JD 09/02
    //Tested...funnily enough doing - on a - number adds so...changed that. lol - JD 09/02
    public void takeDamage (int damage)
    {
        int damageRemainder = 0;
        if(defence > 0)
        {
            defence -= damage;
            if(defence < 0)
            {
                damageRemainder = -defence;
                health -= damageRemainder;
                defence = 0;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }

            }
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }
        }
        
       
    }

    //Called when the entity (player or enemy) is healed
    //Caps the max hp to the maxHealth. - JD 09/02
    public void heal (int healing)
    {
        health += healing;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    //Called when an entity gains Defence
    public void gainDefence (int defence)
    {
        this.defence += defence;
    }

    
}
