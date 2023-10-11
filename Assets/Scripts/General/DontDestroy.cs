using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private FloatVariable[] floatVariables;
    [SerializeField] private ShopTimesBought[] shops;
    [SerializeField] private Money money;
    [SerializeField] private SceneChange[] scenes;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
