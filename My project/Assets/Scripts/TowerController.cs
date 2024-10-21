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
        // ���� ��Ÿ�� ������Ʈ
        attackCurTime += Time.deltaTime;

        // Ÿ���� �ı��ǰų� ��Ȱ��ȭ�Ǿ����� Ȯ��
        if (targetEnemy == null || !targetEnemy.activeSelf)
        {
            targetEnemy = null; // Ÿ�� �ʱ�ȭ
            towerState = TOWERSTATE.IDLE; // ���¸� IDLE�� ����
        }

        // Ÿ�� ���¿� ���� ���� ����
        switch (towerState)
        {
            case TOWERSTATE.IDLE:
                //anim.SetInteger("TowerState", (int)towerState);
                // ���� Ž���Ǿ����� Ȯ��
                if (enemyDetecting.enemies.Count > 0)
                {
                    // �� ����Ʈ���� ����ִ� ���� ã��
                    for (int i = 0; i < enemyDetecting.enemies.Count; i++)
                    {
                        if (enemyDetecting.enemies[i] != null && enemyDetecting.enemies[i].activeSelf)
                        {
                            targetEnemy = enemyDetecting.enemies[i]; // Ÿ�� ����
                            towerState = TOWERSTATE.ATTACK; // ���� ����
                            break;
                        }
                    }
                }
                break;

            case TOWERSTATE.ATTACK:
                //anim.SetInteger("TowerState", (int)towerState);

                if (targetEnemy != null)
                {
                    // Ÿ���� ���� Ÿ�� ȸ��
                    transform.LookAt(targetEnemy.transform);
                    Vector3 dir = transform.localRotation.eulerAngles;
                    dir.x = 0;  // Ÿ���� �������θ� ȸ���ϵ���
                    transform.localRotation = Quaternion.Euler(dir);

                    // ���� ��Ÿ���� ���� ��� ����
                    if (attackCurTime > attackSpeed)
                    {
                        Attack(targetEnemy);
                        attackCurTime = 0;  // ��Ÿ�� �ʱ�ȭ
                    }

                    // ���� ����Ʈ���� ��������� Ȯ��
                    if (!enemyDetecting.enemies.Contains(targetEnemy))
                    {
                        targetEnemy = null;
                        towerState = TOWERSTATE.IDLE; // ���� ��Ÿ����� ����� IDLE ���·� ��ȯ
                    }
                }
                else
                {
                    towerState = TOWERSTATE.IDLE; // ���� ������ IDLE ���·� ����
                }
                break;

            case TOWERSTATE.NONE:
                // �ʿ� �� �߰� ����
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
                enemy.TakeDamage(attackPower); // ���ݷ¿� ���� ������ ����
            }

            // ���� ����
            towerState = TOWERSTATE.ATTACK;
        }
    }
}
