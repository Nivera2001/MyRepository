using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform target;
    public RectTransform canvas;
    public RectTransform hpBar;
    public Camera mainCam;
    public GameObject hpBarPrefab;
    public GameObject hpObj;
    public float maxHp;
    void Start()
    {
        target = transform;
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        hpObj = Instantiate(hpBarPrefab);
        hpObj.transform.SetParent(canvas);
        hpBar = hpObj.GetComponent<RectTransform>();
        hpBar.transform.position = Vector3.zero;
        hpBar.localScale = Vector3.one;
        hpBar.localRotation = Quaternion.Euler(0, 0, 0);
        mainCam = Camera.main;
        maxHp = transform.parent.GetComponent<EnemyController>().enemyHp;
        //maxHp = transform.parent.GetComponent<EnemyController_NaviMesh>().enemyHp;
    }

    void Update()
    {
        Vector3 curPos = target.transform.position;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(curPos);
        Vector2 canvasPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas,
            screenPoint,
            mainCam,
            out canvasPos);
        if (hpBar != null)
        {
            hpBar.localPosition = canvasPos;
            hpBar.GetComponent<Slider>().value =
                (float)transform.parent.GetComponent<EnemyController>().enemyHp / maxHp;
            //(float)transform.parent.GetComponent<EnemyController_NaviMesh>().enemyHp / maxHp;
        }
    }

    public void DestroyHpBar()
    {
        Destroy(hpBar.gameObject);
    }
}
