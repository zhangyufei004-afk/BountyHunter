using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpdate : MonoBehaviour
{
    public FloatVariable statToUpdate;
    public float amountToUpdate;

    public void OnBuyButton()
    {
        statToUpdate.Value += amountToUpdate;
    }

}
