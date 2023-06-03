using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float intensityOnHover = 1f;
    public float intensityOnExit = 0f;
    public Shader buttonShader;

    private Image img;
    private Material localMaterial;
    private void Awake()
    { 
        localMaterial = new Material(buttonShader);
        img = GetComponent<Image>();
        img.material = localMaterial;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("fuck off unity");
        localMaterial.SetFloat("Intensity", intensityOnHover);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("hel");
        localMaterial.SetFloat("Intensity", intensityOnExit);
    }
}
