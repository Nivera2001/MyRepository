using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : TowerController
{
    public UpgradeMgr upgradeMgr;

    private void Awake()
    {
        upgradeMgr = GameObject.Find("UpgradeMgr").GetComponent<UpgradeMgr>();
        if (gameObject.tag == "Tower")
        {
            attackPower = 15 + (5 * upgradeMgr.sniperAtk); // ���ݷ� ����
            attackSpeed = 0.5f; // ���� �ӵ� ����
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            attackPower = 25 + (5 * upgradeMgr.sniperAtk);
            attackSpeed = 0.5f;
        }
        else if(gameObject.tag == "EliteTower")
        {
            attackPower = 25 + (5 * upgradeMgr.sniperAtk);
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
