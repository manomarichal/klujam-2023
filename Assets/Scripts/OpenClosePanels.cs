using Sirenix.OdinInspector;
using UnityEngine;

public class OpenClosePanels : MonoBehaviour
{
    [SerializeField] private int panelNumberToOpen;

    [SerializeField] private bool needToClosePanel;

    [ShowIf("needToClosePanel")]
    [SerializeField] private GameObject panelToClose;

    public void OpenPanel()
    {
        PanelsManager.Instance.OpenPanel(panelNumberToOpen);
        TryClose();
    }

    private void TryClose()
    {
        if (needToClosePanel)
        {
            panelToClose.SetActive(false);
        }
    }
}
