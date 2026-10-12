using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnomalyBase : MonoBehaviour
{

    [SerializeField] protected AnomalyData data;

    public string AnomalyId => data.AnomalyId;
    public string DisplayName => data.DisplayName;   // 추가: 도감 표시용
    public string Description => data.Description;    // 추가: 도감 표시용

    public abstract void Activate();

    public virtual bool IsResolved() => true;

    public virtual void Deactivate() { }
}