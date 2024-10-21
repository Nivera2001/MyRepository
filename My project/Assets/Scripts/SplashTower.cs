using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerController;

public class SplashTower : TowerController
{
    public float splahRadius = 2f;
    private void Awake()
    {
        if (gameObject.tag == "Tower")
        {
            attackPower = 30; // ���ݷ� ����
            attackSpeed = 1.0f; // ���� �ӵ� ����
        }
        else if (gameObject.tag == "AdvancedTower")
        {
            attackPower = 45;
            attackSpeed = 1.0f;
        }
        else if (gameObject.tag == "EliteTower")
        {
            attackPower = 60;
            attackSpeed = 1.0f;
        }
    }

    public override void Attack(GameObject target)
    {
        if (target != null)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(target.transform.position, splahRadius);
            foreach (var hitEnemy in hitEnemies)
            {
                if (hitEnemy.CompareTag("Enemy"))
                {
                    hitEnemy.GetComponent<EnemyController>().TakeDamage(attackPower); // ������ ������ ����
                }
            }

                towerState = TOWERSTATE.ATTACK;
            
        }
    }
}
