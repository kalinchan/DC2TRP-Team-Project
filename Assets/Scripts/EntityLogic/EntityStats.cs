using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class EntityStats : MonoBehaviour
{
    public int health = 10, defence = 0, maxHealth = 10;
    public bool isDead, skipDraw, drained;
    public GameObject victoryScreen, victoryScreenReplay, optionsBackground, defeatScreen;
    private List<GameObject> resultDisable;
    private bool gameOver;
    private int specialInt;
    public int currentScene;
    public string currentSceneName;
    public LevelManager levelManager;
    public LevelLoad levelLoad;
    public GameObject self, enemy;
    public TMP_Text specialx2Text;

    // for special card attack multiplier
    public bool specialx2 = false;

    //animation
    public Animator animP;
    public Animator animE;

    private GameObject progress;
    private ProgressManager progressManager;
    private GradeManager gradeManager;
    public TurnManager turnManager;
    public TMP_Text V1grade;
    public TMP_Text VRgrade;
    public GradeReportLogic gradeReportLogic;



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
            GameObject.Find("Turn Manager"),
            GameObject.Find("Bin")
        };


        health = maxHealth;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        specialInt = currentScene - 3; // scene index 3 = level 1 - first card in the special card list [0] to be selected
        skipDraw = false;
        drained = false;

        GameObject backgroundObject = GameObject.Find("Background");
        if (backgroundObject != null)
        {
            levelLoad = backgroundObject.GetComponent<LevelLoad>();
            levelManager = backgroundObject.GetComponent<LevelManager>();


            self = GameObject.Find("Player");
            enemy = GameObject.Find("Enemy");
            animP = self.GetComponent<Animator>();
            animE = enemy.GetComponent<Animator>();
            specialx2Text = GameObject.Find("DoubleDamageActive").GetComponent<TextMeshProUGUI>();
            specialx2Text.enabled = false;

            progress = GameObject.Find("Progress");
            progressManager = progress.GetComponent<ProgressManager>();
            gradeManager = progress.GetComponent<GradeManager>();
            defeatScreen = GameObject.Find("Defeat");
            defeatScreen.SetActive(false);
            victoryScreen = GameObject.Find("Victory");
            victoryScreen.SetActive(false);
            victoryScreenReplay = GameObject.Find("VictoryReplay");
            victoryScreenReplay.SetActive(false);

            if (currentSceneName == "BattleScene5")
            {
                GameObject endGame = GameObject.Find("End Game");
                gradeReportLogic = endGame.GetComponent<GradeReportLogic>();
                endGame.SetActive(false);
            }
        }


        

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
                applyPlayerAnim("playerIsDead");
                StartCoroutine(DefeatScreenCoroutine());

            }

            else if (currentScene < levelManager.finalSceneId)
            {
                applyEnemyAnim("enemyIsDead");
                gradeManager.setVictoryGrade(currentSceneName);
                if (PlayerPrefs.GetInt("Player_Max_Level", 1) < currentScene) {
                    PlayerPrefs.SetInt("Player_Max_Level", currentScene);
                }
                // if it's the first playthrough, show the victory screen where the user gets a special card
                if (progressManager.getFirstPlayBool(currentSceneName)) 
                {
                    
                    StartCoroutine(VictoryScreenCoroutine());
                }
                // otherwise it's a replay and the user does not get a special card
                else
                {
                    
                    StartCoroutine(VictoryReplayScreenCoroutine());
                }
            }

            else 
            {
                applyEnemyAnim("enemyIsDead");
                gradeManager.setVictoryGrade(currentSceneName);
                StartCoroutine(EndGameScreenCoroutine());
            }

            gameOver = true;

        }
    }

    IEnumerator DefeatScreenCoroutine()
    {
        AudioManager.instance.PlaySound("End Level");
        yield return new WaitForSeconds(1);
        resultDisable.ForEach(x => x.SetActive(false));
        optionsBackground.SetActive(true);
        AudioManager.instance.PlaySound("Defeat Screen");
        defeatScreen.SetActive(true);
        gradeManager.ResetMoves();
    }

    IEnumerator VictoryScreenCoroutine()
    {
        AudioManager.instance.PlaySound("End Level");
        yield return new WaitForSeconds(1.2f);
        resultDisable.ForEach(x => x.SetActive(false));
        progressManager.setPlaythoughAsReplay(currentSceneName);
        optionsBackground.SetActive(true);
        victoryScreen.SetActive(true);
        V1grade = GameObject.Find("V1Grade").GetComponent<TextMeshProUGUI>();
        V1grade.text = gradeManager.getGrade(currentSceneName);
        levelLoad.DisplayCardAtEnd();
        progressManager.incrementLevel();
    }

    IEnumerator VictoryReplayScreenCoroutine()
    {
        AudioManager.instance.PlaySound("End Level");
        yield return new WaitForSeconds(1.2f);
        resultDisable.ForEach(x => x.SetActive(false));
        optionsBackground.SetActive(true);
        victoryScreenReplay.SetActive(true);
        VRgrade = GameObject.Find("VRGrade").GetComponent<TextMeshProUGUI>();
        VRgrade.text = gradeManager.getGrade(currentSceneName);
        Debug.Log("Victory Replay Grade: " + VRgrade.text);
        Debug.Log("GradeManager: " + gradeManager.getGrade(currentSceneName)); 
        gradeManager.PrintDictionaryContents();

    }



    IEnumerator EndGameScreenCoroutine()
    {
        Debug.Log("EndGameCoroutine method invoked");
        AudioManager.instance.PlaySound("End Level");
        
        yield return new WaitForSeconds(1.2f);

        resultDisable.ForEach(x => x.SetActive(false));
        AudioManager.instance.PlaySound("Victory Screen");
        Debug.Log("before getting scene");

        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);

        GetGradeReport();

        gradeManager.ResetMoves();
    }

    void GetGradeReport()
    {
        Debug.Log("GetGradeReport method invoked");
        if (gradeReportLogic != null)
        {
            gradeReportLogic.DisplayReportCard();
        }
        else
        {
            Debug.Log("Grade Report Logic is null");
        }
    }





    IEnumerator PlayerMoveCoroutine(string animation)
    {
        yield return new WaitForSeconds(1);
        disablePlayerAnim(animation);
    }

    IEnumerator EnemyMoveCoroutine(string animation)
    {
        yield return new WaitForSeconds(1f);
        disableEnemyAnim(animation);
    }

    // for enemy attacks -- ensures enemy attacks before player hurt animation displays
    IEnumerator EnemyAttack()
    {
        applyEnemyAnim("enemyAttack");
        AudioManager.instance.PlaySound("Attack");
        yield return new WaitForSeconds(0.4f);
        applyPlayerAnim("playerTakeDamage");
        AudioManager.instance.PlaySound("Player Damaged");
    }

    IEnumerator PlayerAttack()
    {
      
        applyPlayerAnim("playerAttack");
        AudioManager.instance.PlaySound("Attack");
        yield return new WaitForSeconds(0.3f);
        applyEnemyAnim("enemyTakeDamage");
        AudioManager.instance.PlaySound("Enemy Damaged");
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

    public void testTakeDamage(int damage) // FOR TESTING ONLY - SKIPS UNECCESSARIES LIKE ANIMATIONS ETC
    {
        damageEnemy(damage);


    }
    public void takeDamage(int damage)
    {

        if (gameObject.name.Equals("Enemy"))
        {
            StartCoroutine(PlayerAttack());
        }

        else
        {   
            StartCoroutine(EnemyAttack());
        }

        damageEnemy(damage);

    }

    public void damageEnemy(int damage) // separated for tesing and less duplication
    {

        if (specialx2 == true)
        {
            damage = damage * 2;
        }

        int damageRemainder = 0;
        if (defence > 0)
        {
            defence -= damage;
            if (defence < 0)
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
        if (gameObject.name.Equals("Enemy") && specialx2)
        {
            specialx2 = false;// after one turn
            changeSpecialx2Text();
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
        AudioManager.instance.PlaySound("Defence");
        if (gameObject.name.Equals("Player"))
        {
            
            applyPlayerAnim("playerAddDefence");
        }

        else
        {
            applyEnemyAnim("enemyDefence");
        }


    }

    public void gainDefenceTest(int defence) // for testing defence card, skipping animations and sounds
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

    public void setMultiplierToTrue()
    {
        specialx2 = true;
    }

    public void setMultiplierToFalse()
    {
        specialx2 = false;
    }

    // player animations

    public void applyPlayerAnim(string animation)
    {
        animP.SetBool(animation, true);
        StartCoroutine(PlayerMoveCoroutine(animation));
    }

    public void disablePlayerAnim(string animation)
    {
        animP.SetBool(animation, false);
        animP.SetBool("playerIdle", true);
    }

    // enemy anmiations

    public void applyEnemyAnim(string animation)
    {
        animE.SetBool("enemyIdle", false); 
        animE.SetBool(animation, true);
        StartCoroutine(EnemyMoveCoroutine(animation));
    }

    public void disableEnemyAnim(string animation)
    {
        animE.SetBool(animation, false);
        animE.SetBool("enemyIdle", true);
    }

    public void changeSpecialx2Text()
    {
        if (specialx2) { 
            specialx2Text.enabled = true;
        }

        if (!specialx2)
        {
            specialx2Text.enabled = false;
        }

    }
}
