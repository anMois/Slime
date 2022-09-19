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
    Image slimeimg;
    Image locksimg;
    Image conditionimg;

    Text slimename;
    Text slimegold;
    Text pagetext;
    Text conditiontext;
    #endregion

    GameObject lockobj;

    GameManager _gm;
    Slime _slime;

    private void Awake()
    {
        slimeimg = GameObject.Find("Unlock Group/Image").GetComponent<Image>();
        slimename = GameObject.Find("Unlock Group/Name Text").GetComponent<Text>();
        slimegold = GameObject.Find("Unlock Group/Button/Text").GetComponent<Text>();
        pagetext = GameObject.Find("Page Text").GetComponent<Text>();

        lockobj = GameObject.Find("Lock Group").transform.Find("Lock Group").gameObject;
        locksimg = GameObject.Find("Lock Group").transform.Find("Lock Group/Image").GetComponent<Image>();
        conditionimg = GameObject.Find("Lock Group").transform.Find("Lock Group/Button/Image").GetComponent<Image>();
        conditiontext = GameObject.Find("Lock Group").transform.Find("Lock Group/Button/Text").GetComponent<Text>();

        _slime = GameObject.Find("GameManager").GetComponent<Create_Slime>()._slime;
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        SlimeChage();
    }

    private void FixedUpdate()
    {
        conditiontext.text = String.Format("{0:00} / 20", SlimeCount(page));
    }

    public void PageUp()
    {
        if (page == 5)
            return;
        else
        {
            page += 1;
            SlimeChage();
            pagetext.text = String.Format("#{0:00}", page + 1);
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
            pagetext.text = String.Format("#{0:00}", page + 1);
        }
    }

    public void SlimeChage()
    {
        if (!UnlockList[page])
        {
            lockobj.SetActive(true);
            locksimg.sprite = _gm.SlimeSpriteList[page];
            locksimg.SetNativeSize();
            conditionimg.sprite = _gm.SlimeSpriteList[page];
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

    int SlimeCount(int num)
    {
        switch (num)
        {
            case 0:
                return _slime.orignal_s;
            case 1:
                return _slime.sticky_s;
            case 2:
                return _slime.acid_s;
            case 3:
                return _slime.poision_s;
            case 4:
                return _slime.blood_s;
            case 5:
                return _slime.cleaner_s;
            default:
                return 0;
        }
    }

    public void UnlockBtn()
    {
        if (SlimeCount(page) == 20)
        {
            UnlockList[page] = true;
            lockobj.SetActive(false);
            SlimeChage();
        }
    }
}
