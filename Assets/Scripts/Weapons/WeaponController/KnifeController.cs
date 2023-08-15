using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 更改父类
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
        // 获取玩家的移动方向，传给刀的行为脚本
        spawnedknife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
