using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    public int level;
    public int towerKind;

    public float attackPower;
    public float attackCurTime;
    public float attackSpeed;

    public GameObject targetEnemy;

    public enum TOWERSTATE
    {
        IDLE,
        ATTACK,
        NONE
    }
    public TOWERSTATE towerState;
    public EnemyDetecting enemyDetecting;
    public Animator anim;

    void Start()
    {
        towerState = TOWERSTATE.IDLE;
        enemyDetecting = GetComponentInChildren<EnemyDetecting>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 공격 쿨타임 업데이트
        attackCurTime += Time.deltaTime;

        // 타겟이 파괴되거나 비활성화되었는지 확인
        if (targetEnemy == null || !targetEnemy.activeSelf)
        {
            targetEnemy = null; // 타겟 초기화
            towerState = TOWERSTATE.IDLE; // 상태를 IDLE로 변경
        }

        // 타워 상태에 따라 로직 수행
        switch (towerState)
        {
            case TOWERSTATE.IDLE:
                //anim.SetInteger("TowerState", (int)towerState);
                // 적이 탐지되었는지 확인
                if (enemyDetecting.enemies.Count > 0)
                {
                    // 적 리스트에서 살아있는 적을 찾음
                    for (int i = 0; i < enemyDetecting.enemies.Count; i++)
                    {
                        if (enemyDetecting.enemies[i] != null && enemyDetecting.enemies[i].activeSelf)
                        {
                            targetEnemy = enemyDetecting.enemies[i]; // 타겟 설정
                            towerState = TOWERSTATE.ATTACK; // 상태 변경
                            break;
                        }
                    }
                }
                break;

            case TOWERSTATE.ATTACK:
                //anim.SetInteger("TowerState", (int)towerState);

                if (targetEnemy != null)
                {
                    // 타겟을 향해 타워 회전
                    transform.LookAt(targetEnemy.transform);
                    Vector3 dir = transform.localRotation.eulerAngles;
                    dir.x = 0;  // 타워가 수평으로만 회전하도록
                    transform.localRotation = Quaternion.Euler(dir);

                    // 공격 쿨타임이 지난 경우 공격
                    if (attackCurTime > attackSpeed)
                    {
                        Attack(targetEnemy);
                        attackCurTime = 0;  // 쿨타임 초기화
                    }

                    // 적이 리스트에서 사라졌는지 확인
                    if (!enemyDetecting.enemies.Contains(targetEnemy))
                    {
                        targetEnemy = null;
                        towerState = TOWERSTATE.IDLE; // 적이 사거리에서 벗어나면 IDLE 상태로 전환
                    }
                }
                else
                {
                    towerState = TOWERSTATE.IDLE; // 적이 없으면 IDLE 상태로 변경
                }
                break;

            case TOWERSTATE.NONE:
                // 필요 시 추가 로직
                break;
        }
    }
    public virtual void Attack(GameObject target)
    {
        if (target != null)
        {
            var enemy = target.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackPower); // 공격력에 따라 데미지 전달
            }

            // 상태 변경
            towerState = TOWERSTATE.ATTACK;
        }
    }
}
