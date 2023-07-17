using UnityEngine;
using System.Collections.Generic;

//this is to store the deck and special cars in user preferecnces


public class PlayerPrefsExample : MonoBehaviour
{
    [System.Serializable]
    public class MyGameObject
    {
        public string name;
        public int score;
    }

    // Convert a list of objects to a string separated by commas
    public string ConvertListToString(List<MyGameObject> objects)
    {
        string result = string.Empty;

        for (int i = 0; i < objects.Count; i++)
        {
            result += objects[i].name + ":" + objects[i].score;

            if (i < objects.Count - 1)
            {
                result += ",";
            }
        }

        return result;
    }

    // Convert a string to a list of objects using commas as separators
    public List<MyGameObject> ConvertStringToList(string data)
    {
        List<MyGameObject> objects = new List<MyGameObject>();

        string[] objectData = data.Split(',');

        foreach (string obj in objectData)
        {
            string[] properties = obj.Split(':');

            if (properties.Length == 2)
            {
                MyGameObject newObj = new MyGameObject();
                newObj.name = properties[0];
                newObj.score = int.Parse(properties[1]);

                objects.Add(newObj);
            }
        }

        return objects;
    }

    // Example usage
    void Start()
    {
        
    }
}
