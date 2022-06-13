﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;

    private float getY;

    private void Start()
    {
        getY = transform.position.y;
    }

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0));

        if (transform.position.x > 7)
        {
            transform.localPosition = new Vector3(-7.0f, getY, 0);
        }
    }
}