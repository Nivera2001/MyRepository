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
            attackPower = 10 + (2 * upgradeMgr.ccAtk); // 공격력 설정
            attackSpeed = 0.75f; // 공격 속도 설정
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
                enemy.Slow(slowAmount); // 슬로우값 전달
                
                enemy.TakeDamage(attackPower); // 데미지 전달
            }

            towerState = TOWERSTATE.ATTACK;
        }
    }
}
