using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ����
    public CharacterScriptableObject characterData;
    Rigidbody2D rb;
    // �ƶ�����
    // public �����󣬻��ڼ��Ӵ����п�����HideInInspector��ʹ������
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    // ����ƶ��ķ���
    public Vector2 lastMovedVector;
    
    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ��ǰ��ɫ�ĸ���
        rb = GetComponent<Rigidbody2D>();
        // ���ó�ʼ�����ƶ�����
        lastMovedVector = new Vector2(1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    // ������������Ҷ�����֡��
    // ��ÿ��Update����󶼻�ִ����Ӧ��ִ�еĶ��Fixedupdate����������ס��
    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        // ��ȡx���y��İ�������ֵ����ƽ������-1��0 �� 1��
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        // �������ĳ��ȱ�Ϊ1���þ��뱣��һ�£������ڶԽ������ƶ�ʱ��ˮƽ��ֱ�ƶ���
        moveDir = new Vector2(moveX, moveY).normalized;

        // ���˶�ֹ֮ͣǰ���������һ���˶���״̬
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
        // ����������ٶȣ����õ�λ / ����ʽ��
        rb.velocity = new Vector2(moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
}
