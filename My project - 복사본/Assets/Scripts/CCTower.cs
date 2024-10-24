using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTower : TowerController
{
    public float slowAmount = 0.5f;

    public UpgradeMgr upgradeMgr;

    private void Awake()
    {
        upgradeMgr = GameObject.Find("UpgradeMgr").GetComponent<UpgradeMgr>();
        if (gameObject.tag == "Tower")
        {
            attackPower = 10 + (2 * upgradeMgr.ccAtk); // ���ݷ� ����
            attackSpeed = 0.75f; // ���� �ӵ� ����
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            attackPower = 15 + (2 * upgradeMgr.ccAtk);
            attackSpeed = 0.75f;
        }
        else if (gameObject.tag == "EliteTower")
        {
            attackPower = 20 + (2 * upgradeMgr.ccAtk);
            attackSpeed = 0.75f;
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
