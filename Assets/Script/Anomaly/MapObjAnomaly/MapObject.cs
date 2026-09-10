using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField] private string objectId;

    public string ObjectId => objectId;

    public void ApplyState(MapObjectState state)
    {
        gameObject.SetActive(state.isActive);

        if (state.isActive)
        {
            transform.localPosition = state.position;
            transform.rotation = Quaternion.Euler(0f, 0f, state.rotationZ);
        }
    }
}
