using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AircraftFactory
{
    private static float m_enemyTime = 5;
    /// <summary>
    /// 对象池
    /// </summary>
    private static Dictionary<AircraftType, Queue<EnemyAircraft>> m_enemyPool = new Dictionary<AircraftType, Queue<EnemyAircraft>>();

    private static List<EnemyAircraft> m_enemy = new List<EnemyAircraft>();
    public static void Update()
    {
        m_enemyTime += Time.deltaTime;
        if (m_enemyTime >= Const.EnemySpawnTime)
        {
            RandomGenerateEnemy();
            m_enemyTime = 0;
        }
    }
    private static Transform m_aircraftRoot = null;
    public static BaseAircraft CreateAircraft(AircraftType type)
    {
        if (m_aircraftRoot == null)
        {
            GameObject rootObj = new GameObject("AircraftRoot");
            m_aircraftRoot = rootObj.transform;
        }
        BaseAircraft aircraft = null;
        switch (type)
        {
            case AircraftType.Player:
                {
                    var prefab = ResourceMgr.Instance.LoadRes("Player/Player");
                    var obj = Object.Instantiate(prefab);
                    aircraft = obj.GetComponent<PlayerAircraft>();
                    aircraft.blood = Const.PlayerBlood;
                    break;
                }
            case AircraftType.Enemy1:
                {
                    var prefab = ResourceMgr.Instance.LoadRes("Enemy/Enemy1");
                    var obj = Object.Instantiate(prefab);
                    aircraft = obj.GetComponent<EnemyAircraft>();
                    aircraft.blood = Const.EnemyBlood1;
                    break;
                }
            case AircraftType.Enemy2:
                {
                    var prefab = ResourceMgr.Instance.LoadRes("Enemy/Enemy2");
                    var obj = Object.Instantiate(prefab);
                    aircraft = obj.GetComponent<EnemyAircraft>();
                    aircraft.blood = Const.EnemyBlood2;
                    break;
                }
        }
        if (aircraft == null) return aircraft;
        aircraft.transform.SetParent(m_aircraftRoot, false);
        aircraft.m_aircraftType = type;
        return aircraft;
    }

    public static void RandomGenerateEnemy()
    {
        EnemyAircraft enemy = null;
        var enemyType = (AircraftType)Random.Range(0, 2);
        if (m_enemyPool.ContainsKey(enemyType) && m_enemyPool[enemyType].Count > 0)
        {
            enemy = m_enemyPool[enemyType].Dequeue();
            enemy.ActiveSelf(true);
        }
        else
        {
            enemy = (EnemyAircraft)CreateAircraft(enemyType);
            enemy.backToPoolAction = () =>
            {
                if (!m_enemyPool.ContainsKey(enemyType))
                {
                    m_enemyPool[enemyType] = new Queue<EnemyAircraft>();
                }
                m_enemyPool[enemyType].Enqueue(enemy);
                if (!m_enemy.Contains(enemy))
                {
                    m_enemy.Remove(enemy);
                }
            };
        }
        enemy.moveSpeed = Const.EnemySpeed[(int)enemyType];
        enemy.RandomPos();
        enemy.m_timeToFire = 1;
        if (!m_enemy.Contains(enemy))
        {
            m_enemy.Add(enemy);
        }
    }

    public static void ClearAll()
    {
        m_enemyPool.Clear();
        m_enemy.Clear();
        if (m_aircraftRoot != null)
        {
            Object.Destroy(m_aircraftRoot.gameObject);
        }
        m_aircraftRoot = null;
    }

}
public enum AircraftType
{
    Enemy1,
    Enemy2,
    Player,
}
