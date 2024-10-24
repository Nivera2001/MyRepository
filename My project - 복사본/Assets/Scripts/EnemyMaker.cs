using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public int enemyCnt;
    public int enemyMaxCnt = 0;
    public float curTime;
    public float coolTime = .25f;
    public bool isRunning = false;
    public bool bossSpawned = false;
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
                if (gameMgr.curLv < 16)
                {
                    GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                    enemy.name = "ENEMY_" + enemyCnt;
                    enemy.GetComponent<EnemyController>().enemyHp += 50;
                    enemyCnt++;
                }
                else if (gameMgr.curLv == 16&&bossSpawned)
                {
                    SpawnBoss();
                }

            }
        }
    }

    public void InitEnemyMaker()
    {
        enemyCnt = 0;
        isRunning = true;
        if (gameMgr.curLv >= 15)
        {
            isRunning = false;
            Debug.Log("15웨이브를 넘겼습니다.");
            return;
        }
        else
        {
            gameMgr.curLv++;
            gameMgr.StageLvUp();
        }
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab,transform.position, transform.rotation);
        boss.name = "BOSS";
        enemyMaxCnt++;
        bossSpawned = true;
        isRunning = false;
    }
}
