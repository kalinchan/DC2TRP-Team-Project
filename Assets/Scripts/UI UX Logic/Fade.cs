using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public Animator fadeAnimator;
    public GameObject fadeScreen;


    // Start is called before the first frame update
    void Start()
    {
        fadeAnimator.keepAnimatorControllerStateOnDisable = true;
        fadeScreen.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeInCoroutine()
    {
        fadeAnimator.SetBool("FadeOut", false);
        fadeScreen = GameObject.Find("FadeScreen");
        yield return new WaitForSeconds(1);
        fadeScreen.SetActive(false);

    }


}
