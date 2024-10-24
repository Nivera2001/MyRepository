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
                Debug.Log("��� ��ġ�� �������� �ʾҽ��ϴ�.");
                return;
            }

            int random = Random.Range(0, hillLocations.Count);
            Transform randomHill = hillLocations[random];

            int randomTower = Random.Range(0, towerPrefabs.Length);
            GameObject tower = towerPrefabs[randomTower];

            Vector3 spawn = new Vector3(randomHill.position.x, randomHill.position.y + 1, randomHill.position.z);
            GameObject newTower = Instantiate(tower, spawn, randomHill.rotation);

            hillLocations.RemoveAt(random);

            // Ÿ���� ��� ��ġ�� Ÿ�� ���� ��ũ��Ʈ�� ����
            newTower.AddComponent<Tower>().Initialize(randomHill, this);
        }
        else if (gameMgr.Cost <= 0)
        {
            Debug.Log("�ڽ�Ʈ�� �����մϴ�");
        }

    }

    public void RestoreHillLocation(Transform hill)
    {
        // ��� ��ġ�� �ٽ� ����Ʈ�� ����
        if (!hillLocations.Contains(hill))
        {
            hillLocations.Add(hill);
            Debug.Log(hill.name + " ��ġ ����");
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
                Debug.Log("Wall" + i + "��(��) ã�� �� �����ϴ�.");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}

// Ÿ�� Ŭ������ Ÿ���� �������ų� ������ �� ��� ��ġ ����
public class Tower : MonoBehaviour
{
    private Transform hillLocation; // Ÿ���� ��ġ�� ��� ��ġ
    private TowerCreate towerCreate; // TowerCreate ��ũ��Ʈ ����

    // Ÿ�� �ʱ�ȭ �޼���
    public void Initialize(Transform hill, TowerCreate creator)
    {
        hillLocation = hill;
        towerCreate = creator;
    }

    // Ÿ���� �ı��ǰų� ������ �� ��� ��ġ�� ����
    private void OnDestroy()
    {
        if (towerCreate != null && hillLocation != null)
        {
            towerCreate.RestoreHillLocation(hillLocation); // ��� ��ġ ����
        }
    }
}

