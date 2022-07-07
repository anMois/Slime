using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] SlimeSpriteList;
    public string[] SlimeNameList;
    public int[] SlimeGoldList;

    public Vector3[] PointList;

    public RuntimeAnimatorController[] LevelAc;

    public void Change(Animator anim, int level)
    {
        anim.runtimeAnimatorController = LevelAc[level - 1];
    }
}