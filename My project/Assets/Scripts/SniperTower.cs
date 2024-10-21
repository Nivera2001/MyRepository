using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : TowerController
{
    private void Awake()
    {
        if (gameObject.tag == "Tower")
        {
            attackPower = 15; // 공격력 설정
            attackSpeed = 0.5f; // 공격 속도 설정
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
                enemy.TakeDamage(attackPower); // 데미지 전달
            }

            towerState = TOWERSTATE.ATTACK;
        }
    }
}
