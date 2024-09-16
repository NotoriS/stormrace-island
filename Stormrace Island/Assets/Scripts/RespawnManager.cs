using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : Singleton<RespawnManager>
{
    [SerializeField]
    private float timeRemovedByRespawn;
    [SerializeField]
    private CharacterController characterController;

    public Vector3 RespawnPoint { get; set; }

    private void Awake()
    {
        RespawnPoint = new Vector3 (0f, 2f, 0f);
    }

    public void RespawnPlayer()
    {
        characterController.enabled = false;
        characterController.transform.position = RespawnPoint;
        characterController.enabled = true;
        FindFirstObjectByType<GameTimer>().RemoveTime(timeRemovedByRespawn);
    }
}
