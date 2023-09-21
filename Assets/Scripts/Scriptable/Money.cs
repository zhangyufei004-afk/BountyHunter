using UnityEngine;

[CreateAssetMenu]
public class Money : ScriptableObject
{
    public float MoneyValue;
    
    public void IncrementMoney()
    {
        MoneyValue++;
    }

    public void DecrementMoney(float value)
    {
        MoneyValue = MoneyValue - value;
    }
}
