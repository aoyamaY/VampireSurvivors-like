using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // ���ã��������������ƶ��ű���������Ⱦ��
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
        // ����ƶ�����������x��y��Ϊ0�������ƶ�
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
            // ��x�ᷴת��ɫ�Ͷ���
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
