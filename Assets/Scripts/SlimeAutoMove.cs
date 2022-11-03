using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAutoMove : MonoBehaviour
{
    public int id;      //슬라임 이미지순서
    public int level;   //레벨 => 애니메이션 컨트롤러

    float speedX;   //움질일떄 x값
    float speedY;   //움질일떄 y값
    float walk_Num; //움직이는 시간
    float idle_Num; //기본상태에서 대기하는시간
    float pick_time;//마우스 버튼을 누른시간

    bool iswalk = false;
    bool isidle = false;

    public GameObject TopLeft;
    public GameObject BottomRight;
    public GameObject Gm;
    public UIManager Uim;

    Transform tl; //topleft 위치
    Transform br; //bottomright 위치
    Vector3 point; //돌아갈 위치

    Animator ani;
    public SpriteRenderer slime_sprite;

    private void Awake()
    {
        TopLeft = GameObject.Find("Border Group/TopLeft");
        BottomRight = GameObject.Find("Border Group/BottomRight");
        Gm = GameObject.Find("GameManager");
        Uim = GameObject.Find("Canvas").GetComponent<UIManager>();

        ani = GetComponent<Animator>();
        slime_sprite = GetComponent<SpriteRenderer>();

        tl = TopLeft.transform;
        br = BottomRight.transform;
    }

    private void Start()
    {
        idle_Num = Random.Range(3.0f, 5.0f);

        if (ani.runtimeAnimatorController != Gm.GetComponent<GameManager>().LevelAc[0])
            ani.runtimeAnimatorController = Gm.GetComponent<GameManager>().LevelAc[0];
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
        if (!Uim.isLive) return;

        iswalk = false;
        ani.SetBool("isWalk", false);
        ani.SetTrigger("doTouch");

        if (Uim.Jelatine < 9999999)
            Uim.Jelatine += (id + 1) * level;
    }

    private void OnMouseDrag()
    {
        if (!Uim.isLive) return;

        pick_time += Time.deltaTime;

        if (pick_time < 0.5f) return;

        iswalk = false;
        ani.SetBool("doWalk", false);
        ani.SetTrigger("doTouch");

        Vector3 mouse_pos = Input.mousePosition;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.y));

        transform.position = point;
    }

    private void OnMouseUp()
    {
        if (!Uim.isLive) return;

        pick_time = 0;

        if ((transform.position.x < tl.position.x || transform.position.x > br.position.x) ||
            (transform.position.y > tl.position.y || transform.position.y < br.position.y))
        {
            int n = Random.Range(0, 3);
            transform.position = Gm.GetComponent<GameManager>().PointList[n];
        }
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
            point = Gm.GetComponent<GameManager>().PointList[n];
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