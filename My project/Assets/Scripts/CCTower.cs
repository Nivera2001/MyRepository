using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTower : TowerController
{
    public float slowAmount = 1.0f;

    public float initialAttackPower; // 초기 공격력 변수 추가

    private void Awake()
    {
        InitializeTower();
    }

    private void InitializeTower()
    {
        if (gameObject.tag == "Tower")
        {
            initialAttackPower = 10; // 기본 공격력 저장
            attackPower = initialAttackPower; // 초기 공격력 설정
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            initialAttackPower = 15; // 기본 공격력 저장
            attackPower = initialAttackPower; // 초기 공격력 설정
        }
        else if (gameObject.tag == "EliteTower")
        {
            initialAttackPower = 20; // 기본 공격력 저장
            attackPower = initialAttackPower; // 초기 공격력 설정
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
