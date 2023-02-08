using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int Health = 10, Defence = 0, Damage = 0;
    private bool IsDead;
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
    //if Health is 0 then isDead becomes true! So we can do something with that! - JD
    public void TakeDamage (int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;
        }
    }
}
