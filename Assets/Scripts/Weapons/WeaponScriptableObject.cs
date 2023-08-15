using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    // �����Ļ������� �˺����ٶȡ���ȴʱ�� ����������ʱ�ı�ı���

    // ���л�
    [SerializeField]
    GameObject prefab;
    // ʹ�����Է�װ��ֻ��ȡ������ֵ
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

    // ����ǰ�������д���
    [SerializeField]
    int pierce;
    public int Pierce
    {
        get => pierce;
        private set => pierce = value;
    }
}
