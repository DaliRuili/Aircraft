using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletGenerator
{
    /// <summary>
    /// 玩家子弹对象池
    /// </summary>
    private Queue<PlayerBullet> m_bulletPool = new Queue<PlayerBullet>();
    private Transform m_bulletRoot;

    private float m_timer;//计时器
    public void Update()
    {
        if (GameMgr.Instance.player ==null)
            return;
        m_timer += Time.deltaTime;
        if (m_timer>=Const.FireTimer)
        {
            CreateBullet();
            m_timer = 0;
        }
    }

    private void CreateBullet()
    {
        if (m_bulletRoot == null)
        {
            var rootObj = new GameObject("PlayerBulletRoot");
            m_bulletRoot = rootObj.transform;
        }

        PlayerBullet bullet = null;
        if (m_bulletPool.Count>0)
        {
            bullet = m_bulletPool.Dequeue();
            bullet.ActiveSelf(true);
        }
        else
        {
            var prefab = ResourceMgr.Instance.LoadRes("Bullet/PlayerBullet");
            var obj = Object.Instantiate(prefab, m_bulletRoot, false);
            bullet = obj.GetComponent<PlayerBullet>();
            bullet.backToPoolAction = () =>
            {
                m_bulletPool.Enqueue(bullet);
            };
        }
        bullet.SetStartPos(GameMgr.Instance.player.transform.position+new Vector3(0,0.1f,0));
    }

    public void ClearBullet()
    {
        if(null != m_bulletRoot)
        {
            Object.Destroy(m_bulletRoot.gameObject);
            m_bulletRoot = null;
        }
        m_bulletPool.Clear();
    }
}