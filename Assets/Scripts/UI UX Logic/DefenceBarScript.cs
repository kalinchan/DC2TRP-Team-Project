using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceBarScript : MonoBehaviour
{
   public Slider slider;

   public void SetMaxDefence(int defence){
    slider.maxValue = defence;
    slider.value = defence;
   }

   public void SetDefence(int defence){
    slider.value = defence;
   }
}
