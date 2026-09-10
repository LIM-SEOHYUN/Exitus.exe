using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeAnomaly : AnomalyBase //AnomalyBase 상속
{
    private ObjectChangeAnomalyData MyData => data as ObjectChangeAnomalyData;

    public override void Activate()
    {
        if (MyData == null)
        {
            Debug.LogError($"{gameObject.name}: ObjectChangeAnomalyData가 연결되지 않음");
            return;
        }

        MapObject[] allMapObjects = FindObjectsOfType<MapObject>();

        foreach (MapObjectState state in MyData.objectStates)
        {
            MapObject target = System.Array.Find(allMapObjects, o => o.ObjectId == state.objectId);

            if (target != null)
            {
                target.ApplyState(state);
            }
        }
    }
}