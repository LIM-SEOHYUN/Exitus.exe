using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.currentStage == 0)
        {
            if (other.CompareTag("Entrance") || other.CompareTag("Exit"))
            {
                GameManager.Instance.Success();
                return;
            }
        }

        if (other.CompareTag("Entrance"))
        {
            if (AnomalyManager.Instance.CurrentStageHasAnomaly)
            {
                GameManager.Instance.Success();
            }
            else
            {
                GameManager.Instance.Fail();
            }
        }

        if (other.CompareTag("Exit"))
        {
            if (AnomalyManager.Instance.CurrentStageHasAnomaly)
            {
                GameManager.Instance.Fail();
            }
            else
            {
                GameManager.Instance.Success();
            }
        }
    }
    
}