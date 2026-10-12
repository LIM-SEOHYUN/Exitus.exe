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

    private Vector3 originalLocalPosition;
    private float originalRotationZ;
    private bool originalActive;
    private void Awake()
    {
        originalLocalPosition = transform.localPosition;
        originalRotationZ = transform.eulerAngles.z;
        originalActive = gameObject.activeSelf;
    }

    public void ResetToOriginal()
    {
        gameObject.SetActive(originalActive);
        transform.localPosition = originalLocalPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, originalRotationZ);
    }
}
