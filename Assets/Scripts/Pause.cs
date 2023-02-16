using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public List<GameObject> gameObjects;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameObjects.ForEach(obj =>
           {
               obj.SetActive(false);
           });
            optionsMenu.SetActive(true);
        }
    }
    public void back()
    {
        gameObjects.ForEach(obj =>
        {
            obj.SetActive(true);
        });
        optionsMenu.SetActive(false);
    }
}
