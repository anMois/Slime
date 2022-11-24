using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;
    public int jelatin;
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
    GameObject lockgroup;
    GameObject prefab;
    int page;

    private void Awake()
    {
        instance = this;

        lockgroup = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().lockobj;
        page = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().page;
        prefab = GetComponent<Create_Slime>().obj;
        dm = GameObject.Find("DataManager").gameObject;
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
            SlimeAutoMove jelly = obj.GetComponent<SlimeAutoMove>();
            jelly.id = slime_data_list[i].id;
            jelly.level = slime_data_list[i].level;
            jelly.slime_sprite.sprite = SlimeSpriteList[jelly.id];
            jelly.ani.runtimeAnimatorController = LevelAc[jelly.level - 1];
            obj.name = "Jelly " + jelly.id;

            slime_list.Add(jelly);
        }
    }

    public void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }


    private void OnApplicationQuit()
    {
        dm.GetComponent<DataManager>().JsonSave();
    }
}