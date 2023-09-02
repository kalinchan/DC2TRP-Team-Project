using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public List<GameObject> gameObjects;
    public GameObject optionsMenu;
    public GameObject optionsBackground;
    public GameObject victory;
    public GameObject victoryReplay;
    public GameObject defeat;
    public GameObject endGame;
    public TurnManager turnManager;
    public bool state;

    // Start is called before the first frame update
    void Start()
    {
        state = false;
        endGame = GameObject.Find("EndGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (state)
            {
                return;
            }
            if (victory.activeInHierarchy)
            {
                return;
            }
            if (victoryReplay.activeInHierarchy)
            {
                return;
            }
            if (defeat.activeInHierarchy)
            {
                return;
            }
            if(endGame!=null)
            {
                if (endGame.activeInHierarchy)
                {
                    return;
                }
            }
            if (turnManager.currentTurn == TurnManager.turnStatus.enemyTurn)
            {
                return;
            }
            state = true;
            gameObjects.ForEach(obj =>
            {
                bool current = obj.activeSelf;
                obj.SetActive(!current);
            });
            optionsBackground.SetActive(true);
            optionsMenu.SetActive(true);
        }
    }
    public void back()
    {
        gameObjects.ForEach(obj =>
        {
            bool current = obj.activeSelf;
            obj.SetActive(!current);
        });
        optionsBackground.SetActive(false);
        optionsMenu.SetActive(false);
        state = false;
    }
}
