using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUpdate : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public Money money;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.MoneyValue.ToString();
    }
}
