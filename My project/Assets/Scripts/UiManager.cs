using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    
    public GameMgr gameMgr;

    public CCTower ccTower;
    public SniperTower sniperTower;
    public SplashTower splashTower;
    
    public Text hpText;
    public Text waveText;
    public Text costText;
    public Text killText;
    public Text upgradeText;

    
    void Start()
    {
        gameMgr=GameObject.Find("GameMgr").GetComponent<GameMgr>();
        

    }

    public void ccTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            gameMgr.currentUpgradeMultiplier *= 1.025f; // 강화 배율 증가
            UpdateAllCCTowers(); // 모든 CCTower의 공격력 업데이트
        }
        else return;
    }

    public void sniperTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            sniperTower.attackPower *= 1.025f;
        }
        else return;
    }

    public void splashTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            splashTower.attackPower *= 1.025f;
        }
        else return;
    }

    private void UpdateAllCCTowers()
    {
        CCTower[] towers = FindObjectsOfType<CCTower>(); // 씬 내의 모든 CCTower 찾기
        foreach (var tower in towers)
        {
            tower.attackPower = tower.initialAttackPower * gameMgr.currentUpgradeMultiplier; // 초기 공격력에 현재 배율 적용
        }
    }



    void Update()
    {
        //ccTower = FindObjectOfType<CCTower>();
        //sniperTower = FindObjectOfType<SniperTower>();
        //splashTower = FindObjectOfType<SplashTower>();

        if (gameMgr.killCount2 >= 10)
        {
            gameMgr.killCount2 -= 10;
            gameMgr.Cost++;
        }
        if (gameMgr.killCount3 >= 5) 
        {
            gameMgr.killCount3 -= 5;
            gameMgr.Upgrade++;
            
        }
        hpText.text = "목숨 " + gameMgr.Hp;
        waveText.text = "단계 " + gameMgr.curLv;
        costText.text = "Cost : " + gameMgr.Cost;
        killText.text = "킬수 " + gameMgr.killCount;
        upgradeText.text = "재료 : "+gameMgr.Upgrade;
    }
}
