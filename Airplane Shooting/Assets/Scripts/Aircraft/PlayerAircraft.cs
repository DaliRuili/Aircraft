using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAircraft : BaseAircraft
{
    public float maxSpeed = 1f; // 最大速度  
    public float acceleration = 0.8f; // 加速度  
    public float deceleration = 0.2f; // 减速度  
    private Vector3 velocity = Vector3.zero; // 当前速度  

    public float xMin, xMax, yMin, yMax;
    private PlayerBulletGenerator _playerBulletGenerator = new PlayerBulletGenerator();

    private void Update()
    {
        PlayerMovement();
        if (Input.GetMouseButton(0))
        {
            _playerBulletGenerator.Update();
        }
    }
    

    /// <summary>
    /// 玩家飞机移动
    /// </summary>
    public void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 desiredVelocity = new Vector3(moveHorizontal, 0, moveVertical) * maxSpeed;

        // 计算加速度或减速度  
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // 计算当前速度与目标速度之间的差值  
            Vector3 accelerationVector = desiredVelocity - velocity;
            // 限制加速度大小  
            float accelerationMagnitude = Mathf.Clamp01(accelerationVector.magnitude / acceleration);
            accelerationVector = accelerationVector.normalized * (accelerationMagnitude * acceleration);

            velocity += accelerationVector;
        }
        else
        {
            // 计算当前速度到零速度之间的差值  
            Vector3 decelerationVector = -velocity;
            // 限制减速度大小  
            float decelerationMagnitude = Mathf.Clamp01(decelerationVector.magnitude / deceleration);
            decelerationVector = decelerationVector.normalized * (decelerationMagnitude * deceleration);

            velocity += decelerationVector;
        }

        // 限制速度不超过最大速度  
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // 应用速度到飞船  
        m_rigidbody.velocity = new Vector2(velocity.x, velocity.z);

        //限制在屏幕内
        m_rigidbody.position = new Vector2
        (
            Mathf.Clamp(m_rigidbody.position.x, xMin, xMax),
            Mathf.Clamp(m_rigidbody.position.y, yMin, yMax)
        );
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Enemy")||other.tag.Contains("EnemyBullet"))
        {
            --blood;
            if (blood <= 0)
            {
                DestroySelf();
                GameMgr.Instance.GameOver();
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(m_gameObject);
        _playerBulletGenerator.ClearBullet();
    }
}