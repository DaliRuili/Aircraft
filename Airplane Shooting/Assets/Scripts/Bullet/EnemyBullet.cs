using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// 回收委托
    /// </summary>
    public Action backToPoolAction;

    /// <summary>
    /// 是否追踪
    /// </summary>
    [HideInInspector]
    public bool isTrace;

    /// <summary>
    /// 子弹射向玩家的向量
    /// </summary>
    public Vector3 tarDir;
    public void SetDistance(Vector3 dir)
    {
        tarDir = dir;
    }

    public void SetStartPos(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BackToPool();
        }
    }

    private void Update()
    {
        // 超过屏幕外面
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y <= -50 || screenPos.x < -50 || screenPos.x > Screen.width + 50)
        {
            BackToPool();
        }
        Movement();
    }
    private Vector3 velocity; // 子弹当前速度向量  
    private void Movement()
    {
        if (isTrace)
        {
            if (GameMgr.Instance.player)
            {
                var pos = GameMgr.Instance.player.transform.position;
                Vector3 desiredVelocity = (pos - transform.position).normalized * 10;
                velocity = Vector3.Lerp(velocity, desiredVelocity, Time.deltaTime * 0.3f);
            
                // 更新子弹位置  
                transform.position += velocity * Time.deltaTime;
            }
        }
        else
        {
            transform.Translate(tarDir * Const.EnemyBulletSpeed * Time.deltaTime);
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
}
