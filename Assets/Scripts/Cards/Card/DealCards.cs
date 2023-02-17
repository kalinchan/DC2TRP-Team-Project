using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCards : MonoBehaviour
{

    private GameObject levelL;
    public GameObject dCButton;


    // Start is called before the first frame update
    void Start()
    {
        dCButton = GameObject.Find("Deal Cards Button");
        levelL = GameObject.Find("Background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        levelL.GetComponent<LevelLoad>().initialiseHand();
    }
}
