using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletGenerator
{
    private static Transform enemyBulletRoot;
    private static Queue<EnemyBullet> bullet1Queue = new Queue<EnemyBullet>();
    private static Queue<EnemyBullet> bullet2Queue = new Queue<EnemyBullet>();
    /// <summary>
    /// 是否是第一颗子弹
    /// </summary>
    /// <summary>
    /// 根据敌机类型创建子弹
    /// </summary>
    /// <param name="aircraftType">敌机类型</param>
    /// <param name="aircraftPos">敌机坐标</param>
    public static void GenerateBullet(Vector3 aircraftPos, bool isBullet1)
    {
        if (null == enemyBulletRoot)
        {
            var rootObj = new GameObject("EnemyBulletRoot");
            enemyBulletRoot = rootObj.transform;
        }
        CreateBulletObj(aircraftPos, isBullet1);
    }
    /// <summary>
    /// 创建子弹物体
    /// </summary>
    /// <param name="startPos"></param>
    /// <returns></returns>
    private static EnemyBullet CreateBulletObj(Vector3 startPos, bool isBullet1)
    {
        EnemyBullet bullet = null;
        var prefabPath = "";
        if (isBullet1)
        {
            prefabPath = "Bullet/EnemyBullet1";
            if (bullet1Queue.Count > 0)
            {
                bullet = bullet1Queue.Dequeue();
            }
            else
            {
                var prefab = ResourceMgr.Instance.LoadRes(prefabPath);
                var obj = Object.Instantiate(prefab);
                obj.transform.SetParent(enemyBulletRoot, false);
                bullet = obj.GetComponent<EnemyBullet>();
                bullet.backToPoolAction = () =>
                {
                    bullet.transform.position = Vector3.zero;
                    bullet1Queue.Enqueue(bullet);
                };
            }
            var dir = -Vector3.up;
            if (GameMgr.Instance.player)
            {
                dir = (GameMgr.Instance.player.transform.position - startPos).normalized;
            }
            bullet.SetDistance(dir);
        }
        else
        {
            prefabPath = "Bullet/EnemyBullet2";
            if (bullet2Queue.Count > 0)
            {
                bullet = bullet2Queue.Dequeue();
            }
            else
            {
                var prefab = ResourceMgr.Instance.LoadRes(prefabPath);
                var obj = Object.Instantiate(prefab);
                obj.transform.SetParent(enemyBulletRoot, false);
                bullet = obj.GetComponent<EnemyBullet>();
                bullet.backToPoolAction = () =>
                {
                    bullet2Queue.Enqueue(bullet);
                };
            }
            bullet.isTrace = true;
        }
        bullet.SetStartPos(startPos);
        bullet.ActiveSelf(true);
        return bullet;
    }

    /// <summary>
    /// 清理
    /// </summary>
    public static void CLear()
    {
        if (null != enemyBulletRoot)
        {
            Object.Destroy(enemyBulletRoot.gameObject);
            enemyBulletRoot = null;
        }
        bullet1Queue.Clear();
        bullet2Queue.Clear();
    }


}
