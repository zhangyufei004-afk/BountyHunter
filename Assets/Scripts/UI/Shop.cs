using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject mapUI;
    public TextMeshProUGUI coins;
    public Money money;
    // Start is called before the first frame update
    void Start()
    {
        shopUI.SetActive(false);
        mapUI.SetActive(true);
    }

    void Update()
    {
        coins.text = money.MoneyValue.ToString();
    }

    public void OnShopButton()
    {
        shopUI.SetActive(true);
        mapUI.SetActive(false);
    }
    
    public void OnMapButton()
    {
        shopUI.SetActive(false);
        mapUI.SetActive(true);
    }
}
