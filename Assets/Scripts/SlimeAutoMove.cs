using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAutoMove : MonoBehaviour
{
    public float idle_Num;

    float speedX;
    float speedY;
    float walk_Num = 2.5f;

    bool isidle = true;
    bool iswalk = false;

    public GameObject TopLeft;
    public GameObject BottomRight;

    Transform tl;
    Transform br;

    Animator isMove;
    SpriteRenderer slime_sprite;

    private void Awake()
    {
        isMove = transform.GetComponent<Animator>();
        slime_sprite = transform.GetComponent<SpriteRenderer>();
        tl = TopLeft.transform;
        br = BottomRight.transform;
    }

    private void Start()
    {
        Newidle_Num();
    }

    private void Update()
    {
        //idle
        if (isidle == true && iswalk == false)
        {
            idle_Num -= Time.deltaTime;
            if (idle_Num < 0.0f)
            {
                isMoveCheck();
            }
        }
        //walk
        else
        {
            transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
            SelfPositionCheck();
            walk_Num -= Time.deltaTime;
            if (walk_Num < 0.0f)
            {
                isMoveCheck();
            }
        }
        
    }

    float Newidle_Num()
    {
        idle_Num = Random.Range(3.0f, 5.0f);
        return idle_Num;
    }

    void isMoveCheck()
    {
        //idle
        if (isMove.GetBool("isWalk"))
        {
            isMove.SetBool("isWalk", false);
            isidle = true;
            iswalk = false;
            idle_Num = Newidle_Num();
        }
        //walk
        else
        {
            
            walk_Num = 2.5f;
            isMove.SetBool("isWalk", true);
            speedX = RandomSpeedX();
            speedY = RandomSpeedY();
            isidle = false;
            iswalk = true;
        }
        
    }

    #region RandomSpeed
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
    #endregion

    #region 자기위치 확인
    void SelfPositionCheck()
    {
        if (transform.position.x < tl.position.x || transform.position.x > br.position.x)
        {
            Debug.Log("Outsite");
        }
        else if (transform.position.y > tl.position.y || transform.position.y < br.position.y)
        {
            Debug.Log("Outsite");
        }
    }
    #endregion
}
