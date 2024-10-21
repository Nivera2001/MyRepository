using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    public int towerKind; // ��ž ���� (1, 2, 3)
    public int level; // ��ž ���� (1, 2, 4)

    // ���� ���� ��ž ������
    public GameObject level2TowerPrefab;
   

    private bool isDragging = false;
    private Vector3 originalPosition;


    void Update()
    {
        // �巡�� �� ���
        if (isDragging)
        {
            // ���콺 ��ġ�� ��ž ���󰡰� �ϱ�
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition.z = 35.0f; // ī�޶󿡼��� �Ÿ�
            //transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -2f;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        // ���콺 ��ư�� ������ �� �巡�� ����
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            TryMergeTower();
        }
    }

    void OnMouseDown()
    {
        // �巡�� ����
        isDragging = true;
        originalPosition = transform.position;
    }

    void TryMergeTower()
    {
        // Raycast�� ����Ͽ� ��ӵ� ��ġ���� �ٸ� ��ž�� �浹 ���θ� Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            TowerMerge otherTower = hit.collider.GetComponent<TowerMerge>();

            // �浹�� ������Ʈ�� �ٸ� ��ž���� Ȯ��
            if (otherTower != null && otherTower != this)
            {
                // towerKind�� level�� ������ ��
                if (towerKind == otherTower.towerKind && level == otherTower.level)
                {
                    // �����ϸ� ���׷��̵�
                    MergeTowers(otherTower);
                }
                else
                {
                    // �ٸ� ��ž�̸� ���� ��ġ�� ���ư���
                    transform.position = originalPosition;
                }
            }
            else
            {
                // �浹 ����� ��ž�� �ƴϸ� ���� ��ġ�� ���ư���
                transform.position = originalPosition;
            }
        }
        else
        {
            // �ƹ� �͵� ������ ���� ��ġ�� ���ư���
            transform.position = originalPosition;
        }
    }

    void MergeTowers(TowerMerge otherTower)
    {
        GameObject upgradedTowerPrefab = null;

        if (level == 1)
        {
            upgradedTowerPrefab = level2TowerPrefab; // ���� 1 -> ���� 2 ���׷��̵�
        }
        

        if (upgradedTowerPrefab != null)
        {
            // Find the TowerCreate script to access hillLocations
            TowerCreate towerCreate = FindObjectOfType<TowerCreate>();

            if (towerCreate != null)
            {
                // A�� ��ġ�� hillLocations���� ����
                towerCreate.hillLocations.Remove(this.transform); // A�� ��ġ ����

                // Create the upgraded tower at otherTower's position (B�� ��ġ)
                GameObject upgradedTower = Instantiate(upgradedTowerPrefab, otherTower.transform.position, Quaternion.identity);

                // Destroy both original towers (A�� B)
                otherTower.gameObject.SetActive(false);
                Destroy(this.gameObject);       // A�� �ı�
            }
        }
    }


}