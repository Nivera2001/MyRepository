using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreate : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public List<Transform> hillLocations = new List<Transform>();

    public GameMgr gameMgr;

    public void ClickSummon()
    {
        if (gameMgr.Cost > 0)
        {
            gameMgr.Cost--;
            if (hillLocations.Count == 0)
            {
                Debug.Log("언덕 위치가 설정되지 않았습니다.");
                return;
            }

            int random = Random.Range(0, hillLocations.Count);
            Transform randomHill = hillLocations[random];

            int randomTower = Random.Range(0, towerPrefabs.Length);
            GameObject tower = towerPrefabs[randomTower];

            Vector3 spawn = new Vector3(randomHill.position.x, randomHill.position.y + 1, randomHill.position.z);
            GameObject newTower = Instantiate(tower, spawn, randomHill.rotation);

            hillLocations.RemoveAt(random);

            // 타워에 언덕 위치와 타워 관리 스크립트를 전달
            newTower.AddComponent<Tower>().Initialize(randomHill, this);
        }
        else if (gameMgr.Cost <= 0)
        {
            Debug.Log("코스트가 부족합니다");
        }

    }

    public void RestoreHillLocation(Transform hill)
    {
        // 언덕 위치를 다시 리스트에 복원
        if (!hillLocations.Contains(hill))
        {
            hillLocations.Add(hill);
            Debug.Log(hill.name + " 위치 복원");
        }
    }

    void Start()
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();

        for (int i = 1; i <= 75; i++)
        {
            GameObject hill = GameObject.Find("Wall" + i);
            if (hill != null)
            {
                hillLocations.Add(hill.transform);
            }
            else
            {
                Debug.Log("Wall" + i + "을(를) 찾을 수 없습니다.");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}

// 타워 클래스에 타워가 합쳐지거나 삭제될 때 언덕 위치 복원
public class Tower : MonoBehaviour
{
    private Transform hillLocation; // 타워가 설치된 언덕 위치
    private TowerCreate towerCreate; // TowerCreate 스크립트 참조

    // 타워 초기화 메서드
    public void Initialize(Transform hill, TowerCreate creator)
    {
        hillLocation = hill;
        towerCreate = creator;
    }

    // 타워가 파괴되거나 합쳐질 때 언덕 위치를 복원
    private void OnDestroy()
    {
        if (towerCreate != null && hillLocation != null)
        {
            towerCreate.RestoreHillLocation(hillLocation); // 언덕 위치 복원
        }
    }
}

