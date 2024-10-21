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
            Debug.Log("사거리 내 적 발견!!!!!!!!!!!");
            towerController.targetEnemy = other.gameObject;

            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log(other.name + " :::  사거리에서 벗어남...");
            enemies.Remove(other.gameObject);
        }
    }
}
