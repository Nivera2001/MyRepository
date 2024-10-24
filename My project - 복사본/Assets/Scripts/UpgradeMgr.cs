using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeMgr : MonoBehaviour
{
   
    public int ccAtk = 1;
    public int sniperAtk = 1;
    public int splashAtk = 1;

    public GameMgr gameMgr;

    private void Start()
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
    }

    public void ccTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            ccAtk++;
        }
        else
            return;
        
    }

    public void sniperTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            sniperAtk++;
        }
        else
            return;

    }

    public void splashTowerAtk()
    {
        if (gameMgr.Upgrade >= 1)
        {
            splashAtk++;
        }
        else
            return;

    }
}
