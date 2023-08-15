using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    // 武器的基础数据 伤害、速度、冷却时间 不会在运行时改变的变量

    // 序列化
    [SerializeField]
    GameObject prefab;
    // 使用属性封装，只读取，不赋值
    public GameObject Prefab
    {
        get => prefab;
        private set => prefab = value;
    }
    [SerializeField]

    float damage;
    public float Damage
    {
        get => damage;
        private set => damage = value;
    }
    [SerializeField]

    float speed;
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }
    [SerializeField]

    float cooldownDuration;
    public float CooldownDuration
    {
        get => cooldownDuration;
        private set => cooldownDuration = value;
    }

    // 销毁前的最大击中次数
    [SerializeField]
    int pierce;
    public int Pierce
    {
        get => pierce;
        private set => pierce = value;
    }
}
