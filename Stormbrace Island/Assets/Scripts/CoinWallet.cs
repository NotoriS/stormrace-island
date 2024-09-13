using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    public int CoinCount { get; private set; }

    public void AddCoins(int amount)
    {
        CoinCount += amount;
    }

    public void RemoveCoins(int amount)
    {
        CoinCount -= amount;
        if (CoinCount < 0) CoinCount = 0;
    }
}
