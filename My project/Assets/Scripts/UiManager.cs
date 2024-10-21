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
            gameMgr.currentUpgradeMultiplier *= 1.025f; // ��ȭ ���� ����
            UpdateAllCCTowers(); // ��� CCTower�� ���ݷ� ������Ʈ
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
        CCTower[] towers = FindObjectsOfType<CCTower>(); // �� ���� ��� CCTower ã��
        foreach (var tower in towers)
        {
            tower.attackPower = tower.initialAttackPower * gameMgr.currentUpgradeMultiplier; // �ʱ� ���ݷ¿� ���� ���� ����
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
        hpText.text = "��� " + gameMgr.Hp;
        waveText.text = "�ܰ� " + gameMgr.curLv;
        costText.text = "Cost : " + gameMgr.Cost;
        killText.text = "ų�� " + gameMgr.killCount;
        upgradeText.text = "��� : "+gameMgr.Upgrade;
    }
}
