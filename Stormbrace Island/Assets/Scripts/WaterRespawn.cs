using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RespawnManager.Instance.RespawnPlayer();
        }
    }
}
