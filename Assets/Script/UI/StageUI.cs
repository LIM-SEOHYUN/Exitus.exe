using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI successText;
    
    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;

    public void UpdateStage(int stage)
    {
        stageText.text = "STAGE " + stage;
    }

    public void UpdateSuccess(int count)
    {
        successText.text = "SUCCESS " + count + " / 8";  

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < count)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].color = new Color(0.4f, 0.4f, 0.4f);
            }
        }
    }
}