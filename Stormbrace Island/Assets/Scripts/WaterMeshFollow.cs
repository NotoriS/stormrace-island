using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeshFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Material _material;
    private Vector3 _lastTargetPosition;

    private void Awake()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _lastTargetPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPositionDelta = target.position - _lastTargetPosition;

        float materialPositionOffsetX = _material.GetFloat("_PlayerPositionX");
        float materialPositionOffsetZ = _material.GetFloat("_PlayerPositionZ");

        _material.SetFloat("_PlayerPositionX", materialPositionOffsetX + targetPositionDelta.x/transform.localScale.x);
        _material.SetFloat("_PlayerPositionZ", materialPositionOffsetZ + targetPositionDelta.z/transform.localScale.z);

        transform.position = new Vector3(transform.position.x + targetPositionDelta.x, transform.position.y, transform.position.z + targetPositionDelta.z);

        _lastTargetPosition = target.position;
    }
}
