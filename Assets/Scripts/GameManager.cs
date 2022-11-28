using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;
    public int jelatin;
    public int slimeCount;
    public List<SlimeAutoMove> slime_list;
    public List<Slime> slime_data_list;
    public bool[] slime_unlock_list;

    public Sprite[] SlimeSpriteList;
    public string[] SlimeNameList;
    public int[] SlimeCreateGoldList;
    public int[] JelatineList;

    public Vector3[] PointList;

    public RuntimeAnimatorController[] LevelAc;

    GameObject dm;
    public GameObject lockgroup;
    GameObject prefab;
    int page;

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
        prefab = GameObject.Find("Canvas").GetComponent<Create_Slime>().obj;
        dm = GameObject.Find("DataManager").gameObject;

        slime_unlock_list = new bool[6];
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
            slime.slime_sprite.sprite = SlimeSpriteList[slime.id];
            slime.ani.runtimeAnimatorController = LevelAc[slime.level - 1];
            obj.name = "Slime " + slime.id;

            slime_list.Add(slime);
        }
        slimeCount = slime_list.Count;
    }

    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }

    public void BuySlime(int num)
    {
        gold -= SlimeCreateGoldList[num];
    }

    private void OnApplicationQuit()
    {
        //dm.GetComponent<DataManager>().JsonSave();
    }
}