using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{

    public GameObject playerStats;
    public GameObject enemyStats;
    public GameObject cards;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats.SetActive(true);
        cards.SetActive(false);
        enemyStats.SetActive(false);
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
    }

    public void enemyNext()
    {
        AudioManager.instance.PlaySound("Button Click");
        playerStats.SetActive(false);
        cards.SetActive(true);
        enemyStats.SetActive(false);
    }

    public void finish()
    {
        AudioManager.instance.PlaySound("Button Click");
        SceneManager.LoadScene("BattleScene");
    }
}
