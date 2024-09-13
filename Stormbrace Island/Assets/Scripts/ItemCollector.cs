using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
