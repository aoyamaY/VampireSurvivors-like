using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ĸ���
public class KnifeController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedknife = Instantiate(weaponData.Prefab);
        spawnedknife.transform.position = transform.position;
        // ��ȡ��ҵ��ƶ����򣬴���������Ϊ�ű�
        spawnedknife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
