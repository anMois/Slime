using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject Coin_image;

    //private UIManager _uiManager;
    private Create_Slime slimes_count;
    public float shCoin_num;

    private void Awake()
    {
        Coin_image.SetActive(false);
        slimes_count = GameObject.Find("OBJ").GetComponent<Create_Slime>();
        //_uiManager = GameObject.Find("OBJ").GetComponent<UIManager>();
    }

    private void Update()
    {

    }

    public void CheckSlime()
    {
        
    }

    public void AddCoin()
    {
        Coin_image.SetActive(false);
        //_uiManager.goldscore = (int)shCoin_num + _uiManager.goldscore;

        shCoin_num = 0.0f;
    }
}
