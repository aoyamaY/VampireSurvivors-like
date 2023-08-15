using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    // 武器发射的方向
    protected Vector3 direction;
    // 武器销毁的时间
    public float destoryAfterSeconds;

    // 实时数值
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destoryAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;
        // 相对于父对象的变换缩放
        Vector3 scale = transform.localScale;
        // 以欧拉角表示的旋转
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // 左
        {
            scale.x = scale.x * -1;
            rotation.z = 45f;
        }
        else if (dirx == 0 && diry > 0) // 上
        {
            scale.x = scale.x * -1;
        }
        else if (dirx == 0 && diry < 0) // 下
        {
            scale.y = scale.y * -1;
        }
        else if (dirx > 0 && diry > 0) // 右上
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // 右下
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) //左上
        {
            scale.x = scale.x * -1;
            rotation.z = 0f;
        }
        else if (dirx < 0 && diry < 0) // 左下
        {
            scale.x = scale.x * -1;
            rotation.z = 90f;
        }

        transform.localScale = scale;
        //返回一个旋转
        transform.rotation = Quaternion.Euler(rotation);
    }
    // 事件处理器？
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            // 确保使用当前伤害，而不是固定伤害，因为后期伤害会变化
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        } 
    }
    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce < 0)
        {
            Destroy(gameObject);
        }
    }
}
