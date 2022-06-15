using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAutoMove : MonoBehaviour
{
    public float idle_Num;

    float speedX;
    float speedY;
    float walk_Num;

    bool isidle = true;
    bool iswalk = false;

    public GameObject TopLeft;
    public GameObject BottomRight;
    public GameObject Manager;

    Transform tl; //topleft 위치
    Transform br; //bottomright 위치
    Vector3 point; //돌아갈 위치

    Animator isMove;
    SpriteRenderer slime_sprite;

    private void Awake()
    {
        TopLeft = GameObject.Find("Border Group/TopLeft");
        BottomRight = GameObject.Find("Border Group/BottomRight");
        Manager = GameObject.Find("GameManager");

        isMove = transform.GetComponent<Animator>();
        slime_sprite = transform.GetComponent<SpriteRenderer>();

        tl = TopLeft.transform;
        br = BottomRight.transform;
    }

    private void Start()
    {
        idle_Num = Random.Range(3.0f, 5.0f);
    }

    private void Update()
    {
        //idle 상태
        if (isidle == true && iswalk == false)
        {
            idle_Num -= Time.deltaTime;
            if (idle_Num < 0.0f)
            {
                isWalkCheck();
            }
        }
        //walk 상태
        else
        {
            transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
            if (SelfPositionCheck())
            {
                transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
            }
            walk_Num -= Time.deltaTime;
            if (walk_Num < 0.0f)
            {
                isWalkCheck();
            }
        }
    }

    void isWalkCheck()
    {
        //idle 전환
        if (isMove.GetBool("isWalk"))
        {
            isMove.SetBool("isWalk", false);
            idle_Num = Random.Range(3.0f, 5.0f);
            isidle = true;  iswalk = false;
        }
        //walk 전환
        else
        {
            walk_Num = 2.5f;
            isMove.SetBool("isWalk", true);
            speedX = Random.Range(-0.8f, 0.8f);
            speedY = Random.Range(-0.8f, 0.8f);

            if (speedX < 0)
                slime_sprite.flipX = true;
            else
                slime_sprite.flipX = false;

            isidle = false; iswalk = true;
        }
    }

    #region 자기위치 확인
    bool SelfPositionCheck()
    {
        if ((transform.position.x < tl.position.x || transform.position.x > br.position.x) ||
            (transform.position.y > tl.position.y || transform.position.y < br.position.y))
        {
            int n = Random.Range(0, 3); //지정된 위치 정하기
            point = Manager.GetComponent<GameManager>().PointList[n];
            Vector3 v = point - transform.position;
            v.Normalize();
            v = v * 0.3f;
            speedX = v.x;
            speedY = v.y;

            if (speedX < 0)
                slime_sprite.flipX = true;
            else
                slime_sprite.flipX = false;

            return true;
        }
        else
            return false;
    }
    #endregion
}