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

    public GameObject lockobj;     //잠겨있는 슬라임 오브젝트

    GameManager _Gm;
    UIManager _uiM;

    private void Awake()
    {
        slimeimg = GameObject.Find("Unlock Group/Image").GetComponent<Image>();
        slimename = GameObject.Find("Unlock Group/Name Text").GetComponent<Text>();
        slimegold = GameObject.Find("Unlock Group/Button/Text").GetComponent<Text>();
        pagetext = GameObject.Find("Page Text").GetComponent<Text>();

        lockobj = GameObject.Find("Lock Group").transform.Find("Lock Group").gameObject;
        locksimg = GameObject.Find("Lock Group").transform.Find("Lock Group/Image").GetComponent<Image>();
        
        conditiontext = GameObject.Find("Lock Group").transform.Find("Lock Group/Button/Text").GetComponent<Text>();

        _Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiM = GameObject.Find("Canvas").GetComponent<UIManager>();

        for (int i = 0; i < UnlockList.Length; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
                UnlockList[i] = true;
        }
        lockobj.SetActive(!UnlockList[page]);
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
            locksimg.sprite = _Gm.SlimeSpriteList[page];
            locksimg.SetNativeSize();
            conditiontext.text = String.Format("{0:n0}", _Gm.JelatineList[page]);
        }
        else
        {
            lockobj.SetActive(false);
            slimeimg.sprite = _Gm.SlimeSpriteList[page];
            slimeimg.SetNativeSize();
            slimename.text = _Gm.SlimeNameList[page];
            slimegold.text = String.Format("{0:n0}", _Gm.SlimeCreateGoldList[page]);
        }
    }

    public void UnlockBtn()
    {
        if (_Gm.jelatin < _Gm.JelatineList[page])
        {
            _uiM.ErrorPanel("해당 젤라틴 부족!");
            return;
        }

        UnlockList[page] = true;
        SlimeChage();

        _Gm.jelatin -= _Gm.JelatineList[page];

        PlayerPrefs.SetInt(page.ToString(), 1);
    }
}
