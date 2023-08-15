using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // 引用：动画组件，玩家移动脚本，精灵渲染器
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;
    
    
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果移动方向向量的x或y不为0，即在移动
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if (pm.lastHorizontalVector < 0)
        {
            // 在x轴反转角色和动画
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
