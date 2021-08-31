using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Slime : MonoBehaviour
{
    public GameObject[] obj;
    

    private void Start()
    {
        Instantiate(obj[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void Create()
    {
        int rannum = Random.Range(0, 100);

        if(rannum <= 40)
        {
            //기본슬라임
            Instantiate(obj[0], new Vector3(-4.5f, 0, 0), Quaternion.identity);
        }
        else if (rannum > 40 && rannum <= 60)
        {
            //스티키슬라임
            Instantiate(obj[1], new Vector3(-3f, 0, 0), Quaternion.identity);
        }
        else if (rannum > 40 && rannum <= 90)
        {
            if(rannum > 75)
                //애시드슬라임
                Instantiate(obj[3], new Vector3(1.5f, 0, 0), Quaternion.identity);
            else
                //포이든슬라임
                Instantiate(obj[2], new Vector3(-1.5f, 0, 0), Quaternion.identity);
            
        } 
        else if (rannum > 90 && rannum <= 100)
        {
            if(rannum > 95)
                //블러드슬라임
                Instantiate(obj[5], new Vector3(4.5f, 0, 0), Quaternion.identity);
            else
                //클리너슬라임
                Instantiate(obj[4], new Vector3(3f, 0, 0), Quaternion.identity);
        }

        Debug.Log(rannum);
        //Instantiate(obj[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void Update()
    {
      
    }
}
