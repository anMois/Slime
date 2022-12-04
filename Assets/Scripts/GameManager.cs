using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;
    public int jelatin;
    public int slimeCount;
    public int clicklv;
    public int doblgdlv;
    public int doblgdcount;
    public bool isClear;
    public List<SlimeAutoMove> slime_list = new List<SlimeAutoMove>();
    public List<Slime> slime_data_list = new List<Slime>();
    public bool[] slime_unlock_list;

    public Sprite[] SlimeSpriteList;
    public string[] SlimeNameList;
    public int[] SlimeCreateGoldList;
    public int[] JelatineList;
    public int[] DobGoldList;
    public int[] ClickList;
    public float[] SlimeExpList;

    public Vector3[] PointList;

    public RuntimeAnimatorController[] LevelAc;

    public GameObject lockgroup;
    public UIManager _Uim;
    public GameObject prefab;
    public GameObject ClearImg;
    int page;
    int maxGold;
    int maxJelatin;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        lockgroup = GameObject.Find("Canvas/SlimeCreate Panel/Lock Group").transform.Find("Lock Group").gameObject;
        page = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().page;

        slime_unlock_list = new bool[6];
        maxGold = maxJelatin = 999999999;
    }

    private void Start()
    {
        Invoke("LoadData", 0.1f);
    }

    void LoadData()
    {
        lockgroup.gameObject.SetActive(!slime_unlock_list[page]);

        for (int i = 0; i < slime_data_list.Count; ++i)
        {
            GameObject obj = Instantiate(prefab, slime_data_list[i].pos, Quaternion.identity);
            SlimeAutoMove slime = obj.GetComponent<SlimeAutoMove>();
            slime.id = slime_data_list[i].id;
            slime.level = slime_data_list[i].level;
            slime.exp = slime_data_list[i].exp;
            slime.slime_sprite.sprite = SlimeSpriteList[slime.id];
            slime.ani.runtimeAnimatorController = LevelAc[slime.level - 1];
            obj.name = "Slime " + slime.id;

            slime_list.Add(slime);
        }

        slimeCount = slime_list.Count;
        if (isClear) ClearImg.SetActive(isClear);
        _Uim.ClickContect();
        _Uim.DoubleGdContect();
    }

    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }

    public void GetJelatin(int id, int level)
    {
        jelatin += (id + 1) * level * clicklv;

        if (jelatin > maxJelatin)
        {
            jelatin = maxJelatin; 
            SoundManager.instance.PlayerSound("Fail");
        }
    }

    public void GetGold(int id, int level, SlimeAutoMove slime)
    {
        if (_Uim.isDoblgd && doblgdcount != 0)
        {
            gold += SlimeCreateGoldList[id] * level * 2;
            doblgdcount--;
        }
        else gold += SlimeCreateGoldList[id] * level;
        SoundManager.instance.PlayerSound("Sell");
        slimeCount--;
        slime_list.Remove(slime);
        

        if (gold > maxGold)
        {
            gold = maxGold;
            SoundManager.instance.PlayerSound("Fail");
        }
    }

    public bool BuySlime(int num)
    {
        if (gold >= SlimeCreateGoldList[num])
        {
            gold -= SlimeCreateGoldList[num];
            return true;
        }
        else
            return false;
    }

    public void ClickCheck()
    {
        if (gold < ClickList[clicklv]) return;

        gold -= ClickList[clicklv++];
    }

    public void DoubleGdCheck()
    {
        if (gold < DobGoldList[doblgdlv]) return;

        gold -= DobGoldList[doblgdlv++];

        doblgdcount = Random.Range(2, 10);
    }

    public void ClearCheack()
    {
        List<bool> clearlist = new List<bool>();
        for (int i = 0; i < slime_unlock_list.Length; i++)
        {
            if (slime_unlock_list[i]) clearlist.Add(slime_unlock_list[i]);
            
            if(clearlist.Count == slime_unlock_list.Length)
            {
                isClear = !isClear;
                ClearImg.SetActive(isClear);
            }
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.JsonSave();
    }
}