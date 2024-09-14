using UnityEngine;

public class CollectCoin : MonoBehaviour, ICollectable
{
    [SerializeField]
    private GameObject collectSoundPrefab;

    private CoinWallet _wallet;

    private void Awake()
    {
        _wallet = GameObject.FindWithTag("Player").GetComponent<CoinWallet>();
    }

    public void Collect()
    {
        _wallet.AddCoins(1);
        Instantiate(collectSoundPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
