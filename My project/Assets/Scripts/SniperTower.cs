using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : TowerController
{
    private void Awake()
    {
        if (gameObject.tag == "Tower")
        {
            attackPower = 15; // ���ݷ� ����
            attackSpeed = 0.5f; // ���� �ӵ� ����
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            attackPower = 20;
            attackSpeed = 0.5f;
        }
        else if(gameObject.tag == "EliteTower")
        {
            attackPower = 25;
            attackSpeed = 0.5f;
        }
    }

    public override void Attack(GameObject target)
    {
        if (target != null)
        {
            var enemy = target.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackPower); // ������ ����
            }

            towerState = TOWERSTATE.ATTACK;
        }
    }
}
