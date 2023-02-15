using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    private GameObject turnManager;
    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("Turn Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
  
        turnManager.GetComponent<TurnManager>().switchTurn();
    }
}
