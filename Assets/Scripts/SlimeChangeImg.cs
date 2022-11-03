using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeChangeImg : MonoBehaviour
{
    public bool[] UnlockList;

    public int page;

    #region ui
    public Image slimeimg;  //슬라임 이미지
    public Image locksimg;  //잠겨있는 슬라임 이미지

    public Text slimename;  //슬라임 이름
    Text slimegold;         //슬라임 구매 골드
    public Text pagetext;   //해당 페이지
    Text conditiontext;     //슬라임 잠금 조건 텍스트
    #endregion

    GameObject lockobj;

    GameManager _gm;
    UIManager _ui;

    private void Awake()
    {
        slimeimg = GameObject.Find("Unlock Group/Image").GetComponent<Image>();
        slimename = GameObject.Find("Unlock Group/Name Text").GetComponent<Text>();
        slimegold = GameObject.Find("Unlock Group/Button/Text").GetComponent<Text>();
        pagetext = GameObject.Find("Page Text").GetComponent<Text>();

        lockobj = GameObject.Find("Lock Group").transform.Find("Lock Group").gameObject;
        locksimg = GameObject.Find("Lock Group").transform.Find("Lock Group/Image").GetComponent<Image>();
        
        conditiontext = GameObject.Find("Lock Group").transform.Find("Lock Group/Button/Text").GetComponent<Text>();

        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Start()
    {
        SlimeChage();
    }

    public void PageUp()
    {
        if (page == 5)  return;

        ++page;
        SlimeChage();
        pagetext.text = String.Format("#{0:00}", page + 1);
    }

    public void PageDown()
    {
        if (page == 0)  return;

        --page;
        SlimeChage();
        pagetext.text = String.Format("#{0:00}", page + 1);
    }

    public void SlimeChage()
    {
        if (!UnlockList[page])
        {
            lockobj.SetActive(true);
            locksimg.sprite = _gm.SlimeSpriteList[page];
            locksimg.SetNativeSize();
            conditiontext.text = String.Format("{0:n0}", _gm.JelatineList[page]);
        }
        else
        {
            lockobj.SetActive(false);
            slimeimg.sprite = _gm.SlimeSpriteList[page];
            slimeimg.SetNativeSize();
            slimename.text = _gm.SlimeNameList[page];
            slimegold.text = String.Format("{0:n0}", _gm.SlimeCreateGoldList[page]);
        }
    }

    public void UnlockBtn()
    {
        if (_ui.Jelatine < _gm.JelatineList[page])
        {
            _ui.ErrorPanel("해당 젤라틴 부족!");
            return;
        }

        UnlockList[page] = true;
        SlimeChage();

        _ui.Jelatine -= _gm.JelatineList[page];
    }
}
