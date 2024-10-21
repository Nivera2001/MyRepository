using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCnt;
    public int enemyMaxCnt = 0;
    public float curTime;
    public float coolTime = .25f;
    public bool isRunning = false;
    public GameMgr gameMgr;


    // Start is called before the first frame update
    void Start()
    {
        
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        enemyMaxCnt = gameMgr.stageEnemyCnt;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCnt >= enemyMaxCnt)
        {
            isRunning = false;
        }
        if (isRunning)
        {
            curTime += Time.deltaTime;
            if(curTime > coolTime)
            {
                curTime = 0;
                GameObject enemy = Instantiate(enemyPrefab,transform.position,transform.rotation);
                enemy.name = "ENEMY_" + enemyCnt;
                enemy.GetComponent<EnemyController>().enemyHp += 50;
                enemyMaxCnt = gameMgr.stageEnemyCnt;
                enemyCnt++;

            }
        }
    }

    public void InitEnemyMaker()
    {
        enemyCnt = 0;
        isRunning = true;
        gameMgr.curLv++;
        gameMgr.StageLvUp();
    }
}
