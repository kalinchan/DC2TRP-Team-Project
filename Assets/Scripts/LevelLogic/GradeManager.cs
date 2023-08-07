using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GradeManager : MonoBehaviour
{
    public string grade;
    public Dictionary<string, string> gradeDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); // level : grade
    public TurnManager turnManager;
    public int moves;
    private const string GradeDict = "GradeDict";


    // Start is called before the first frame update
    void Start()
    {
        ResetMoves();
        Debug.Log("GradeManager Started");
        LoadGradesFromPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads
        Debug.Log("GradeManager Awake");
        LoadGradesFromPlayerPrefs();
    }



    public void setVictoryGrade(string level)
    {
        string grade;
        if (moves <= 3)
        {
            grade = "A+";
        }
        else if (moves >= 4 && moves <= 6)
        {
            grade = "A";
        }
        else if (moves >= 7 && moves <= 9)
        {
            grade = "B";
        }
        else if (moves >= 10 && moves <= 12)
        {
            grade = "C";
        }
        else
        {
            grade = "D";
        }

        // Check if the level key already exists in the dictionary with a different case
        if (gradeDict.TryGetValue(level, out string currentGrade) &&
            !string.Equals(grade, currentGrade, StringComparison.OrdinalIgnoreCase))
        {
            // Clear the existing entry with a different case
            gradeDict.Remove(level);
        }

        // Update the grade for the level
        gradeDict[level] = grade;

        ResetMoves();
        SaveGradesToPlayerPrefs();
        Debug.Log("Level Grade: " + getGrade(level));
    }


    public void PrintDictionaryContents()
    {
        foreach (var kvp in gradeDict)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }


    // get grade for that level
    public string getGrade(string level)
    {
        string grade;
        gradeDict.TryGetValue(level, out grade);
        return grade;
    }

    public void ResetGrades()
    {
        gradeDict.Clear();
        SaveGradesToPlayerPrefs();
    }

    public void IncrementMoves()
    {
        moves++;
    }

    public void ResetMoves()
    {
        moves = -1;
    }

    public void SaveGradesToPlayerPrefs()
    {
        // Serialize the dictionary to a JSON string
        string serializedGrades = JsonUtility.ToJson(new GradeDictWrapper(gradeDict));

        // Save the JSON string to PlayerPrefs
        PlayerPrefs.SetString("GradeDict", serializedGrades);

        // Call Save to actually write the data to disk (optional, but ensures data is saved immediately)
        PlayerPrefs.Save();
    }

    public void LoadGradesFromPlayerPrefs()
    {
        // Get the JSON string from PlayerPrefs
        string serializedGrades = PlayerPrefs.GetString("GradeDict", string.Empty);

        Debug.Log("Serialized Grades: " + serializedGrades);

        // Deserialize the JSON string back to the wrapper class
        GradeDictWrapper wrapper = JsonUtility.FromJson<GradeDictWrapper>(serializedGrades);

        // Populate the gradeDict from the wrapper class
        if (wrapper != null)
        {
            gradeDict = wrapper.ToDictionary();
        }
    }

    // Wrapper class to enable serialization/deserialization of the dictionary
    [Serializable]
    private class GradeDictWrapper
    {
        public List<string> keys;
        public List<string> values;

        public GradeDictWrapper(Dictionary<string, string> dictionary)
        {
            keys = new List<string>(dictionary.Keys);
            values = new List<string>(dictionary.Values);
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(keys.Count);
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            return dictionary;
        }
    }

    private void OnApplicationQuit()
    {
        SaveGradesToPlayerPrefs();
    }
}
