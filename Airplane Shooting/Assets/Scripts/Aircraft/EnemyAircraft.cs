using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAircraft : BaseAircraft
{
    [HideInInspector]
    public float moveSpeed = 0;
    public Action backToPoolAction;

    public float m_timeToFire;

    private bool isBullet1 = false;

    public void RandomPos()
    {
        var randomX = UnityEngine.Random.Range(0, Screen.width);
        m_transform.position = Camera.main.ScreenToWorldPoint(new Vector3(randomX, Screen.height, 10));
    }

    private void Update()
    {

        // 敌机下落
        m_transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);

        // 超过屏幕下面
        if (Camera.main.WorldToScreenPoint(m_transform.position).y <= -50)
        {
            BackToPool();
        }

        m_timeToFire += Time.deltaTime;
        if (m_timeToFire >= Const.EnemyFireTimer)
        {
            var bulletStartPos = m_transform.position + new Vector3(0,-0.5f,0);
            EnemyBulletGenerator.GenerateBullet(bulletStartPos,isBullet1);
            isBullet1 = !isBullet1;
            m_timeToFire = 0;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            blood--;
            if (blood <= 0)
            {
                GameMgr.Instance.Score += ((int)m_aircraftType+1) * Const.AddScore;
                BackToPool();
            }
        }
    }
    private void BackToPool()
    {
        ActiveSelf(false);
        backToPoolAction?.Invoke();
    }

    public void ActiveSelf(bool active)
    {
        m_collider.enabled = active;
        m_gameObject.SetActive(active);
    }

}
