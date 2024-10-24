using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 6.0f; // �⺻ �̵� �ӵ�
    private float orignalSpeed; // ���� �ӵ�
    private Coroutine slowSpeed; // ���ο� ȿ���� ���� �ڷ�ƾ

    private Transform target; // ���� Ÿ��
    private int wavepointIndex = 0;

    public float enemyHp; // ���� �⺻ ü��

    public GameMgr gameMgr;

    public bool isDead = false;
   

    
    void Start()
    {
        target = Waypoints.points[0];
        orignalSpeed = speed; //���� �ӵ� ����
        gameMgr=GameObject.Find("GameMgr").GetComponent<GameMgr>();

        enemyHp = gameMgr.curEnemyHp;
    }

    public void TakeDamage(float damege)
    {
        if (isDead) return;

        enemyHp -= damege; // ������ ��ŭ ����
        if(enemyHp <= 0)
        {
            isDead = true;    
            GetComponentInChildren<HpBar>().DestroyHpBar();
            gameMgr.killCount++;
            gameMgr.killCount2++;
            gameMgr.killCount3++;
            Destroy(gameObject); // HP�� 0�� �Ǹ� �ı�
        }
    }

    public void Slow(float slowAmount)
    {
        if(slowSpeed != null)
        {
            StopCoroutine(slowSpeed); // ���� ���ο� ȿ�� ����
        }
        slowSpeed = StartCoroutine(SlowCoroutine(slowAmount)); //���ο� ȿ�� ����
    }

    private IEnumerator SlowCoroutine(float slowAmount)
    {
        float slowSpeed2 = orignalSpeed * (1- slowAmount);
        speed = Mathf.Max(slowSpeed2, 2.0f);
        Debug.Log("���ο� ����: ���� �ӵ� = " + speed);
        yield return new WaitForSeconds(3f); // 3�� ����
        speed = orignalSpeed; // ���� �ӵ��� �ǵ���
        Debug.Log("���ο� ����: ���� �ӵ� = " + speed); // ���ο� ���� �� �ӵ� ���
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
