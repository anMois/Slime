﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text goldscore_text;

    public int goldscore = 0;
    private float delaytime;

    private void Awake()
    {
        goldscore_text = GameObject.Find("Canvas/Gold/Gold_Text").GetComponent<Text>();
    }

    private void Start()
    {
        goldscore_text.text = ": " + goldscore.ToString();
    }

    private void Update()
    {
        delaytime += Time.deltaTime;

        if (delaytime > 2.0f)
        {
            goldscore++;
            goldscore_text.text = ": " + goldscore.ToString();
            
            delaytime = 0.0f;
        }
    }
}
