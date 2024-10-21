using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetecting : MonoBehaviour
{
    public List<GameObject> enemies;
    public TowerController towerController;

    void Start()
    {
        towerController = transform.parent.GetComponent<TowerController>();
    }

    void Update()
    {
        if (enemies.Count < 0 && enemies[0] == null)
            enemies.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("��Ÿ� �� �� �߰�!!!!!!!!!!!");
            towerController.targetEnemy = other.gameObject;

            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log(other.name + " :::  ��Ÿ����� ���...");
            enemies.Remove(other.gameObject);
        }
    }
}
