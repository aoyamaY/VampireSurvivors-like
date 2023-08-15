using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 引用
    public CharacterScriptableObject characterData;
    Rigidbody2D rb;
    // 移动方向
    // public 公开后，会在检视窗口中看到，HideInInspector可使其隐藏
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    // 最后移动的方向
    public Vector2 lastMovedVector;
    
    // Start is called before the first frame update
    void Start()
    {
        // 获取当前角色的刚体
        rb = GetComponent<Rigidbody2D>();
        // 设置初始化的移动方向
        lastMovedVector = new Vector2(1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    // 用于物理计算且独立于帧率
    // 在每次Update卡完后都会执行完应该执行的多次Fixedupdate（尽量不卡住）
    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        // 获取x轴和y轴的按键输入值（不平滑处理：-1、0 或 1）
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        // 将向量的长度变为1，让距离保持一致，以免在对角线上移动时比水平或垂直移动快
        moveDir = new Vector2(moveX, moveY).normalized;

        // 在运动停止之前，保存最后一次运动的状态
        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    private void Move()
    {
        // 刚体的线性速度，采用单位 / 秒形式。
        rb.velocity = new Vector2(moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
}
