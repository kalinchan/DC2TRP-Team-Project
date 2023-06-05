using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class EntityStats : MonoBehaviour
{
    public int health = 10, defence = 0, maxHealth = 10;
    public bool isDead, skipDraw, drained;
    public GameObject victoryScreen, optionsBackground, defeatScreen;
    private List<GameObject> resultDisable;
    private bool gameOver;
    private int specialInt;
    public int currentScene;
    public LevelManager levelManager;
    public GameObject self;

    //special card at end of level
    public GameObject SpecialCard01, SpecialCard02, SpecialCard03, SpecialCard04;
    public GameObject SpecialCardArea;
    public List<GameObject> SpecialCards = new List<GameObject>();

    // for special card attack multiplier
    public bool specialx2 = false;

    //animation
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        resultDisable = new List<GameObject>
        {
            GameObject.Find("Background"),
            GameObject.Find("End Turn Button"),
            GameObject.Find("Enemy"),
            GameObject.Find("Player"),
            GameObject.Find("ActiveArea"),
            GameObject.Find("Turn Manager")
        };


        health = maxHealth;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        specialInt = currentScene - 3; // scene index 3 = level 1 - first card in the special card list [0] to be selected
        skipDraw = false;
        drained = false;
        levelManager = GameObject.Find("Background").GetComponent<LevelManager>();
        self = GameObject.Find("Player");
        anim = self.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (isDead || health <= 0)
        {

            if (gameObject.name.Equals("Player"))
            {
                applyPlayerDeathAnim();
                StartCoroutine(DefeatScreenCoroutine());

            }
            else if (currentScene < levelManager.finalSceneId)
            {
                StartCoroutine(VictoryScreenCoroutine());
            }

            else {
                StartCoroutine(EndGameScreenCoroutine());
            }

            gameOver = true;

        }


    }

    IEnumerator DefeatScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        resultDisable.ForEach(x => x.SetActive(false));
        optionsBackground.SetActive(true);
        defeatScreen.SetActive(true);
    }

    IEnumerator VictoryScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        resultDisable.ForEach(x => x.SetActive(false));
        optionsBackground.SetActive(true);
        AddSpecialCard();
        victoryScreen.SetActive(true);
    }

    IEnumerator EndGameScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        resultDisable.ForEach(x => x.SetActive(false));
        SceneManager.LoadScene("EndGame");

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

        if (specialx2 == true)
        {
            damage = damage * 2;
        }

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

        specialx2 = false; // after one turn  
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

    // get cuttent health velue
    public int getCurrentHealth()
    {
        return health;
    }

    // get maximum health value
    public int getMaxHealth()
    {
        return maxHealth;
    }

    // get current defence value 
    public int getCurrentDefence()
    {
        return defence;
    }

    // add special card to victory screen, showing what was rewarded
    private void AddSpecialCard()
    {
        // get all special cards
        SpecialCards.AddRange(new List<GameObject>
            {
                SpecialCard01, SpecialCard02, SpecialCard03, SpecialCard04

            }
        );

        // instantiate special card depending on which level is completed --
        GameObject specialCard = Instantiate(SpecialCards[specialInt], new Vector3(0, 0, 0), Quaternion.identity);
        specialCard.transform.SetParent(SpecialCardArea.transform, false);

        // to get int for special card list --
        if (specialInt == 4)
        {
            specialInt = 0;
        }
        else { specialInt++; }

    }

    public void setMultiplierToTrue()
    {
        specialx2 = true;
    }

    public void setMultiplierToFalse()
    {
        specialx2 = false;
    }

    // player animations

    public void applyPlayerDefenceAnim()
    {
        anim.SetBool("playerAddDefence", true);
    }

    public void applyPlayerTakeDamageAnim()
    {
        anim.SetBool("playerTakeDamage", true);
    }

    public void applyPlayerAttackAnim()
    {
        anim.SetBool("playerAttack", true);
    }

    public void applyPlayerSpecialAnim()
    {
        anim.SetBool("playerSpecial", true);
    }

    public void applyPlayerDeathAnim()
    {
        anim.SetBool("playerIsDead", true);
    }

    // enemy anmiations

    public void applyEnemyDefenceAnim()
    {
        anim.SetBool("enemyAddDefence", true);
    }

    public void applyEnemyTakeDamageAnim()
    {
        anim.SetBool("enemyTakeDamage", true);
    }

    public void applyEnemyAttackAnim()
    {
        anim.SetBool("enemyAttack", true);
    }

    public void applyEnemySpecialAnim()
    {
        anim.SetBool("enemySpecial", true);
    }

    public void applyEnemyDeathAnim()
    {
        anim.SetBool("enemyIsDead", true);
    }



}
