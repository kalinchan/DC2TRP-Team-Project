using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI theText;

    public void OnPointerEnter(PointerEventData eventData) => theText.color = new Color32(66,170,221,225); //Or however you do your color

    public void OnPointerExit(PointerEventData eventData) => theText.color = Color.white; //Or however you do your color


}
