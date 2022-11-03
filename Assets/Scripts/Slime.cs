using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slime
{
    public int id;
    public int level;

    public Slime(int _id, int _level)
    {
        this.id = _id;
        this.level = _level;
    }
}


