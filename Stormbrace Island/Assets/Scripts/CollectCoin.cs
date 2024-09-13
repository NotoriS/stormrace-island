using UnityEngine;

public class CollectCoin : MonoBehaviour, ICollectable
{
    private CoinWallet _wallet;

    private void Awake()
    {
        _wallet = GameObject.FindWithTag("Player").GetComponent<CoinWallet>();
    }

    public void Collect()
    {
        _wallet.AddCoins(1);
        Destroy(gameObject);
    }
}
