using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 기본데이터
    public int SlimeCount;
    public int MaxSlime;

    int G_value;
    int J_value;

    public bool isLive;
    public bool isClick;

    bool isSlimeCheck;
    bool isPlantCheck;
    bool isOption;
    bool isError;
    #endregion

    #region ui
    Text gold_Text;
    Text jelatine_Text;
    Text slimecount_Text;
    Text error_Text;

    public Sprite slime_showSp;
    public Sprite slime_hideSp;
    public Sprite plant_showSp;
    public Sprite plant_hideSp;

    Image slime_Img;
    Image plant_Img;
    Animator _ani_Plant;
    Animator _ani_Slime;
    #endregion

    #region GameObject
    GameObject option_Panel;
    GameObject error_Panel;
    public GameObject dm;

    GameManager _Gm;
    #endregion

    private void Awake()
    {
        gold_Text = GameObject.Find("Gold/Text").GetComponent<Text>();
        jelatine_Text = GameObject.Find("Jelatine/Text").GetComponent<Text>();
        slimecount_Text = GameObject.Find("SlimeCount/Text").GetComponent<Text>();
        error_Text = GameObject.Find("Canvas").transform.Find("Error Panel/Text").GetComponent<Text>();

        slime_Img = GameObject.Find("LeftBtn/Slime Button").GetComponent<Image>();
        plant_Img = GameObject.Find("LeftBtn/Plant Button").GetComponent<Image>();

        option_Panel = GameObject.Find("Canvas").transform.Find("Option Panel").gameObject;
        error_Panel = GameObject.Find("Canvas").transform.Find("Error Panel").gameObject;

        _ani_Plant = GameObject.Find("Plant Panel").GetComponent<Animator>();
        _ani_Slime = GameObject.Find("SlimeCreate Panel").GetComponent<Animator>();

        _Gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        isLive = true;
    }

    private void Start()
    {
        
        MaxSlime = 10;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isSlimeCheck) SlimeShowPanel();
            else if (isPlantCheck) PlantShowPanel();
            else OptionShowPanel();
        }
    }

    private void LateUpdate()
    {
        float gold_num = Mathf.SmoothStep(G_value, _Gm.gold, 0.5f);
        float jelatine_num = Mathf.SmoothStep(J_value, _Gm.jelatin, 0.5f);
        
        gold_Text.text = String.Format("{0:n0}", gold_num);
        jelatine_Text.text = String.Format("{0:n0}", jelatine_num);

        G_value = (int)gold_num;
        J_value = (int)jelatine_num;

        slimecount_Text.text = String.Format("{0} / {1}", SlimeCount, MaxSlime);
    }

    public void MaxSlimeAdd()
    {
        if (_Gm.gold < 500)
        {
            ErrorPanel("골드가 부족해요...");
            return;
        }

        _Gm.gold -= 500;
        MaxSlime += 5;
    }

    public void SlimeShowPanel()
    {
        if (isPlantCheck)
        {
            _ani_Plant.SetTrigger("doHide");
            plant_Img.sprite = plant_hideSp;
            isPlantCheck = false;
            isLive = true;
        }

        if(isSlimeCheck)
        {
            _ani_Slime.SetTrigger("doHide");
            slime_Img.sprite = slime_hideSp;
        }
        else
        {
            _ani_Slime.SetTrigger("doShow");
            slime_Img.sprite = slime_showSp;
        }

        isSlimeCheck = !isSlimeCheck;
        isLive = !isLive;
    }

    public void PlantShowPanel()
    {
        if (isSlimeCheck)
        {
            _ani_Slime.SetTrigger("doHide");
            slime_Img.sprite = slime_hideSp;
            isSlimeCheck = false;
            isLive = true;
        }

        if (isPlantCheck)
        {
            _ani_Plant.SetTrigger("doHide");
            plant_Img.sprite = plant_hideSp;
        }
        else
        {
            _ani_Plant.SetTrigger("doShow");
            plant_Img.sprite = plant_showSp;
        }

        isPlantCheck = !isPlantCheck;
        isLive = !isLive;
    }

    public void OptionShowPanel()
    {
        isOption = !isOption;
        isLive = !isLive;

        option_Panel.SetActive(isOption);
        Time.timeScale = isOption == true ? 0 : 1;
    }

    public void ErrorPanel(String msg)
    {
        isError = !isError;
        isLive = !isLive;

        error_Panel.SetActive(isError);
        error_Text.text = msg;
    }

    public void GoShop()
    {
        DontDestroyOnLoad(_Gm);
        SceneManager.LoadScene("ShopScene");
    }
}