using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float bossHp = 10000;
    public UiManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
    }

    public void TakeDamage(float damage)
    {
        bossHp -= damage;
        if (bossHp <= 0)
        {
            DestroyBoss();
        }
    }

    private void DestroyBoss()
    {
        Destroy(gameObject);
        uiManager.ShowVictoryPanel();
    }
    
        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
