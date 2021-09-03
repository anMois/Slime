using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text goldscore_text;

    private int goldscore = 0;
    private float delaytime;

    private void Awake()
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
