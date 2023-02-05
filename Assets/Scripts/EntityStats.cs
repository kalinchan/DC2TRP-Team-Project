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
