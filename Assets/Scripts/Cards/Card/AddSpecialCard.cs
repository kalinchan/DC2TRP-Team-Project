using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpecialCard : MonoBehaviour


{

    private GameObject levelL;

    // Start is called before the first frame update
    void Start()
    {
        levelL = GameObject.Find("Background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        levelL.GetComponent<LevelLoad>().addSpecial();
    }
}
