using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RespawnManager.Instance.RespawnPoint = transform.position;
        }
    }
}
