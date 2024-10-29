using System;
using UnityEngine;

public class BaseAircraft : MonoBehaviour
{
    protected Transform m_transform;
    protected GameObject m_gameObject;
    protected Collider2D m_collider;
    protected Rigidbody2D m_rigidbody;
    public AircraftType m_aircraftType;
    [HideInInspector]
    public int blood;//血量
    protected virtual void Awake()
    {
        m_gameObject = gameObject;
        m_transform = m_gameObject.transform;
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
