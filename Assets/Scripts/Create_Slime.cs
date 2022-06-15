using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create_Slime : MonoBehaviour
{
    public GameObject obj;
    //public GameObject[] obj;
    public Slime _slime;

    public UIManager _uiManager;
    GameManager _Gm;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _Gm = GetComponent<GameManager>();
    }

    public void Create()
    {
        Instantiate(obj, _Gm.PointList[Random.Range(0, 3)], Quaternion.identity);
        _slime.orignal_s++;
        /*
        int rannum = Random.Range(0, 100);

        if(rannum <= 40)
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
            if(rannum > 95)
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
        }

        Debug.Log(rannum);
        */
    }

}
