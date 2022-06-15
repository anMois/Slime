using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int Glod;

    public int SlimeCount;   
    int value;
    int MaxSlime;

    Slime _slime;

    public Text gold_Text;
    public Text slimecount_Text;

    private void Awake()
    {
        gold_Text = GameObject.Find("Gold/Text").GetComponent<Text>();
        slimecount_Text = GameObject.Find("SlimeCount/Text").GetComponent<Text>();

        _slime = GameObject.Find("GameManager").GetComponent<Create_Slime>()._slime;
    }

    private void Start()
    {
        MaxSlime = 10;
    }

    private void LateUpdate()
    {
        float num = Mathf.SmoothStep(value, Glod, 0.5f);
        
        gold_Text.text = String.Format("{0:n0}", num);
        value = (int)num;

        SlimeCount = _slime.orignal_s + _slime.poision_s + _slime.sticky_s + _slime.blood_s + _slime.acid_s;
        slimecount_Text.text = String.Format("{0} / {1}", SlimeCount, MaxSlime);
    }
}
