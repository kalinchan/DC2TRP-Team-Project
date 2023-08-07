using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GradeReportLogic : MonoBehaviour
{

    public TMP_Text gradeL1;
    public TMP_Text gradeL2;
    public TMP_Text gradeL3;
    public TMP_Text gradeL4;
    public TMP_Text gradeL5;

    private GradeManager gradeManager;
    public GameObject progress;


    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("Progress");
        gradeManager = progress.GetComponent<GradeManager>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayReportCard()
    {
        Debug.Log("DisplayReportCard method invoked");
        // Load the grades from PlayerPrefs
        gradeManager.LoadGradesFromPlayerPrefs();
        Debug.Log("Successfully loaded grades");

        Debug.Log("Level1: " + gradeManager.getGrade("BattleScene"));
        Debug.Log("Level2: " + gradeManager.getGrade("BattleScene2"));
        Debug.Log("Level3: " + gradeManager.getGrade("BattleScene3"));
        Debug.Log("Level4: " + gradeManager.getGrade("BattleScene4"));
        Debug.Log("Level5: " + gradeManager.getGrade("BattleScene5"));

        gradeL1.text = "" + gradeManager.getGrade("BattleScene") + "";
        gradeL2.text = "" + gradeManager.getGrade("BattleScene2") + "";
        gradeL3.text = "" + gradeManager.getGrade("BattleScene3") + "";
        gradeL4.text = "" + gradeManager.getGrade("BattleScene4") + "";
        gradeL5.text = "" + gradeManager.getGrade("BattleScene5") + "";
    }
}
