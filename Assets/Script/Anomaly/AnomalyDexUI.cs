using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnomalyDexUI : MonoBehaviour
{
    [SerializeField] private GameObject dexPanel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private TextMeshProUGUI progressText; // 선택 사항, 없으면 null 체크로 건너뜀

    private bool isOpen;

    private void Start()
    {
        dexPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOpen = !isOpen;
            dexPanel.SetActive(isOpen);

            if (isOpen)
            {
                RefreshList();
            }
        }
    }

    private void RefreshList()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<AnomalyDexManager.DexEntry> unlocked = AnomalyDexManager.Instance.GetUnlockedEntries();
        int totalCount = AnomalyManager.Instance.GetTotalAnomalyCount();

        foreach (var entry in unlocked)
        {
            GameObject entryObj = Instantiate(entryPrefab, contentParent);
            entryObj.GetComponent<DexEntryUI>().SetData(entry.displayName, entry.description);
        }

        int lockedCount = totalCount - unlocked.Count;
        for (int i = 0; i < lockedCount; i++)
        {
            GameObject entryObj = Instantiate(entryPrefab, contentParent);
            entryObj.GetComponent<DexEntryUI>().SetLocked();
        }

        if (progressText != null)
        {
            progressText.text = $"발견: {unlocked.Count} / {totalCount}";
        }
    }
}