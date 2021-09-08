using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public UIManager gold;

    private void Awake()
    {
        gold = GameObject.Find("GameObject").GetComponent<UIManager>();
    }

    private void Start()
    {
        Debug.Log(gold.goldscore);
    }
}
