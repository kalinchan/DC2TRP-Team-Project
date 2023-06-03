using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public List<GameObject> gameObjects;
    public GameObject optionsMenu;
    public GameObject optionsBackground;
    public bool state;
    // Start is called before the first frame update
    void Start()
    {
        state = false;   
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
