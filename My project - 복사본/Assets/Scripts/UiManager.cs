using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    
    public GameMgr gameMgr;
    public UpgradeMgr upgradeMgr;
    public EnemyMaker enemyMaker;

    public CCTower ccTower;
    public SniperTower sniperTower;
    public SplashTower splashTower;
    
    public Text hpText;
    public Text waveText;
    public Text costText;
    public Text killText;
    public Text upgradeText;

    public GameObject GameOverPanel;
    public GameObject VictoryPanel;

    
    void Start()
    {
        gameMgr=GameObject.Find("GameMgr").GetComponent<GameMgr>();
        upgradeMgr=GameObject.Find("UpgradeMgr").GetComponent<UpgradeMgr>();
        enemyMaker=GameObject.Find("EnemyBase").GetComponent<EnemyMaker>();

    }

    public void ccTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            CCTower[] ccTowers = FindObjectsOfType<CCTower>();
            foreach (var tower in ccTowers)
            {
                if (tower.tag == "Tower")
                {
                    tower.attackPower = 10 + (2 * upgradeMgr.ccAtk);
                }

                else if (tower.tag == "AdvancedTower")
                {
                    tower.attackPower = 15 + (2 * upgradeMgr.ccAtk);
                }
            }

        }
        else return;
    }

    public void sniperTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            SniperTower[] sniperTowers = FindObjectsOfType<SniperTower>();
            foreach (var tower in sniperTowers)
            {
                if (tower.tag == "Tower")
                {
                    tower.attackPower = 15 + (5 * upgradeMgr.sniperAtk);
                }
                else if(tower.tag == "AdvancedTower")
                {
                    tower.attackPower = 25 + (5 * upgradeMgr.sniperAtk);
                }
            }
        }
        else return;
    }

    public void splashTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            gameMgr.Upgrade--;
            SplashTower[] splashTowers = FindObjectsOfType<SplashTower>();
            foreach (var tower in splashTowers)
            {
                if (tower.tag == "Tower")
                {
                    tower.attackPower = 30 + (10 * upgradeMgr.splashAtk);
                }
                else if (tower.tag == "AdvancedTower")
                {
                    tower.attackPower = 50 + (10 * upgradeMgr.splashAtk);
                }
            }
        }
        else return;
    }

    public void ClickPause()
    {
        Time.timeScale = 0;
    }

    public void ClickContinue()
    {
        Time.timeScale = 1f;
    }

    public void ClickReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }

    public void CilckFail()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
       
    }

    public void ShowVictoryPanel()
    {
        Time.timeScale = 0;
        VictoryPanel.SetActive(true);
        Debug.Log("½Â¸®");

    }

    



    void Update()
    {
        //ccTower = FindObjectOfType<CCTower>();
        //sniperTower = FindObjectOfType<SniperTower>();
        //splashTower = FindObjectOfType<SplashTower>();

        if (gameMgr.Hp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            
        }

        if (gameMgr.curLv == 16 && enemyMaker.enemyCnt == 0)
        {
            Debug.Log("½Â¸®");
        }

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
        hpText.text = "¸ñ¼û " + gameMgr.Hp;
        waveText.text = "´Ü°è " + gameMgr.curLv;
        costText.text = "Cost : " + gameMgr.Cost;
        killText.text = "Å³¼ö " + gameMgr.killCount;
        upgradeText.text = "Àç·á : "+gameMgr.Upgrade;
    }
}
