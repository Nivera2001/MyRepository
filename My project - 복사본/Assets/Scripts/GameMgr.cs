using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public int curLv = 0;
    public int curEnemyHp = 20;
    public int stageEnemyCnt = 20;
    public int killCount;
    public int killCount2;
    public int killCount3;
    public int Hp = 3;
    public int Cost = 5;
    public int Upgrade;

    public float currentUpgradeMultiplier = 1.0f;



    public void StageLvUp()
    {       
        curEnemyHp += 45;
        
    }
}
