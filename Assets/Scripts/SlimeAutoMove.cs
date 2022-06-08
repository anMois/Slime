using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAutoMove : MonoBehaviour
{
    public float num;

    float speedX;
    float speedY;
    float num1 = 2.5f;

    bool isidle = true;
    bool iswalk = false;

    Animator isMove;
    SpriteRenderer slime_sprite;

    private void Awake()
    {
        isMove = transform.GetComponent<Animator>();
        slime_sprite = transform.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        NewNum();
    }

    private void Update()
    {
        if (isidle == true && iswalk == false)
        {
            num -= Time.deltaTime;
            if (num < 0.0f)
            {
                isMoveCheck();
            }
        }
        else
        {
            num1 -= Time.deltaTime;
            if (num1 < 0.0f)
            {
                isMoveCheck();
            }
        }
        
    }


    float NewNum()
    {
        num = Random.Range(3.0f, 5.0f);
        Debug.Log(num);
        return num;
    }

    void isMoveCheck()
    {
        if (isMove.GetBool("isWalk"))
        {
            isMove.SetBool("isWalk", false);
            isidle = true;
            iswalk = false;
            num = NewNum();
        }
        else
        {
            isMove.SetBool("isWalk", true);
            transform.Translate(RandomSpeedX(), RandomSpeedY(), RandomSpeedY());
            isidle = false;
            iswalk = true;
        }
        
    }

    float RandomSpeedX()
    {
        speedX = Random.Range(-0.8f, 0.8f);

        if (speedX < 0)
            slime_sprite.flipX = true;
        else
            slime_sprite.flipX = false;

        return speedX;
    }
    float RandomSpeedY()
    {
        speedY = Random.Range(-0.8f, 0.8f);
        return speedY;
    }
}
