using UnityEngine;

public class GuidePanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject guidePanel;

    public void OpenGuide()
    {
        guidePanel.SetActive(true);
    }

    public void CloseGuide()
    {
        guidePanel.SetActive(false);
    }
}