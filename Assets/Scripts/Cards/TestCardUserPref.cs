using UnityEngine;

public class TestCardUserPref : CardUserPref
{
    // Override the methods you use in your tests with simple implementations

  
    public override GameObject GetCardByName(string cardName)
    {   
        // Return a mock GameObject or null based on your test needs
        return null;
    }
}