using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Create_Slime : MonoBehaviour
{
    public GameObject obj;
    //public GameObject[] obj;
    public Slime _slime;

    public bool[] isCrtCheck;

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
            _uiManager.ShowError("슬라임 과부화 상태!");
        }
        else
        {
            obj.tag = "Orignal";
            _slime.orignal_s++;
            obj.GetComponent<SpriteRenderer>().sprite = _Gm.SlimeSpriteList[0];
            PointCreate();
        }

        #region create
        /*
        int rannum = Random.Range(0, 100);

        if (rannum <= 40)
        {
            //기본슬라임
            Instantiate(obj[0], new Vector3(-4.5f, 0, 0), Quaternion.identity);
            slim.orignal_s++;
        }
        else if (rannum > 40 && rannum <= 60)
        {
            //스티키슬라임
            Instantiate(obj[1], new Vector3(-3f, 0, 0), Quaternion.identity);
            slim.sticky_s++;
        }
        else if (rannum > 40 && rannum <= 90)
        {
            if (rannum > 75)
            {
                //애시드슬라임
                Instantiate(obj[3], new Vector3(1.5f, 0, 0), Quaternion.identity);
                slim.acid_s++;
            }
            else
            {
                //포이든슬라임
                Instantiate(obj[2], new Vector3(-1.5f, 0, 0), Quaternion.identity);
                slim.poision_s++;
            }
        }
        else if (rannum > 90 && rannum <= 100)
        {
            if (rannum > 95)
            {
                //블러드슬라임
                Instantiate(obj[5], new Vector3(4.5f, 0, 0), Quaternion.identity);
                slim.blood_s++;
            }
            else
            {
                //클리너슬라임
                Instantiate(obj[4], new Vector3(3f, 0, 0), Quaternion.identity);
                slim.cleaner_s++;
            }
        }*/
        #endregion
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
