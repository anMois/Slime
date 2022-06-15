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
        if(slimes_count._slime.cleaner_s >= 2)
        {
            shCoin_num += Time.deltaTime;

            if (shCoin_num > 10.0f)
            {
                Coin_image.SetActive(true);
            }
        }
        
    }

    public void CheckSlime()
    {
        if (slimes_count._slime.cleaner_s >= 2)
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        else
            Debug.Log("클리너 슬라임이 부족합니다.");
    }

    public void AddCoin()
    {
        Coin_image.SetActive(false);
        //_uiManager.goldscore = (int)shCoin_num + _uiManager.goldscore;

        shCoin_num = 0.0f;
    }
}
