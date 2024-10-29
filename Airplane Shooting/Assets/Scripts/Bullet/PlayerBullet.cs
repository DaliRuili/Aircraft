using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Action backToPoolAction;

    private void Update()
    {
        // 子弹向上飞行
        transform.position += Vector3.up * (Const.playerBulletSpeed * Time.deltaTime);

        // 超过屏幕上面
        if (Camera.main.WorldToScreenPoint(transform.position).y >= Screen.height + 50)
        {
            BackToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            BackToPool();
        }
    }

    private void BackToPool()
    {
        ActiveSelf(false);
        if (null != backToPoolAction)
        {
            backToPoolAction();
        }
    }
    
    public void ActiveSelf(bool active)
    {
        gameObject.SetActive(active);
    }
    
    /// <summary>
    /// 设置坐标
    /// </summary>
    public void SetStartPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
