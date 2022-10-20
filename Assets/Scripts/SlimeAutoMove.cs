using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAutoMove : MonoBehaviour
{
    public float num;
    float speedX;
    float speedY;
    float walk_Num;
    float idle_Num;

    bool iswalk = false;
    bool isidle = false;

    public GameObject TopLeft;
    public GameObject BottomRight;
    public GameObject Manager;

    Transform tl; //topleft 위치
    Transform br; //bottomright 위치
    Vector3 point; //돌아갈 위치

    Animator ani;
    SpriteRenderer slime_sprite;

    private void Awake()
    {
        TopLeft = GameObject.Find("Border Group/TopLeft");
        BottomRight = GameObject.Find("Border Group/BottomRight");
        Manager = GameObject.Find("GameManager");

        ani = transform.GetComponent<Animator>();
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

    }

    private void FixedUpdate()
    {
        if (!isidle)
            StartCoroutine(IdleCheck()); //코루틴사용
        if (iswalk)
            SlimeMove();
    }

    void OnMouseDown()
    {
        ani.SetBool("isWalk", false);
        iswalk = false;
        
        Debug.Log("Click");
        ani.SetTrigger("doTouch");
    } 

    void SlimeMove()
    {
        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
        if (SelfPositionCheck())
        {
            transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
        }
    }

    #region 벽에 부디쳤을때 자기위치 확인
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

            slime_sprite.flipX = (speedX < 0) ? true : false;

            return true;
        }
        else
            return false;
    }
    #endregion

    IEnumerator IdleCheck()
    {
        speedX = Random.Range(-0.8f, 0.8f);
        speedY = Random.Range(-0.8f, 0.8f);

        isidle = true;

        yield return new WaitForSeconds(idle_Num); //기본상태에서 움직이는 상태로

        walk_Num = 2.5f;
        ani.SetBool("isWalk", true);
        slime_sprite.flipX = (speedX < 0) ? true : false;

        iswalk = true;

        yield return new WaitForSeconds(walk_Num); //움직이는 상태에서 기본상태로

        ani.SetBool("isWalk", false);
        idle_Num = Random.Range(3.0f, 5.0f);
        iswalk = false;

        isidle = false;
    }
}