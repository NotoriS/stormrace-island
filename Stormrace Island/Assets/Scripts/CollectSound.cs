using UnityEngine;

public class CollectSound : MonoBehaviour
{
    private AudioSource audioSource;
    private float clipLength;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipLength = audioSource.clip.length + 0.2f;
    }

    private void Update()
    {
        clipLength -= Time.deltaTime;
        if (clipLength < 0) Destroy(gameObject);
    }
}
