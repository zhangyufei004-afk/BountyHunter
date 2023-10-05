using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseBuy : MonoBehaviour
{
    public GameObject[] purchasedUI;
    public GameObject[] avialableUI;
    public TextMeshProUGUI costText;
    public GameObject buyButton;
    public Money money;
    public ShopTimesBought timesBought;

    public float[] cost;
    private float activeCost;
    private int purchasedLength;

    // Start is called before the first frame update
    public void OnShopButton()
    {
        for(int i = 0; i < purchasedUI.Length; i++)
        {
            purchasedUI[i].SetActive(false);
            avialableUI[i].SetActive(true);
        }
        

        if(timesBought.TimesShopped >= purchasedUI.Length)
        {
            buyButton.SetActive(false);
            for(int i = 0; i < purchasedUI.Length; i++)
            {
                purchasedUI[i].SetActive(true);
            }
        }
        else
        {
            for(int i = 0; i < timesBought.TimesShopped; i++)
            {
                purchasedUI[i].SetActive(true);
            }
            purchasedLength = timesBought.TimesShopped;
            activeCost = cost[purchasedLength];
            costText.text = activeCost.ToString();
        }
    }

    public void OnBuyButton()
    {
        if(money.MoneyValue >= activeCost && purchasedLength < purchasedUI.Length)
        {
            purchasedUI[purchasedLength].SetActive(true);
            money.DecrementMoney(activeCost);
            // avialableUI[purchasedLength].SetActive(false);
            purchasedLength++;
            timesBought.TimesShopped++;
            if(purchasedLength >= purchasedUI.Length)
            {
                buyButton.SetActive(false);
            }
            else
            {
                activeCost = cost[purchasedLength];
                costText.text = activeCost.ToString();
            }
        }
    }
}
