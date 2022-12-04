using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Create_Slime : MonoBehaviour
{
    public GameObject slime_prefap;

    public bool[] isCrtCheck;

    int page;

    public GameManager _Gm;
    UIManager _Uim;

    private void Awake()
    {
        _Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _Uim = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Start()
    {
        int num = _Gm.PointList.Length;
        isCrtCheck = new bool[num];
    }

    public void Create()
    {
        if (_Uim.MaxSlime <= _Gm.slimeCount)
        {
            _Uim.ErrorPanel("슬라임 과부화 상태!");
            return;
        }

        int num = Random.Range(0, 3);
        page = GameObject.Find("Canvas/SlimeCreate Panel").GetComponent<SlimeChangeImg>().page;

        if (GameManager.instance.BuySlime(page))
        {
            GameObject obj = PointCreate(num);
            SoundManager.instance.PlayerSound("Buy");
            SlimeAutoMove _slm = obj.GetComponent<SlimeAutoMove>();
            obj.name = "Slime " + page;
            _slm.id = page;
            _slm.GetComponent<SpriteRenderer>().sprite = _Gm.SlimeSpriteList[page];

            Slime _slime = new Slime(_Gm.PointList[num], _slm.id, _slm.level, _slm.exp);

            _Gm.slime_data_list.Add(_slime);
            _Gm.slime_list.Add(_slm);
            _Gm.slimeCount++;
        }
        else
        {
            _Uim.ErrorPanel("해당 골드 부족!");
            return;
        }
    }

    GameObject PointCreate(int num)
    {
        if(isCrtCheck[num] == false)
        {
            GameObject obj = Instantiate(slime_prefap, _Gm.PointList[num], Quaternion.identity);

            for (int i = 0; i < isCrtCheck.Length; i++)
            {
                if(isCrtCheck[i] == true)
                {
                    isCrtCheck[i] = false;
                }
            }
            isCrtCheck[num] = true;

            return obj;
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

                        GameObject obj = Instantiate(slime_prefap, _Gm.PointList[CrtSlArr[n]], Quaternion.identity);

                        for (int j = 0; j < isCrtCheck.Length; j++)
                        {
                            if (isCrtCheck[j] == true)
                            {
                                isCrtCheck[j] = false;
                            }
                        }
                        isCrtCheck[CrtSlArr[n]] = true;

                        return obj;
                    }
                }
            }

            return null;
        }
    }
}
