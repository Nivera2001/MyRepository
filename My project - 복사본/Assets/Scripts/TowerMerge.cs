using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMerge : MonoBehaviour
{
    public int towerKind; // 포탑 종류 (1, 2, 3)
    public int level; // 포탑 레벨 (1, 2, 4)

    // 상위 레벨 포탑 프리팹
    public GameObject level2TowerPrefab;
   

    private bool isDragging = false;
    private Vector3 originalPosition;


    void Update()
    {
        // 드래그 앤 드롭
        if (isDragging)
        {
            // 마우스 위치에 포탑 따라가게 하기
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition.z = 35.0f; // 카메라에서의 거리
            //transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -2f;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        // 마우스 버튼을 놓았을 때 드래그 종료
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            TryMergeTower();
        }
    }

    void OnMouseDown()
    {
        // 드래그 시작
        isDragging = true;
        originalPosition = transform.position;
    }

    void TryMergeTower()
    {
        // Raycast를 사용하여 드롭된 위치에서 다른 포탑과 충돌 여부를 확인
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            TowerMerge otherTower = hit.collider.GetComponent<TowerMerge>();

            // 충돌한 오브젝트가 다른 포탑인지 확인
            if (otherTower != null && otherTower != this)
            {
                // towerKind와 level이 같은지 비교
                if (towerKind == otherTower.towerKind && level == otherTower.level)
                {
                    // 동일하면 업그레이드
                    MergeTowers(otherTower);
                }
                else
                {
                    // 다른 포탑이면 원래 위치로 돌아가기
                    transform.position = originalPosition;
                }
            }
            else
            {
                // 충돌 대상이 포탑이 아니면 원래 위치로 돌아가기
                transform.position = originalPosition;
            }
        }
        else
        {
            // 아무 것도 없으면 원래 위치로 돌아가기
            transform.position = originalPosition;
        }
    }

    void MergeTowers(TowerMerge otherTower)
    {
        GameObject upgradedTowerPrefab = null;

        if (level == 1)
        {
            upgradedTowerPrefab = level2TowerPrefab; // 레벨 1 -> 레벨 2 업그레이드
        }
        

        if (upgradedTowerPrefab != null)
        {
            // Find the TowerCreate script to access hillLocations
            TowerCreate towerCreate = FindObjectOfType<TowerCreate>();

            if (towerCreate != null)
            {
                // A의 위치를 hillLocations에서 제거
                towerCreate.hillLocations.Remove(this.transform); // A의 위치 제거

                // Create the upgraded tower at otherTower's position (B의 위치)
                GameObject upgradedTower = Instantiate(upgradedTowerPrefab, otherTower.transform.position, Quaternion.identity);

                // Destroy both original towers (A와 B)
                otherTower.gameObject.SetActive(false);
                Destroy(this.gameObject);       // A를 파괴
            }
        }
    }


}