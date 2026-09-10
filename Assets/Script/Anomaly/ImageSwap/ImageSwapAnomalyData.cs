using UnityEngine;

[CreateAssetMenu(fileName = "ImageSwapAnomalyData", menuName = "Anomaly/Image Swap Anomaly Data")]
public class ImageSwapAnomalyData : AnomalyData
{
    [Header("Image Swap Target")]
    public string targetEntityId;

    [Header("Image Swap Result")]
    public Sprite swappedSprite;

    [Header("Interaction Option")]
    [Range(0f, 1f)]
    public float swapChance = 1f;
}