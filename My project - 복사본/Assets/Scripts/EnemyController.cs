using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 6.0f; // 기본 이동 속도
    private float orignalSpeed; // 원래 속도
    private Coroutine slowSpeed; // 슬로우 효과를 위한 코루틴

    private Transform target; // 다음 타겟
    private int wavepointIndex = 0;

    public float enemyHp; // 적의 기본 체력

    public GameMgr gameMgr;

    public bool isDead = false;
   

    
    void Start()
    {
        target = Waypoints.points[0];
        orignalSpeed = speed; //원래 속도 저장
        gameMgr=GameObject.Find("GameMgr").GetComponent<GameMgr>();

        enemyHp = gameMgr.curEnemyHp;
    }

    public void TakeDamage(float damege)
    {
        if (isDead) return;

        enemyHp -= damege; // 데미지 만큼 피해
        if(enemyHp <= 0)
        {
            isDead = true;    
            GetComponentInChildren<HpBar>().DestroyHpBar();
            gameMgr.killCount++;
            gameMgr.killCount2++;
            gameMgr.killCount3++;
            Destroy(gameObject); // HP가 0이 되면 파괴
        }
    }

    public void Slow(float slowAmount)
    {
        if(slowSpeed != null)
        {
            StopCoroutine(slowSpeed); // 기존 슬로우 효과 중지
        }
        slowSpeed = StartCoroutine(SlowCoroutine(slowAmount)); //슬로우 효과 시작
    }

    private IEnumerator SlowCoroutine(float slowAmount)
    {
        float slowSpeed2 = orignalSpeed * (1- slowAmount);
        speed = Mathf.Max(slowSpeed2, 2.0f);
        Debug.Log("슬로우 적용: 현재 속도 = " + speed);
        yield return new WaitForSeconds(3f); // 3초 지속
        speed = orignalSpeed; // 원래 속도로 되돌림
        Debug.Log("슬로우 해제: 현재 속도 = " + speed); // 슬로우 해제 시 속도 출력
        slowSpeed = null;
    }

    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        

        if(Vector3.Distance(transform.position, target.position) <= 0.04f)
        {
            GetNextWaypoint();
        }

        
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            gameMgr.Hp--;
            GetComponentInChildren<HpBar>().DestroyHpBar();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
