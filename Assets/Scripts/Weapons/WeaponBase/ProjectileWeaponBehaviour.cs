using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    // ��������ķ���
    protected Vector3 direction;
    // �������ٵ�ʱ��
    public float destoryAfterSeconds;

    // ʵʱ��ֵ
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
        // ����ڸ�����ı任����
        Vector3 scale = transform.localScale;
        // ��ŷ���Ǳ�ʾ����ת
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // ��
        {
            scale.x = scale.x * -1;
            rotation.z = 45f;
        }
        else if (dirx == 0 && diry > 0) // ��
        {
            scale.x = scale.x * -1;
        }
        else if (dirx == 0 && diry < 0) // ��
        {
            scale.y = scale.y * -1;
        }
        else if (dirx > 0 && diry > 0) // ����
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // ����
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) //����
        {
            scale.x = scale.x * -1;
            rotation.z = 0f;
        }
        else if (dirx < 0 && diry < 0) // ����
        {
            scale.x = scale.x * -1;
            rotation.z = 90f;
        }

        transform.localScale = scale;
        //����һ����ת
        transform.rotation = Quaternion.Euler(rotation);
    }
    // �¼���������
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            // ȷ��ʹ�õ�ǰ�˺��������ǹ̶��˺�����Ϊ�����˺���仯
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
