using UnityEngine;
using TMPro;

public class DexEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetData(string displayName, string description)
    {
        nameText.text = displayName;
        descriptionText.text = description;
    }

    public void SetLocked()
    {
        nameText.text = "???";
        descriptionText.text = "아직 발견하지 못한 이상현상입니다.";
    }
}