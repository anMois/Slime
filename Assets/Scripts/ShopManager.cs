using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    int G_value;

    Text gold_Text;

    GameManager _Gm;

    private void Awake()
    {
        _Gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        gold_Text = GameObject.Find("Canvas/Gold/Text").GetComponent<Text>();
    }

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        float gold_num = Mathf.SmoothStep(G_value, _Gm.gold, 0.5f);
        gold_Text.text = String.Format("{0:n0}", gold_num);
        G_value = (int)gold_num;
    }

    public void GoHome()
    {
        DataManager.instance.JsonSave();
        SceneManager.LoadScene("main");
    }
}
