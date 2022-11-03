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

    UIManager _uiManager;
    GameManager _Gm;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _Gm = GetComponent<GameManager>();
    }

    private void Start()
    {
        int num = _Gm.PointList.Length;
        isCrtCheck = new bool[num];
    }

    public void Create()
    {
        if (_uiManager.MaxSlime <= _uiManager.SlimeCount)
        {
            _uiManager.ErrorPanel("슬라임 과부화 상태!");
            return;
        }
        else if ((_uiManager.Gold < _Gm.SlimeCreateGoldList[page]) &&
            (_uiManager.Gold - _Gm.SlimeCreateGoldList[page] < 0))
        {
            _uiManager.ErrorPanel("해당 골드 부족!");
            return;
        }

        page = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().page;

        SlimeAutoMove _slm = obj.GetComponent<SlimeAutoMove>();
        obj.name = "Slime" + page;
        _slm.id = page;
        _slm.GetComponent<SpriteRenderer>().sprite = _Gm.SlimeSpriteList[page];

        PointCreate();
        _uiManager.Gold -= _Gm.SlimeCreateGoldList[page];
        _uiManager.SlimeCount++;
    }

    void PointCreate()
    {
        int num = Random.Range(0, 3);

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
