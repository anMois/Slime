using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slime
{
    public int id;
    public int level;
    public float exp;
    public Vector3 pos;

    public Slime(Vector3 _pos, int _id, int _level, float _exp)
    {
        this.pos = _pos;
        this.id = _id;
        this.level = _level;
        this.exp = _exp;
    }
}


