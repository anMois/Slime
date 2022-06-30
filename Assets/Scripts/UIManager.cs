using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public int Gold;
    public int SlimeCount;
    public int MaxSlime;

    int value;
    int page;

    Text gold_Text;
    Text slimecount_Text;
    public Text error_Text;

    public Sprite showsp;
    public Sprite hidesp;

    public bool isLive;
    public bool isClick;

    GameObject option_Panel;
    public GameObject error_Panel;

    Image plant_Img;
    Animator _ani;

    GameManager _gm;
    Slime _slime;

    #region 슬라임변경변수
    Image slimeimg;
    Text slimename;
    Text slimegold;
    #endregion

    private void Awake()
    {
        gold_Text = GameObject.Find("Gold/Text").GetComponent<Text>();
        slimecount_Text = GameObject.Find("SlimeCount/Text").GetComponent<Text>();
        error_Text = GameObject.Find("Error Panel/Text").GetComponent<Text>();

        plant_Img = GameObject.Find("LeftBtn/Plant Button").GetComponent<Image>();

        option_Panel = GameObject.Find("Option Panel");
        error_Panel = GameObject.Find("Error Panel");
        _ani = GameObject.Find("Slime Panel").GetComponent<Animator>();

        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _slime = GameObject.Find("GameManager").GetComponent<Create_Slime>()._slime;

        slimeimg = GameObject.Find("Slime Panel/Unlock Group/Image").GetComponent<Image>();
        slimename = GameObject.Find("Slime Panel/Unlock Group/Name Text").GetComponent<Text>();
        slimegold = GameObject.Find("Slime Panel/Unlock Group/Button/Text").GetComponent<Text>();
        
        isLive = true;
    }

    private void Start()
    {
        SlimeChage();
        option_Panel.SetActive(false);
        error_Panel.SetActive(false);
        MaxSlime = 10;
    }

    private void Update()
    {
        OptionCheck();
    }

    private void LateUpdate()
    {
        float num = Mathf.SmoothStep(value, Gold, 0.5f);
        
        gold_Text.text = String.Format("{0:n0}", num);
        value = (int)num;

        SlimeCount = _slime.orignal_s + _slime.poision_s + _slime.sticky_s + _slime.blood_s + _slime.acid_s;
        slimecount_Text.text = String.Format("{0} / {1}", SlimeCount, MaxSlime);
    }

    public void MaxSlimeAdd()
    {
        if (Gold < 500)
        {
            error_Panel.SetActive(true);
            error_Text.text = "골드가 부족해요...";
            isClick = true;
            isLive = false;
        } 
        else
        {
            Gold -= 500;
            MaxSlime += 5;
        }
    }

    public void ShowPanel()
    {
        if (isClick)
        {
            _ani.SetTrigger("doHide");
            plant_Img.sprite = hidesp;
            isClick = false;
            isLive = true;
        }
        else
        {
            _ani.SetTrigger("doShow");
            plant_Img.sprite = showsp;
            isClick = true;
            isLive = false;
        }
    }

    public void OptionCheck()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isLive)
            {
                option_Panel.SetActive(true);
                Time.timeScale = 0;
                isClick = true;
                isLive = false;
            }
            else
            {
                if(option_Panel.activeSelf == true)
                {
                    option_Panel.SetActive(false);
                    Time.timeScale = 1;
                    isClick = false;
                    isLive = true;
                }
                else
                {
                    _ani.SetTrigger("doHide");
                    plant_Img.sprite = hidesp;
                    isClick = false;
                    isLive = true;
                }
            }
        }
    }

    public void ErrorPanel()
    {
        error_Panel.SetActive(false);
        isClick = false;
        isLive = true;
    }

    public void PageUp()
    {
        if (page == 5)
            return;
        else
        {
            page += 1;
            SlimeChage();
        }
    }

    public void PageDown()
    {
        if (page == 0)
            return;
        else
        {
            page -= 1;
            SlimeChage();
        }
    }

    void SlimeChage()
    {
        slimeimg.sprite = _gm.SlimeSpriteList[page];
        slimename.text = _gm.SlimeNameList[page];
        slimegold.text = String.Format("{0:n0}", _gm.SlimeGoldList[page]);
    }

    public void AddGold()
    {
        Gold += 500;
    }
}