using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Title("GlitchAnim")]
    [SerializeField] private float intensityOnHover = 1f;
    [SerializeField] private float intensityOnExit = 0f;
    [SerializeField] private float hoverAnimDuration = 0.5f;
    [SerializeField] private float moveIntensity = 0.1f;
    [SerializeField] private Texture mainTexture;
    [SerializeField] private Shader buttonShader;
    [Title("TextAnim")]
    [SerializeField] private Color colorOnHover = Color.white;
    [SerializeField] private Color colorOnExit = Color.black;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Image _img;
    private Material _localMaterial;
    private static readonly int Intensity = Shader.PropertyToID("Intensity");
    private static readonly int MainTex = Shader.PropertyToID("mainTex");
  
    private static readonly int MoveIntensity = Shader.PropertyToID("MoveIntensity");


    private void Awake()
    {
        _localMaterial = new Material(buttonShader);
        _img = GetComponent<Image>();
        _img.material = _localMaterial;
        _localMaterial.SetFloat(Intensity, 0f);
        _localMaterial.SetTexture(MainTex, mainTexture);
        _localMaterial.SetFloat(MoveIntensity, moveIntensity);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("fuck off unity");
        buttonText.DOColor(colorOnHover, hoverAnimDuration);
        _img.material.DOFloat(intensityOnHover, Intensity, hoverAnimDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("hel");

        buttonText.DOColor(colorOnExit, hoverAnimDuration);
        _img.material.DOFloat(intensityOnExit, Intensity, hoverAnimDuration);
    }
}