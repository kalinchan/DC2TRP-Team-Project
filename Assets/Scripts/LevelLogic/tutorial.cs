using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{

    public GameObject playerStats;
    public GameObject enemyStats;
    public GameObject cards;

    public GameObject save;

    public GameObject grading;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats.SetActive(true);
        cards.SetActive(false);
        enemyStats.SetActive(false);
        save.SetActive(false);
        grading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerNext()
    {
        AudioManager.instance.PlaySound("Button Click");
        playerStats.SetActive(false);
        cards.SetActive(false);
        enemyStats.SetActive(true);
        save.SetActive(false);
        grading.SetActive(false);
    }

    public void enemyNext()
    {
        AudioManager.instance.PlaySound("Button Click");
        playerStats.SetActive(false);
        cards.SetActive(true);
        enemyStats.SetActive(false);
        save.SetActive(false);
        grading.SetActive(false);
    }

    public void cardNext()
    {
        AudioManager.instance.PlaySound("Button Click");
        playerStats.SetActive(false);
        cards.SetActive(false);
        enemyStats.SetActive(false);
        save.SetActive(true);
        grading.SetActive(false);
    }

    public void savingNext()
    {
        AudioManager.instance.PlaySound("Button Click");
        playerStats.SetActive(false);
        cards.SetActive(false);
        enemyStats.SetActive(false);
        save.SetActive(false);
        grading.SetActive(true);
    }

    public void finish()
    {
        AudioManager.instance.PlaySound("Button Click");
        SceneManager.LoadScene("BattleScene");
    }
}
