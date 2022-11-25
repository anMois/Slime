using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Create_Slime : MonoBehaviour
{
    public GameObject obj;

    public bool[] isCrtCheck;

    int page;

    GameManager _Gm;
    UIManager _uiM;

    private void Awake()
    {
        _Gm = GetComponent<GameManager>();
        _uiM = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Start()
    {
        int num = _Gm.PointList.Length;
        isCrtCheck = new bool[num];
    }

    public void Create()
    {
        if (_uiM.MaxSlime <= _uiM.SlimeCount)
        {
            _uiM.ErrorPanel("슬라임 과부화 상태!");
            return;
        }
        else if ((_Gm.gold < _Gm.SlimeCreateGoldList[page]) &&
            (_Gm.gold - _Gm.SlimeCreateGoldList[page] < 0))
        {
            _uiM.ErrorPanel("해당 골드 부족!");
            return;
        }

        int num = Random.Range(0, 3);
        page = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().page;
        
        SlimeAutoMove _slm = obj.GetComponent<SlimeAutoMove>();
        
        obj.name = "Slime" + page;
        _slm.id = page;
        _slm.GetComponent<SpriteRenderer>().sprite = _Gm.SlimeSpriteList[page];
        Slime _slime = new Slime(_Gm.PointList[num], _slm.id, _slm.level);

        PointCreate(num);
        _Gm.gold -= _Gm.SlimeCreateGoldList[page];
        _Gm.slime_data_list.Add(_slime);
        _Gm.slime_list.Add(_slm);
        _uiM.SlimeCount++;
    }

    void PointCreate(int num)
    {
        if(isCrtCheck[num] == false)
        {
            Instantiate(obj, _Gm.PointList[num], Quaternion.identity);
            for (int i = 0; i < isCrtCheck.Length; i++)
            {
                if(isCrtCheck[i] == true)
                {
                    isCrtCheck[i] = false;
                }
            }
            isCrtCheck[num] = true;

        }
        else
        {
            List<int> CrtSlIndex = new List<int>();

            for (int i = 0; i < isCrtCheck.Length; i++)
            {
                if (isCrtCheck[i] == false)
                {
                    CrtSlIndex.Add(i);
                    if (CrtSlIndex.Count == (isCrtCheck.Length - 1))
                    {
                        int[] CrtSlArr = CrtSlIndex.ToArray();
                        int n = Random.Range(0, CrtSlIndex.Count);
                        Instantiate(obj, _Gm.PointList[CrtSlArr[n]], Quaternion.identity);
                        for (int j = 0; j < isCrtCheck.Length; j++)
                        {
                            if (isCrtCheck[j] == true)
                            {
                                isCrtCheck[j] = false;
                            }
                        }
                        isCrtCheck[CrtSlArr[n]] = true;
                    }
                }
            }
        }
    }
}
