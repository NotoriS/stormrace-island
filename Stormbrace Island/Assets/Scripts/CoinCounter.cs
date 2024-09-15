using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField]
    private CoinWallet coinWallet;

    private TextMeshProUGUI _coinCountText;

    private void Awake()
    {
        _coinCountText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _coinCountText.text = coinWallet.CoinCount.ToString();
    }
}
