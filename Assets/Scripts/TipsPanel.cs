using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TipsPanel : MonoBehaviour
{
    [SerializeField] private GameObject tipsPanel;
    [SerializeField] private float fadeOutDuration = 0.6f;
    [SerializeField] private float fadeInDuration = 0.4f;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;

    private int _tipsPanelCount;
    private int _currentPanel;
    private List<TextMeshProUGUI> _tipsList;
    private bool _canPushButtons = true;

    private void Awake()
    {
        _tipsPanelCount = tipsPanel.transform.childCount;
        _tipsList = new List<TextMeshProUGUI>();
        foreach (Transform tipChild in tipsPanel.transform)
        {
            if (tipChild.TryGetComponent<TextMeshProUGUI>(out var tip))
                _tipsList.Add(tip);
        }
        
        ActivateRandomTip();
    }

    private void ActivateRandomTip()
    {
        var randomIndex = Random.Range(0, _tipsPanelCount);
        _tipsList[randomIndex].gameObject.SetActive(true);
        _currentPanel = randomIndex;
    }

    private void Update()
    {
        if (!_canPushButtons) return;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            MoveRight();
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            MoveLeft();
    }

    public void MoveRight()
    {
        if (!_canPushButtons) return;
        _canPushButtons = false;
        if (_currentPanel < _tipsPanelCount - 1)
        {
            _currentPanel++;
            FadeOut(_tipsList[_currentPanel - 1]);
            FadeIn(_tipsList[_currentPanel]);
        }
        else
        {
            FadeOut(_tipsList[_currentPanel]);
            FadeIn(_tipsList[0]);
            _currentPanel = 0;
        }
    }

    public void MoveLeft()
    {
        if (!_canPushButtons) return;
        _canPushButtons = false;
        if (_currentPanel > 0)
        {
            _currentPanel--;
            FadeOut(_tipsList[_currentPanel + 1]);
            FadeIn(_tipsList[_currentPanel]);
        }
        else
        {
            FadeOut(_tipsList[_currentPanel]);
            FadeIn(_tipsList[_tipsPanelCount - 1]);
            _currentPanel = _tipsPanelCount - 1;
        }
    }

    private void FadeOut(TextMeshProUGUI panel) =>
        panel
            .DOFade(0, fadeOutDuration)
            .SetEase(fadeEase)
            .OnComplete((() =>
                panel.gameObject.SetActive(false)));

    private void FadeIn(TextMeshProUGUI panel) =>
        panel
            .DOFade(1, fadeInDuration)
            .SetEase(fadeEase)
            .OnComplete((() =>
            {
                panel.gameObject.SetActive(true);
                _canPushButtons = true;
            }));
}