using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // 跟踪目标
    public Transform target;
    // 偏移量
    public Vector3 offset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 设置相机位置
        transform.position = target.position + offset;
    }
}
