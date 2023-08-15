using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 更改父类
public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // 设置小刀的移动
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
