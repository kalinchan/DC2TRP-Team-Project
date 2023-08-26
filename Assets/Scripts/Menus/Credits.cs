using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float delay = 33;
    void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // on LMB click
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }


}
