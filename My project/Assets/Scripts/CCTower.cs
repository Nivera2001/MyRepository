using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTower : TowerController
{
    public float slowAmount = 1.0f;

    public float initialAttackPower; // �ʱ� ���ݷ� ���� �߰�

    private void Awake()
    {
        InitializeTower();
    }

    private void InitializeTower()
    {
        if (gameObject.tag == "Tower")
        {
            initialAttackPower = 10; // �⺻ ���ݷ� ����
            attackPower = initialAttackPower; // �ʱ� ���ݷ� ����
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            initialAttackPower = 15; // �⺻ ���ݷ� ����
            attackPower = initialAttackPower; // �ʱ� ���ݷ� ����
        }
        else if (gameObject.tag == "EliteTower")
        {
            initialAttackPower = 20; // �⺻ ���ݷ� ����
            attackPower = initialAttackPower; // �ʱ� ���ݷ� ����
        }
    }

    public override void Attack(GameObject target)
    {
        if(target!=null)
        {
            var enemy = target.GetComponent<EnemyController>();
            if(enemy != null)
            {
                Debug.Log(slowAmount);
                enemy.Slow(slowAmount); // ���ο찪 ����
                
                enemy.TakeDamage(attackPower); // ������ ����
            }

            towerState = TOWERSTATE.ATTACK;
        }
    }
}
