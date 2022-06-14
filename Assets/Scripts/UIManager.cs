using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int gold;
    int value;

    public Text gold_text;

    private void Awake()
    {
        gold_text = GameObject.Find("Gold/Text").GetComponent<Text>();
    }

    private void Update()
    {
        value = gold;
    }

    private void LateUpdate()
    {
        float num = Mathf.SmoothStep(value, gold, 0.5f);
        
        gold_text.text = String.Format("{0:n0}", num);
        num = value;
    }
}
