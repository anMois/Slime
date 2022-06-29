using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeType
{
    ORIGNAL,
    STICKY,
    ACID,
    POISION,
    CLEANER,
    BLOOD
}

[System.Serializable]
public class Slime
{
    public int orignal_s;
    public int sticky_s;
    public int acid_s;
    public int poision_s;
    public int cleaner_s;
    public int blood_s;
}


