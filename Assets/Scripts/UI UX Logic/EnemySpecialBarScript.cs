using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpecialBarScript : MonoBehaviour
{

    //comment
     public Slider slider;

   public void SetMaxSpecial(int special){
    slider.maxValue = special;
    slider.value = special;
   }

   public void SetSpecial(int special){
    slider.value = special;
   }
}
