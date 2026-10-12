using UnityEngine;

[CreateAssetMenu(
    fileName = "PanelImageAnomalyData",
    menuName = "Anomaly/Panel Image Anomaly Data"
)]
public class PanelImageAnomalyData : AnomalyData
{
    [Header("Target")]
    public string targetEntityId;

    [Header("Anomaly Image")]
    public Sprite anomalyImage;
}